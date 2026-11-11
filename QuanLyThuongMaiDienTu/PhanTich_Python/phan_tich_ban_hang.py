import argparse
import math
import os
import re
import sys
from collections import Counter, defaultdict
from datetime import datetime, timedelta
from decimal import Decimal, ROUND_HALF_UP

import pandas as pd

sys.path.insert(0, os.path.dirname(os.path.abspath(__file__)))
import nguon_so_lieu as nsl

THU = ["Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7", "Chủ nhật"]
TRANG_THAI_HUY = "Đã hủy"
TRANG_THAI_DA_GIAO = ["Đã giao", "Trả hàng/Hoàn tiền"]

BANG = [
    "DonHang", "ChiTietDonHang", "SanPham", "DanhMuc", "KhachHang", "DoiTac", "NhaCungCap",
    "PhieuNhap", "ChiTietPhieuNhap", "PhieuXuat", "ChiTietPhieuXuat", "TraHang", "HangHoanVeKho",
    "DotSale", "NgayLe", "PhiSan", "DanhGiaSanPham", "KhaoSat_LyDoMua",
    "TuDien_Teencode", "TuDien_CamXuc", "TuDien_LyDoMua", "CachBan_TheoLyDoMua",
    "DuLieuTho_KhachHang", "DuLieuTho_DonHang",
    "PhieuKiemKe", "ChiTietKiemKe", "ChiPhiVanHanh", "BaoHanh", "TienSanChuyenVe", "ThanhToanNCC",
]
BANG_CHI_CO_TREN_SERVER = ["TonKho", "PhanTich_CamXuc_DanhGia"]

TONG_BUOC = 12


def in_buoc(so, noi_dung):
    print(f"Bước {so}/{TONG_BUOC}: {noi_dung}")


def lam_tron(x, so_le=0):
    if x is None or (isinstance(x, float) and (math.isnan(x) or math.isinf(x))):
        return None
    return float(Decimal(repr(float(x))).quantize(Decimal(1).scaleb(-so_le), rounding=ROUND_HALF_UP))


def so_vn(x, so_le):
    if x is None or (isinstance(x, float) and math.isnan(x)):
        return ""
    if so_le == 0:
        return str(int(lam_tron(x, 0)))
    return f"{lam_tron(x, 1):.1f}".replace(".", ",")


def tien_vn(x):
    return f"{lam_tron(x, 0):,.0f}".replace(",", ".")


def ntile(so_dong, so_nhom):
    co_ban, du = divmod(so_dong, so_nhom)
    kq = []
    for nhom in range(1, so_nhom + 1):
        kq += [nhom] * (co_ban + (1 if nhom <= du else 0))
    return kq


def ngay_cua(x):
    if x is None or (isinstance(x, float) and math.isnan(x)) or x is pd.NaT:
        return pd.NaT
    if isinstance(x, (datetime, pd.Timestamp)):
        return pd.Timestamp(x)
    return pd.Timestamp(nsl.doc_ngay(x))


def so(x):
    return pd.to_numeric(x, errors="coerce")


# ---------------------------------------------------------------- đọc dữ liệu

def doc_tu_file_sql(duong_dan):
    with open(duong_dan, encoding="utf-8-sig") as f:
        noi_dung = f.read().replace("\r\n", "\n")
    mau = re.compile(r"INSERT INTO (\w+)\s*\(([^)]*)\)\s*VALUES\s*\n", re.I)
    bang = defaultdict(list)
    for m in mau.finditer(noi_dung):
        ten = m.group(1)
        if ten not in BANG:
            continue
        cot = [c.strip() for c in m.group(2).split(",")]
        bat_dau = ket_thuc = m.end()
        trong_chu = False
        while ket_thuc < len(noi_dung):
            c = noi_dung[ket_thuc]
            if trong_chu:
                if c == "'":
                    if noi_dung[ket_thuc + 1:ket_thuc + 2] == "'":
                        ket_thuc += 2
                        continue
                    trong_chu = False
            elif c == "'":
                trong_chu = True
            elif c == ";":
                break
            ket_thuc += 1
        for dong in nsl.tach_cac_dong(noi_dung[bat_dau:ket_thuc + 1]):
            bang[ten].append(dict(zip(cot, dong)))
    kq = {ten: pd.DataFrame(bang.get(ten, [])) for ten in BANG}
    chot = dict(re.findall(r"EXEC dbo\.sp_ChotKiemKe @MaKK = '(\w+)', @NgayChot = '([^']+)'", noi_dung))
    kk = kq["PhieuKiemKe"]
    if len(kk):
        kk["NgayChot"] = kk["MaKK"].map(chot)
        kk.loc[kk["NgayChot"].notna(), "TrangThai"] = "Đã chốt"
    return kq


def doc_tu_sql_server(chuoi_ket_noi):
    import pyodbc
    kq = {}
    with pyodbc.connect(chuoi_ket_noi) as conn:
        for ten in BANG + BANG_CHI_CO_TREN_SERVER:
            kq[ten] = pd.read_sql(f"SELECT * FROM dbo.{ten}", conn)
    return kq


def them_cot_neu_thieu(df, cot, mac_dinh=None):
    if cot not in df.columns:
        df[cot] = mac_dinh
    return df


def chuan_hoa(b, tu_file):
    dh = b["DonHang"]
    for c in ["NgayDat", "NgayXacNhan", "NgayGiao", "NgayHuy"]:
        them_cot_neu_thieu(dh, c)
        dh[c] = dh[c].map(ngay_cua)
    for c in ["GiamGiaCuaShop", "PhiVanChuyenKhachTra"]:
        them_cot_neu_thieu(dh, c, 0)
        dh[c] = so(dh[c]).fillna(0)
    them_cot_neu_thieu(dh, "HinhThucNhan", "Giao tận nơi")
    ct = b["ChiTietDonHang"]
    ct["SoLuong"] = so(ct["SoLuong"])
    ct["DonGia"] = so(ct["DonGia"])
    sp = b["SanPham"]
    sp["GiaBan"] = so(sp["GiaBan"])
    for t, c in [("PhieuNhap", "NgayNhap"), ("PhieuNhap", "NgayDatHang"), ("PhieuXuat", "NgayXuat"), ("TraHang", "NgayTra"),
                 ("HangHoanVeKho", "NgayHoan"), ("HangHoanVeKho", "NgayNhanVe"), ("DanhGiaSanPham", "NgayDanhGia"),
                 ("KhaoSat_LyDoMua", "NgayTraLoi"), ("DotSale", "NgayBatDau"), ("DotSale", "NgayKetThuc"), ("NgayLe", "Ngay"),
                 ("PhiSan", "TuNgay"), ("PhiSan", "DenNgay"), ("DuLieuTho_KhachHang", "NgayTao"),
                 ("DuLieuTho_DonHang", "NgayDat"), ("DuLieuTho_DonHang", "NgayTao")]:
        them_cot_neu_thieu(b[t], c)
        b[t][c] = b[t][c].map(ngay_cua)
    for t in ["ChiTietPhieuNhap", "ChiTietPhieuXuat", "TraHang", "HangHoanVeKho"]:
        b[t]["SoLuong"] = so(b[t]["SoLuong"])
    b["ChiTietPhieuNhap"]["DonGia"] = so(b["ChiTietPhieuNhap"]["DonGia"])
    th = b["TraHang"]
    th["SoTienHoan"] = so(th["SoTienHoan"]).fillna(0)
    them_cot_neu_thieu(th, "TinhTrangHang", "Còn bán được")
    th["TinhTrangHang"] = th["TinhTrangHang"].fillna("Còn bán được")
    them_cot_neu_thieu(b["HangHoanVeKho"], "TrangThai", "Đã nhận về kho")
    b["DanhGiaSanPham"]["SoSao"] = so(b["DanhGiaSanPham"]["SoSao"])
    ks = b["KhaoSat_LyDoMua"]
    ks["MucHaoHung"] = so(ks["MucHaoHung"])
    b["TuDien_CamXuc"]["Diem"] = so(b["TuDien_CamXuc"]["Diem"])
    b["TuDien_LyDoMua"]["ThuTuNhom"] = so(b["TuDien_LyDoMua"]["ThuTuNhom"])
    b["PhiSan"]["GiaTri"] = so(b["PhiSan"]["GiaTri"])
    b["DuLieuTho_DonHang"]["TongTien"] = so(b["DuLieuTho_DonHang"]["TongTien"])
    for t, cot in [("PhieuKiemKe", ["MaKK", "MaKho", "NgayKiemKe", "TrangThai", "NgayChot"]),
                   ("ChiTietKiemKe", ["MaKK", "MaSP", "SoSach", "ThucTe", "LyDoLech"]),
                   ("ChiPhiVanHanh", ["Thang", "NhomChiPhi", "TenChiPhi", "SoTien", "CachTinh"]),
                   ("BaoHanh", ["MaBaoHanh", "MaSP", "MaDH", "NgayNhan", "TrangThai", "NgayTraKhach", "ChiPhiShopChiu"]),
                   ("TienSanChuyenVe", ["MaDH", "MaDot", "NgayChuyen", "SoTienSanTra", "GhiChu"]),
                   ("ThanhToanNCC", ["MaTT", "MaPN", "NgayTra", "SoTien", "HinhThuc"])]:
        for c in cot:
            them_cot_neu_thieu(b[t], c)
    for t, c in [("PhieuKiemKe", "NgayKiemKe"), ("PhieuKiemKe", "NgayChot"), ("ChiPhiVanHanh", "Thang"), ("BaoHanh", "NgayNhan"),
                 ("BaoHanh", "NgayTraKhach"), ("TienSanChuyenVe", "NgayChuyen"), ("ThanhToanNCC", "NgayTra")]:
        b[t][c] = b[t][c].map(ngay_cua)
    for t, c in [("ChiTietKiemKe", "SoSach"), ("ChiTietKiemKe", "ThucTe"), ("ChiPhiVanHanh", "SoTien"), ("BaoHanh", "ChiPhiShopChiu"),
                 ("TienSanChuyenVe", "SoTienSanTra"), ("ThanhToanNCC", "SoTien")]:
        b[t][c] = so(b[t][c])
    b["BaoHanh"]["ChiPhiShopChiu"] = b["BaoHanh"]["ChiPhiShopChiu"].fillna(0)
    if "NhaCungCap" in b:
        them_cot_neu_thieu(b["NhaCungCap"], "SoNgayCongNo", 0)
        b["NhaCungCap"]["SoNgayCongNo"] = so(b["NhaCungCap"]["SoNgayCongNo"]).fillna(0)
    if tu_file:
        for t in ["KhaoSat_LyDoMua", "TuDien_Teencode", "TuDien_CamXuc", "TuDien_LyDoMua", "DuLieuTho_KhachHang", "DuLieuTho_DonHang"]:
            b[t]["Id"] = range(1, len(b[t]) + 1)
        lam_lai_viec_sql_server_tu_lam(b)
    else:
        b["TonKho"]["SoLuongTon"] = so(b["TonKho"]["SoLuongTon"])
        for t in ["KhaoSat_LyDoMua", "TuDien_Teencode", "TuDien_CamXuc", "TuDien_LyDoMua", "DuLieuTho_KhachHang", "DuLieuTho_DonHang"]:
            b[t]["Id"] = so(b[t]["Id"])
        b["SanPham"]["GiaVon"] = so(b["SanPham"]["GiaVon"])
        ct["GiaVon"] = so(ct["GiaVon"])
        dh["TongTien"] = so(dh["TongTien"]).fillna(0)
    return b


def lam_lai_viec_sql_server_tu_lam(b):
    ct, sp, ctpn = b["ChiTietDonHang"], b["SanPham"], b["ChiTietPhieuNhap"]
    b["DonHang"]["TongTien"] = b["DonHang"]["MaDH"].map((ct["SoLuong"] * ct["DonGia"]).groupby(ct["MaDH"]).sum()).fillna(0)
    nhap = ctpn.assign(T=ctpn["SoLuong"] * ctpn["DonGia"]).groupby("MaSP").agg(T=("T", "sum"), SL=("SoLuong", "sum"))
    gia_von = (nhap["T"] / nhap["SL"]).map(lambda x: lam_tron(x, 2))
    sp["GiaVon"] = sp["MaSP"].map(gia_von)
    ct["GiaVon"] = ct["MaSP"].map(gia_von)
    dong = []
    pn, px = b["PhieuNhap"], b["PhieuXuat"]
    for r in ctpn.merge(pn[["MaPN", "MaKho"]], on="MaPN").itertuples():
        dong.append((r.MaKho, r.MaSP, r.SoLuong))
    for r in b["ChiTietPhieuXuat"].merge(px[["MaPX", "MaKho"]], on="MaPX").itertuples():
        dong.append((r.MaKho, r.MaSP, -r.SoLuong))
    th = b["TraHang"]
    for r in th[th["TinhTrangHang"] == "Còn bán được"].itertuples():
        dong.append((r.MaKho, r.MaSP, r.SoLuong))
    hh = b["HangHoanVeKho"]
    for r in hh[hh["TrangThai"] == "Đã nhận về kho"].itertuples():
        dong.append((r.MaKho, r.MaSP, r.SoLuong))
    kk = b["PhieuKiemKe"]
    for r in b["ChiTietKiemKe"].merge(kk[kk["TrangThai"] == "Đã chốt"][["MaKK", "MaKho"]], on="MaKK").itertuples():
        dong.append((r.MaKho, r.MaSP, r.ThucTe - r.SoSach))
    tk = pd.DataFrame(dong, columns=["MaKho", "MaSP", "SoLuongTon"]).groupby(["MaKho", "MaSP"], as_index=False)["SoLuongTon"].sum()
    b["TonKho"] = tk
    b["PhanTich_CamXuc_DanhGia"] = pd.DataFrame(columns=["DoiTuongId", "LoaiDoiTuong", "NhomCamXuc", "GanTay"])


# ---------------------------------------------------------------- làm sạch chữ (fn_LamSachKyTu, teencode)

KY_TU_BO = "!@#$%^&*()_+={}[]|\\<>?~`\":;'"


def lam_sach_ky_tu(van_ban):
    if van_ban is None or (isinstance(van_ban, float) and math.isnan(van_ban)):
        return None
    s = str(van_ban)
    for c in KY_TU_BO:
        s = s.replace(c, " ")
    s = s.replace("\t", " ").replace("\n", " ").replace("\r", " ")
    s = re.sub(" {2,}", " ", s)
    return s.strip(" ")


def ds_teencode(b):
    t = b["TuDien_Teencode"].copy()
    t["Dai"] = t["TuGoc"].map(lambda x: len(str(x).rstrip(" ")))
    t = t.sort_values(["Dai", "Id"], ascending=[False, True])
    return list(zip(t["TuGoc"].str.lower(), t["TuChuanHoa"]))


def thay_teencode(txt, teen):
    for _ in range(2):
        for goc, chuan in teen:
            txt = txt.replace(" " + goc + " ", " " + chuan + " ")
    return txt


def chuan_bi_chu(txt, teen):
    s = "" if txt is None or (isinstance(txt, float) and math.isnan(txt)) else str(txt)
    s = s.lower().replace(",", " ").replace(".", " ").replace("-", " ").replace("/", " ")
    return thay_teencode(" " + lam_sach_ky_tu(s) + " ", teen)


# ---------------------------------------------------------------- sp_TuDongLamSach + sp_PhanTichChuDeKhieuNai

def nhom_ly_do_tra(b, teen):
    th = b["TraHang"].copy()

    def lam_sach(x):
        if x is None or (isinstance(x, float) and math.isnan(x)):
            return None
        return thay_teencode(" " + lam_sach_ky_tu(x).lower() + " ", teen).strip(" ")

    th["NoiDung_DaLamSach"] = th["LyDoTra"].map(lam_sach)

    def xep(r):
        txt = r["NoiDung_DaLamSach"] if r["NoiDung_DaLamSach"] is not None else r["LyDoTra"]
        if txt is None or (isinstance(txt, float) and math.isnan(txt)) or str(txt).strip(" ") == "":
            return "Không nêu lý do"
        t = str(txt).lower()
        co = lambda *ds: any(x in t for x in ds)
        if co("lỗi", "hỏng", "vỡ", "trầy", "xước", "móp", "không hoạt động"):
            return "Hàng lỗi / hư hỏng / trầy xước"
        if co("khác mô tả", "không đúng mô tả", "không giống", "khác hình", "hàng giả", "nhái"):
            return "Không đúng mô tả / hàng giả"
        if co("sai", "thiếu", "nhầm", "không đúng"):
            return "Giao sai / thiếu hàng"
        if co("chậm", "trễ", "lâu"):
            return "Giao hàng chậm"
        if co("đổi ý", "không cần", "không thích", "không vừa"):
            return "Đổi ý / không có nhu cầu"
        return "Khác"

    th["NhomLyDoChinh"] = th.apply(xep, axis=1)
    return th


# ---------------------------------------------------------------- sp_PhanTichCamXuc_DanhGia

def doc_cam_xuc(b, teen):
    tu = [(str(r.TuKhoa).lower(), int(r.Diem)) for r in b["TuDien_CamXuc"].itertuples()]
    dong = []
    for loai, bang, ma, cot in [("Đánh giá", "DanhGiaSanPham", "MaDanhGia", "NoiDung"), ("Trả hàng", "TraHang", "MaTraHang", "LyDoTra")]:
        for r in b[bang].itertuples():
            txt = chuan_bi_chu(getattr(r, cot), teen)
            khen = che = 0
            tu_khen, tu_che = [], []
            for kw, diem in tu:
                if f" {kw} " not in txt:
                    continue
                ten = kw
                for phu_dinh in ("không", "chưa", "chẳng"):
                    if f" {phu_dinh} {kw} " in txt:
                        diem = -diem
                        ten = f"{phu_dinh} {kw}"
                        break
                if diem > 0:
                    khen += 1
                    tu_khen.append(ten)
                else:
                    che += 1
                    tu_che.append(ten)
            nhom = "Khen" if khen > che else ("Chê" if che > khen else "Trung tính")
            dong.append((loai, getattr(r, ma), khen, che, nhom, tu_khen, tu_che))
    kq = pd.DataFrame(dong, columns=["Loai", "Ma", "SoTuKhen", "SoTuChe", "NhomCamXuc", "TuKhen", "TuChe"])
    gt = b["PhanTich_CamXuc_DanhGia"]
    gt = gt[so(gt["GanTay"]).fillna(0) == 1] if len(gt) else gt
    if len(gt):
        tay = {(r.LoaiDoiTuong, str(r.DoiTuongId)): r.NhomCamXuc for r in gt.itertuples()}
        kq["NhomCamXuc"] = [tay.get((l, str(m)), n) for l, m, n in zip(kq["Loai"], kq["Ma"], kq["NhomCamXuc"])]
    return kq


# ---------------------------------------------------------------- sp_PhanTichLyDoMua

def xep_ly_do_mua(b, teen):
    tu = b["TuDien_LyDoMua"]
    tu = list(zip(tu["TuKhoa"].str.lower(), tu["NhomLyDo"], tu["ThuTuNhom"]))
    kq = []
    for r in b["KhaoSat_LyDoMua"].itertuples():
        txt = chuan_bi_chu(r.LoiKhachNoi, teen)
        dem = Counter()
        thu_tu = {}
        for kw, nhom, tt in tu:
            if f" {kw} " in txt:
                dem[nhom] += 1
                thu_tu[nhom] = tt
        if dem:
            nhom = sorted(dem, key=lambda n: (-dem[n], thu_tu[n]))[0]
            kq.append((r.Id, nhom, thu_tu[nhom], dem[nhom]))
        else:
            kq.append((r.Id, "Khác / không rõ", 99, 0))
    return pd.DataFrame(kq, columns=["KhaoSatId", "NhomLyDo", "ThuTuNhom", "SoTuKhop"])


# ---------------------------------------------------------------- fn_DuBaoBanHang (vw_DuBaoSanPham: 90, 30, 1.65, 7, 60, 20, 30)

def du_bao(b, so_ngay=90, so_ngay_gan=30, he_so_z=1.65, lead_mac_dinh=7.0, ngay_ton_nhieu=60, thay_doi=20.0, min_so_ban=30):
    px, cx, pn = b["PhieuXuat"], b["ChiTietPhieuXuat"], b["PhieuNhap"]
    den = px["NgayXuat"].max().normalize()
    tu = den - timedelta(days=so_ngay - 1)
    tu_gan = den - timedelta(days=so_ngay_gan - 1)
    x = cx.merge(px[["MaPX", "NgayXuat"]], on="MaPX")
    x = x[(x["NgayXuat"] >= tu) & (x["NgayXuat"] < den + timedelta(days=1))]
    x["Ngay"] = x["NgayXuat"].dt.normalize()
    ban_ngay = x.groupby(["MaSP", "Ngay"], as_index=False)["SoLuong"].sum()
    tong = ban_ngay.groupby("MaSP")["SoLuong"].sum()
    binh = (ban_ngay["SoLuong"] ** 2).groupby(ban_ngay["MaSP"]).sum()
    tong_gan = ban_ngay[ban_ngay["Ngay"] >= tu_gan].groupby("MaSP")["SoLuong"].sum()

    p = pn[pn["NgayDatHang"].notna() & (pn["NgayNhap"] < den + timedelta(days=1))].copy()
    p["LT"] = (p["NgayNhap"].dt.normalize() - p["NgayDatHang"].dt.normalize()).dt.days.astype(float)
    lead = p.groupby("MaNCC")["LT"].agg(["mean", lambda s: s.std(ddof=1) if len(s) > 1 else 0.0])
    lead.columns = ["LT_TB", "LT_Lech"]

    ncc_gan = (b["ChiTietPhieuNhap"].merge(pn[["MaPN", "NgayNhap", "MaNCC"]], on="MaPN")
               .sort_values(["MaSP", "NgayNhap", "MaPN"], ascending=[True, False, False])
               .drop_duplicates("MaSP").set_index("MaSP")["MaNCC"])
    ten_ncc = b["NhaCungCap"].set_index("MaNCC")["TenNCC"]
    ton = b["TonKho"].groupby("MaSP")["SoLuongTon"].sum()
    mon_kem = mon_hay_mua_chung_nhat(b)
    ten_sp = b["SanPham"].set_index("MaSP")["TenSP"]

    kq = []
    for r in b["SanPham"].itertuples():
        m = r.MaSP
        t = float(tong.get(m, 0))
        ads = t / so_ngay
        ads30 = float(tong_gan.get(m, 0)) / so_ngay_gan
        if m not in tong.index:
            lech_ngay = 0.0
        else:
            v = float(binh.get(m, 0)) - t * t / so_ngay
            lech_ngay = 0.0 if v < 0 else math.sqrt(v / (so_ngay - 1))
        ncc = ncc_gan.get(m)
        co_lead = ncc in lead.index if ncc is not None else False
        lt = float(lead.loc[ncc, "LT_TB"]) if co_lead else lead_mac_dinh
        lt_lech = float(lead.loc[ncc, "LT_Lech"]) if co_lead and not pd.isna(lead.loc[ncc, "LT_Lech"]) else 0.0
        ton_an_toan = int(math.ceil(he_so_z * math.sqrt(lt * lech_ngay ** 2 + ads ** 2 * lt_lech ** 2)))
        toc_do = ads30 if (t >= min_so_ban and ads30 > ads) else ads
        rop = int(math.ceil(ads * lt)) + ton_an_toan
        ton_ht = int(ton.get(m, 0))
        so_ngay_con = lam_tron(ton_ht / ads, 1) if ads > 0 else None
        xu_huong = lam_tron(100.0 * (ads30 / ads - 1), 1) if (ads > 0 and t >= min_so_ban) else None
        du_doan = int(lam_tron(toc_do * 30, 0))
        if ads == 0:
            cau = "Không có bán"
        elif t < min_so_ban:
            cau = "Bán ít, chưa đủ số liệu"
        elif xu_huong >= thay_doi:
            cau = "ĐANG HOT - bán nhanh lên"
        elif xu_huong <= -thay_doi:
            cau = "ĐANG NGUỘI - bán chậm lại"
        else:
            cau = "Bán đều"
        if ads == 0:
            tt = "KHÔNG BÁN"
        elif ton_ht < ads * lt:
            tt = "KHẨN CẤP"
        elif ton_ht <= rop:
            tt = "ĐẶT HÀNG NGAY"
        elif so_ngay_con > ngay_ton_nhieu:
            tt = "TỒN NHIỀU"
        else:
            tt = "ỔN"
        can = int(math.ceil(toc_do * (lt + 30))) + ton_an_toan - ton_ht
        nen_nhap = can if (tt in ("KHẨN CẤP", "ĐẶT HÀNG NGAY") and can > 0) else 0
        kem = mon_kem.get(m)
        ten_kem = ten_sp.get(kem) if kem else None
        if tt == "KHÔNG BÁN":
            thong_bao = f"Không bán được cái nào trong {so_ngay} ngày qua. Tồn {ton_ht} cái."
        else:
            cuoi = {"KHẨN CẤP": "KHẨN CẤP: tồn không đủ bán trong lúc chờ hàng về, đặt ngay.",
                    "ĐẶT HÀNG NGAY": "Tồn đã chạm mức phải đặt hàng, cần đặt ngay.",
                    "TỒN NHIỀU": f"Tồn đủ bán hơn {ngay_ton_nhieu} ngày, chưa cần nhập thêm."}.get(tt, "Còn đủ hàng, chưa cần đặt.")
            thong_bao = (f"Bán TB {so_vn(ads, 1)} cái/ngày ({cau.lower()}), hàng về mất TB {so_vn(lt, 1)} ngày"
                         + ("" if co_lead else " (chưa có dữ liệu thật, đang tạm tính)")
                         + f". Tồn {ton_ht} cái, đủ bán ~{so_vn(so_ngay_con, 1)} ngày; mức phải đặt hàng là {rop} cái. " + cuoi)
        hot = " Món đang hot nên số đặt đã tính theo tốc độ bán 30 ngày gần nhất." if cau == "ĐANG HOT - bán nhanh lên" else ""
        if tt == "KHÔNG BÁN" and ton_ht > 0:
            viec = (f"Không bán được cái nào trong {so_ngay} ngày qua mà còn tồn {ton_ht} cái: xả hàng (giảm giá, tặng kèm"
                    + (f', ghép với "{ten_kem}"' if ten_kem else "") + ").")
        elif tt == "KHÔNG BÁN":
            viec = "Không bán, không còn tồn: chưa cần làm gì."
        elif tt == "KHẨN CẤP":
            viec = f"Đặt NGAY {nen_nhap} cái: tồn chỉ đủ bán {so_vn(so_ngay_con, 1)} ngày mà hàng về mất {so_vn(lt, 1)} ngày." + hot
        elif tt == "ĐẶT HÀNG NGAY":
            viec = f"Đặt {nen_nhap} cái trong tuần này để đủ bán 30 ngày sau khi hàng về." + hot
        elif tt == "TỒN NHIỀU":
            viec = (f"Tồn đủ bán {so_vn(so_ngay_con, 1)} ngày: chưa nhập thêm"
                    + (f', làm combo với "{ten_kem}" để bán nhanh hơn' if ten_kem else "") + ".")
        elif cau == "ĐANG HOT - bán nhanh lên":
            viec = "Đang hot: đăng bài, chạy quảng cáo ngay và xem tồn kho mỗi ngày" + (f'; gợi ý khách mua kèm "{ten_kem}"' if ten_kem else "") + "."
        elif cau == "ĐANG NGUỘI - bán chậm lại":
            viec = "Đang bán chậm lại: xem lại giá, ảnh, đánh giá xấu gần đây."
        else:
            viec = "Ổn, giữ như hiện tại."
        kq.append(dict(MaSP=m, TenSP=r.TenSP, TenNCC=ten_ncc.get(ncc) if ncc else None, TonHienTai=ton_ht, Tong=t, ADS=ads, ADS30=ads30,
                       LeadTime=lt, LT_Lech=lt_lech, TonAnToan=ton_an_toan, ROP=rop, SoNgayConLai=so_ngay_con, XuHuongPT=xu_huong,
                       DuDoan30Ngay=du_doan, SoNenNhap=nen_nhap, MonHayMuaChung=ten_kem, Cau=cau, TrangThai=tt,
                       ThongBao=thong_bao, ViecNenLam=viec))
    return pd.DataFrame(kq), den


def mon_hay_mua_chung_nhat(b):
    ct = b["ChiTietDonHang"][["MaDH", "MaSP"]].drop_duplicates()
    cap = ct.merge(ct, on="MaDH")
    cap = cap[cap["MaSP_x"] != cap["MaSP_y"]]
    dem = cap.groupby(["MaSP_x", "MaSP_y"]).size().reset_index(name="SoDon")
    dem = dem.sort_values(["MaSP_x", "SoDon", "MaSP_y"], ascending=[True, False, True]).drop_duplicates("MaSP_x")
    return dict(zip(dem["MaSP_x"], dem["MaSP_y"]))


# ---------------------------------------------------------------- vw_CungCau

def cung_cau(b, db):
    dh, ct = b["DonHang"], b["ChiTietDonHang"]
    den = b["PhieuXuat"]["NgayXuat"].max().normalize()
    sale = ngay_sale_set(b)
    d = ct.merge(dh[["MaDH", "NgayDat", "TrangThai"]], on="MaDH")
    d = d[~d["TrangThai"].fillna("").str.contains("hủy")].copy()
    d["Tien"] = d["SoLuong"] * d["DonGia"]
    d["LaSale"] = d["NgayDat"].dt.normalize().isin(sale)
    trong30 = (d["NgayDat"] >= den - timedelta(days=29)) & (d["NgayDat"] < den + timedelta(days=1))
    truoc = (d["NgayDat"] >= den - timedelta(days=89)) & (d["NgayDat"] < den - timedelta(days=29))

    def gia(mask):
        g = d[mask].groupby("MaSP").agg(SL=("SoLuong", "sum"), T=("Tien", "sum"))
        return (g["T"] / g["SL"]).where(g["SL"] > 0)

    g30, gtr, gsale, gthuong = gia(trong30), gia(truoc), gia(d["LaSale"]), gia(~d["LaSale"])
    nhap = b["ChiTietPhieuNhap"].assign(T=lambda x: x["SoLuong"] * x["DonGia"]).groupby("MaSP").agg(T=("T", "sum"), SL=("SoLuong", "sum"))
    gia_nhap = nhap["T"] / nhap["SL"]
    kq = []
    for v in db.itertuples():
        m = v.MaSP
        gn = gia_nhap.get(m)
        p30, ptr = g30.get(m), gtr.get(m)
        gia_doi = lam_tron(100.0 * (p30 / ptr - 1), 1) if (p30 is not None and not pd.isna(p30) and ptr is not None and not pd.isna(ptr) and ptr > 0) else None
        tien_kho = lam_tron(v.TonHienTai * (gn if gn is not None and not pd.isna(gn) else 0), 0)
        gt_, gs_ = gthuong.get(m), gsale.get(m)
        co = lambda x: x is not None and not pd.isna(x)
        lai_thuong = (gt_ - gn) if (co(gt_) and co(gn)) else None
        lai_sale = (gs_ - gn) if (co(gs_) and co(gn)) else None
        if v.TrangThai == "KHÔNG BÁN":
            cc = "Cung > Cầu: không ai mua"
        elif v.TrangThai in ("KHẨN CẤP", "ĐẶT HÀNG NGAY"):
            cc = "Cung < Cầu: hàng ít, khách nhiều"
        elif v.TrangThai == "TỒN NHIỀU" or v.Cau == "ĐANG NGUỘI - bán chậm lại":
            cc = "Cung > Cầu: hàng nhiều, khách ít"
        else:
            cc = "Cung = Cầu: cân bằng"
        gd = {"ĐANG HOT - bán nhanh lên": "Đầu sóng: khách đang đổ vào", "ĐANG NGUỘI - bán chậm lại": "Cuối sóng: khách đang bớt",
              "Bán ít, chưa đủ số liệu": "Chưa rõ: bán còn ít"}.get(v.Cau, "Giữa sóng: bán đều")
        if v.TrangThai == "KHÔNG BÁN":
            gd = "Hết sóng"
        if v.ADS == 0 or v.SoNgayConLai is None or pd.isna(v.SoNgayConLai):
            thu = "Kẹt tiền: hàng không bán ra được"
        elif v.SoNgayConLai <= 15:
            thu = "Nhanh: dưới 15 ngày bán hết tồn"
        elif v.SoNgayConLai <= 45:
            thu = "Vừa: 15 - 45 ngày bán hết tồn"
        else:
            thu = "Chậm: trên 45 ngày mới bán hết tồn"
        gxh = ("Chưa đủ số liệu giá" if gia_doi is None else "Giá đang lên" if gia_doi >= 3 else "Giá đang xuống" if gia_doi <= -3 else "Giá đứng yên")
        combo = f' (combo với "{v.MonHayMuaChung}")' if v.MonHayMuaChung else ""
        if v.TrangThai == "KHÔNG BÁN" and v.TonHienTai > 0:
            nen = f"Hết sóng, không ai mua: xả hàng sớm để lấy tiền về, đừng nhập thêm. Đang kẹt {tien_vn(tien_kho)} đ trong kho."
        elif v.TrangThai == "KHÔNG BÁN":
            nen = "Không bán, không tồn: chưa cần làm gì."
        elif gd.startswith("Đầu sóng") and cc.startswith("Cung < Cầu"):
            nen = f"Đầu sóng, hàng đang thiếu: giữ giá, chưa cần giảm giá; nhập thêm {v.SoNenNhap} cái nhưng đừng ôm quá nhiều vì sóng nào cũng qua."
        elif gd.startswith("Đầu sóng"):
            nen = "Đầu sóng, khách đang đổ vào: giữ giá, đẩy quảng cáo, xem tồn kho mỗi ngày để không bị đứt hàng."
        elif gd.startswith("Cuối sóng") and cc.startswith("Cung > Cầu"):
            nen = f"Cuối sóng, hàng nhiều khách ít: dừng nhập, xả bớt{combo} trước khi giá rớt sâu. Đang kẹt {tien_vn(tien_kho)} đ trong kho."
        elif gd.startswith("Cuối sóng") and cc.startswith("Cung < Cầu"):
            nen = f"Cuối sóng nhưng hàng sắp hết: chỉ đặt {v.SoNenNhap} cái cho đủ bán, đừng nhập dư."
        elif gd.startswith("Cuối sóng"):
            nen = "Cuối sóng, khách bớt dần: nhập ít lại, chưa cần giảm giá."
        elif cc.startswith("Cung < Cầu"):
            nen = f"Hàng sắp thiếu: đặt {v.SoNenNhap} cái, giữ giá."
        elif cc.startswith("Cung > Cầu"):
            nen = f"Tồn nhiều so với sức bán: chưa nhập thêm{combo}."
        else:
            nen = "Cân bằng: lời đủ là được, nhập đều tay theo tốc độ bán."
        if lai_sale is not None and lai_thuong is not None:
            if lai_sale < 0:
                nen += f" Ngày sale khách đông nhưng mỗi cái đang lỗ {tien_vn(-lai_sale)} đ: đừng giảm sâu như cũ."
            elif lai_thuong > 0 and lai_sale < 0.5 * lai_thuong:
                nen += f" Ngày sale khách đông nhưng lãi mỗi cái chỉ còn {tien_vn(lam_tron(100.0 * lai_sale / lai_thuong, 0))}% so với ngày thường: giảm ít lại."
        kq.append(dict(MaSP=m, TenSP=v.TenSP, TonHienTai=v.TonHienTai, ADS=lam_tron(v.ADS, 2), ADS30=lam_tron(v.ADS30, 2), XuHuongPT=v.XuHuongPT,
                       Cau=v.Cau, SoNgayConLai=v.SoNgayConLai, LeadTime=lam_tron(v.LeadTime, 2), TrangThai=v.TrangThai,
                       DuDoan30Ngay=v.DuDoan30Ngay, SoNenNhap=v.SoNenNhap, MonHayMuaChung=v.MonHayMuaChung,
                       GiaBan=b["SanPham"].set_index("MaSP")["GiaBan"].get(m), GiaNhapTB=lam_tron(gn, 2) if gn is not None else None,
                       GiaThucBan30=lam_tron(p30, 2) if p30 is not None else None, GiaThucBanTruoc=lam_tron(ptr, 2) if ptr is not None else None,
                       GiaTBNgayThuong=lam_tron(gt_, 2) if gt_ is not None else None, GiaTBNgaySale=lam_tron(gs_, 2) if gs_ is not None else None,
                       GiaDoiPT=gia_doi, TienNamTrongKho=tien_kho,
                       LaiMoiCaiNgayThuong=lam_tron(lai_thuong, 2) if lai_thuong is not None else None,
                       LaiMoiCaiNgaySale=lam_tron(lai_sale, 2) if lai_sale is not None else None,
                       CungCau=cc, GiaiDoan=gd, ThuTienVe=thu, GiaXuHuong=gxh, NenLamGi=nen))
    return pd.DataFrame(kq)


def cung_cau_theo_thang(b):
    dh, ct = b["DonHang"], b["ChiTietDonHang"]
    thang = sorted(dh["NgayDat"].dt.to_period("M").unique())
    d = ct.merge(dh[["MaDH", "NgayDat", "TrangThai"]], on="MaDH")
    d = d[~d["TrangThai"].fillna("").str.contains("hủy")].copy()
    d["Thang"] = d["NgayDat"].dt.to_period("M")
    d["Tien"] = d["SoLuong"] * d["DonGia"]
    cau = d.groupby(["MaSP", "Thang"]).agg(SL=("SoLuong", "sum"), T=("Tien", "sum"))
    bd = []
    pn, px = b["PhieuNhap"], b["PhieuXuat"]
    for r in b["ChiTietPhieuNhap"].merge(pn[["MaPN", "NgayNhap"]], on="MaPN").itertuples():
        bd.append((r.MaSP, r.NgayNhap, r.SoLuong, 1))
    for r in b["ChiTietPhieuXuat"].merge(px[["MaPX", "NgayXuat"]], on="MaPX").itertuples():
        bd.append((r.MaSP, r.NgayXuat, -r.SoLuong, 0))
    th = b["TraHang"]
    for r in th[th["TinhTrangHang"] == "Còn bán được"].itertuples():
        bd.append((r.MaSP, r.NgayTra, r.SoLuong, 0))
    hh = b["HangHoanVeKho"]
    for r in hh[hh["TrangThai"] == "Đã nhận về kho"].itertuples():
        bd.append((r.MaSP, r.NgayNhanVe, r.SoLuong, 0))
    bd = pd.DataFrame(bd, columns=["MaSP", "Ngay", "SL", "LaNhap"])
    kq = []
    for sp in b["SanPham"].itertuples():
        b_sp = bd[bd["MaSP"] == sp.MaSP]
        for t in thang:
            dau = t.start_time
            cuoi = (t + 1).start_time
            c = int(cau.loc[(sp.MaSP, t), "SL"]) if (sp.MaSP, t) in cau.index else 0
            tien = float(cau.loc[(sp.MaSP, t), "T"]) if (sp.MaSP, t) in cau.index else 0.0
            ton_dau = int(b_sp[b_sp["Ngay"] < dau]["SL"].sum())
            nhap = int(b_sp[(b_sp["LaNhap"] == 1) & (b_sp["Ngay"] >= dau) & (b_sp["Ngay"] < cuoi)]["SL"].sum())
            cung = ton_dau + nhap
            if c == 0 and cung == 0:
                nhan = "Không hàng, không khách"
            elif c == 0:
                nhan = "Không ai mua"
            elif cung / c < 1.5:
                nhan = "Hàng mỏng: đủ bán dưới 1,5 tháng"
            elif cung / c <= 3:
                nhan = "Vừa đủ: đủ bán 1,5 - 3 tháng"
            else:
                nhan = "Hàng dư: đủ bán trên 3 tháng"
            kq.append(dict(MaSP=sp.MaSP, TenSP=sp.TenSP, Thang=str(t), Cau=c, TonDauThang=ton_dau, NhapTrongThang=nhap, Cung=cung,
                           GiaThucBanTB=lam_tron(tien / c, 2) if c > 0 else None, DuBanMayThang=lam_tron(cung / c, 1) if c > 0 else None,
                           CungCau=nhan))
    return pd.DataFrame(kq)


# ---------------------------------------------------------------- sp_TimMonHayMuaChung

def mua_chung(b, support=0.01, confidence=0.30, lift=1.0):
    ct = b["ChiTietDonHang"]
    tong_don = ct["MaDH"].nunique()
    dem_sp = ct.groupby("MaSP").size()
    cap = ct[["MaDH", "MaSP"]].merge(ct[["MaDH", "MaSP"]], on="MaDH")
    cap = cap[cap["MaSP_x"] != cap["MaSP_y"]].groupby(["MaSP_x", "MaSP_y"]).size().reset_index(name="SoDon")
    ten = b["SanPham"].set_index("MaSP")["TenSP"]
    kq = []
    for r in cap.itertuples():
        s = r.SoDon / tong_don
        c = r.SoDon / dem_sp[r.MaSP_x]
        l = c / (dem_sp[r.MaSP_y] / tong_don)
        kq.append(dict(MaSPChinh=r.MaSP_x, TenSPChinh=ten.get(r.MaSP_x), MaSPKem=r.MaSP_y, TenSPKem=ten.get(r.MaSP_y),
                       SoDonCoCaHai=int(r.SoDon), PhanTramTongDon=lam_tron(100 * s, 1), MuaAThiMuaKemB=lam_tron(100 * c, 2),
                       GapMayLanBinhThuong=lam_tron(l, 2),
                       NenLamCombo="Có" if (s >= support and c >= confidence and l >= lift) else "Không"))
    kq = pd.DataFrame(kq)
    return kq.sort_values(["NenLamCombo", "GapMayLanBinhThuong", "MuaAThiMuaKemB"], ascending=[True, False, False]).reset_index(drop=True)


# ---------------------------------------------------------------- sp_ChiaNhomKhachHang

TEN_HANG = {5: "Khách ruột", 4: "Khách thân", 3: "Khách thường", 2: "Khách ít ghé", 1: "Khách sắp mất"}
VIEC_THEO_HANG = {
    5: "Mua gần đây, mua nhiều, chi nhiều. Giữ chân: quà tri ân, cho dùng thử hàng mới trước, đừng spam giảm giá.",
    4: "Mua khá đều. Mời vào nhóm khách thân, báo sớm đợt sale, gợi ý món hay mua kèm để tăng giá trị đơn.",
    3: "Mua vừa phải. Nhắn tin khi có hàng mới cùng loại khách hay mua, tặng mã giảm nhỏ cho đơn tiếp theo.",
    2: "Lâu lâu mới mua. Gửi mã giảm giá có hạn ngắn để kéo quay lại, hỏi xem lần trước có gì chưa hài lòng.",
    1: "Lâu rồi không quay lại, mua ít. Gọi hỏi thăm hoặc gửi ưu đãi lớn một lần; nếu vẫn không mua thì thôi, đừng tốn tiền quảng cáo.",
}


def chia_nhom_khach(b, ngay_tinh):
    dh = b["DonHang"]
    d = dh[dh["MaKH"].notna() & ~dh["TrangThai"].fillna("").str.contains("hủy")]
    g = d.groupby("MaKH").agg(Cuoi=("NgayDat", "max"), F=("MaDH", "count"), M=("TongTien", "sum")).reset_index()
    g["R"] = (pd.Timestamp(ngay_tinh).normalize() - g["Cuoi"].dt.normalize()).dt.days
    n = len(g)
    g = g.sort_values(["R", "MaKH"], ascending=[False, True]); g["DiemR"] = ntile(n, 5)
    g = g.sort_values(["F", "MaKH"], ascending=[True, True]); g["DiemF"] = ntile(n, 5)
    g = g.sort_values(["M", "MaKH"], ascending=[True, True]); g["DiemM"] = ntile(n, 5)
    g["Tong"] = g["DiemR"] + g["DiemF"] + g["DiemM"]
    g = g.sort_values(["Tong", "MaKH"], ascending=[True, True]); g["Hang"] = ntile(n, 5)
    return g


# ---------------------------------------------------------------- sp_BangThongBao_CamXuc

def bang_cam_xuc(b, th, cx, csat_do=60, csat_vang=80, min_dg=10):
    dg = b["DanhGiaSanPham"].merge(cx[cx["Loai"] == "Đánh giá"][["Ma", "NhomCamXuc"]], left_on="MaDanhGia", right_on="Ma", how="left")
    g = dg.groupby("MaSP").agg(SoDanhGia=("MaDanhGia", "count"), SaoTB=("SoSao", "mean"),
                               SoTot=("SoSao", lambda s: int((s >= 4).sum())), SoChe=("NhomCamXuc", lambda s: int((s == "Chê").sum())))
    xuat = b["ChiTietPhieuXuat"].groupby("MaSP")["SoLuong"].sum()
    hoan = b["HangHoanVeKho"].groupby("MaSP")["SoLuong"].sum()
    tra = th.groupby("MaSP")["SoLuong"].sum()
    top = (th.groupby(["MaSP", "NhomLyDoChinh"]).agg(N=("MaTraHang", "count"), Min=("MaTraHang", "min")).reset_index()
           .sort_values(["MaSP", "N", "Min"], ascending=[True, False, True]).drop_duplicates("MaSP").set_index("MaSP")["NhomLyDoChinh"])
    kq = []
    for sp in b["SanPham"].itertuples():
        m = sp.MaSP
        so_dg = int(g.loc[m, "SoDanhGia"]) if m in g.index else 0
        sao = float(g.loc[m, "SaoTB"]) if so_dg else None
        tot = int(g.loc[m, "SoTot"]) if so_dg else 0
        che = int(g.loc[m, "SoChe"]) if so_dg else 0
        sl_giao = float(xuat.get(m, 0)) - float(hoan.get(m, 0))
        sl_tra = float(tra.get(m, 0))
        csat = 100.0 * tot / so_dg if so_dg else None
        chu_che = 100.0 * che / so_dg if so_dg else None
        ty_le_tra = 100.0 * sl_tra / sl_giao if sl_giao else None
        tt = "CHƯA CÓ ĐÁNH GIÁ" if not so_dg else ("ĐỎ" if csat < csat_do else "VÀNG" if csat < csat_vang else "XANH")
        ly_do = top.get(m)
        if not so_dg:
            tb = "Chưa có đánh giá nào để đọc cảm xúc."
        else:
            dau = {"ĐỎ": "Khách đang KHÔNG hài lòng", "VÀNG": "Khách tạm hài lòng, cần theo dõi"}.get(tt, "Khách hài lòng")
            tb = (f"{dau}: {so_vn(csat, 0)}% đánh giá từ 4 sao trở lên, trung bình {so_vn(sao, 1)} sao ({so_dg} đánh giá). "
                  f"Lời chê chiếm {so_vn(chu_che, 0)}%.")
            if ty_le_tra is not None:
                tb += f" Trả hàng {so_vn(ty_le_tra, 1)}%" + (f", lý do nhiều nhất: {ly_do}." if ly_do else ".")
            if so_dg < min_dg:
                tb += f" (Mới {so_dg} đánh giá, ít quá nên chưa đáng tin.)"
        kq.append(dict(MaSP=m, SoDanhGia=so_dg, SaoTB=sao, CSAT=csat, TyLeChuChe=chu_che, SLGiao=sl_giao, SLTra=sl_tra,
                       TyLeTra=ty_le_tra, LyDoTraNhieuNhat=ly_do, TrangThai=tt, ThongBao=tb))
    return pd.DataFrame(kq).set_index("MaSP")


# ---------------------------------------------------------------- sp_BangThongBao_MuaTheoCamXuc (bảng 1)

def ngay_sale_set(b):
    s = set()
    for r in b["DotSale"].itertuples():
        d = r.NgayBatDau
        while d <= r.NgayKetThuc:
            s.add(d.normalize())
            d += timedelta(days=1)
    return s


def mua_theo_hung_sale(b, th, so_ngay=365, min_so_ban=30, nguong_keo=1.3, nguong_hung=2.0):
    dh = b["DonHang"]
    den = dh["NgayDat"].max().normalize()
    tu = den - timedelta(days=so_ngay - 1)
    sale = ngay_sale_set(b)
    ngay_sale = sum(1 for d in sale if tu <= d <= den)
    ngay_thuong = so_ngay - ngay_sale
    hop_le = dh[~dh["TrangThai"].fillna("").str.contains("hủy") & (dh["NgayDat"] >= tu) & (dh["NgayDat"] < den + timedelta(days=1))].copy()
    hop_le["LaSale"] = hop_le["NgayDat"].dt.normalize().isin(sale)
    ban = b["ChiTietDonHang"].merge(hop_le[["MaDH", "LaSale"]], on="MaDH")
    tra = th.merge(hop_le[["MaDH", "LaSale"]], on="MaDH")
    kq = []
    for sp in b["SanPham"].itertuples():
        m = sp.MaSP
        bs = int(ban[(ban["MaSP"] == m) & ban["LaSale"]]["SoLuong"].sum())
        bt = int(ban[(ban["MaSP"] == m) & ~ban["LaSale"]]["SoLuong"].sum())
        ts = int(tra[(tra["MaSP"] == m) & tra["LaSale"]]["SoLuong"].sum())
        tt_ = int(tra[(tra["MaSP"] == m) & ~tra["LaSale"]]["SoLuong"].sum())
        tb_s = bs / ngay_sale if ngay_sale else None
        tb_t = bt / ngay_thuong if ngay_thuong else None
        tra_s = 100.0 * ts / bs if bs else None
        tra_t = 100.0 * tt_ / bt if bt else None
        lift = tb_s / tb_t if (tb_s is not None and tb_t) else None
        if bs < min_so_ban:
            kieu = "Chưa đủ số liệu"
        elif lift is not None and lift >= nguong_hung:
            kieu = "MUA THEO HỨNG SALE"
        elif lift is not None and lift >= nguong_keo:
            kieu = "SALE CÓ KÉO THÊM ĐƠN"
        else:
            kieu = "ÍT BỊ SALE KÉO"
        if bs + bt == 0:
            tb = "Chưa bán được cái nào trong kỳ."
        else:
            tb = (f"Ngày sale bán TB {so_vn(tb_s, 1)} cái/ngày"
                  + ("" if lift is None else f", gấp {so_vn(lift, 1)} lần ngày thường ({so_vn(tb_t, 1)} cái/ngày)")
                  + ". Trả hàng: " + ("–" if tra_s is None else f"{so_vn(tra_s, 1)}%") + " ở ngày sale, "
                  + ("–" if tra_t is None else f"{so_vn(tra_t, 1)}%") + " ở ngày thường."
                  + (f" (Chỉ bán {bs} cái trong ngày sale, ít quá nên chưa đáng tin.)" if bs < min_so_ban else ""))
        kq.append(dict(MaSP=m, TenSP=sp.TenSP, BanNgaySale=bs, BanNgayThuong=bt, TraNgaySale=ts, TraNgayThuong=tt_,
                       BanMoiNgaySale=lam_tron(tb_s, 2), BanMoiNgayThuong=lam_tron(tb_t, 2), GapMayLan=lam_tron(lift, 2) if lift is not None else None,
                       PhanTramTraNgaySale=lam_tron(tra_s, 1) if tra_s is not None else None,
                       PhanTramTraNgayThuong=lam_tron(tra_t, 1) if tra_t is not None else None, KieuMua=kieu, ThongBao=tb))
    kq = pd.DataFrame(kq)
    kq["_r"] = kq["GapMayLan"].isna()
    return kq.sort_values(["_r", "GapMayLan"], ascending=[True, False]).drop(columns="_r").reset_index(drop=True)


# ---------------------------------------------------------------- sp_BangThongBao_LyDoMua

def ly_do_mua_tong_hop(b, ks, min_so_ban=30, he_so=1.96):
    dh = b["DonHang"].set_index("MaDH")
    co_tra = set(b["TraHang"]["MaDH"])
    d = ks.copy()
    d["TongTien"] = d["MaDH"].map(dh["TongTien"])
    d["CoTra"] = d["MaDH"].isin(co_tra).astype(int)
    d["DaHuy"] = d["MaDH"].map(dh["TrangThai"]).fillna("").str.contains("hủy")
    tong_cau, tong_tra = len(d), int(d["CoTra"].sum())
    doanh_thu_ks = float(d.loc[~d["DaHuy"], "TongTien"].sum())
    cach = b["CachBan_TheoLyDoMua"].set_index("NhomLyDo")
    kq = []
    for nhom, g in d.groupby("NhomLyDo"):
        n1, s1 = len(g), int(g["CoTra"].sum())
        n2 = tong_cau - n1
        p1 = s1 / n1
        p2 = (tong_tra - s1) / n2 if n2 else None
        ty_le = 100.0 * n1 / tong_cau
        hao = float(g["MucHaoHung"].mean())
        gia_tri = float(g["TongTien"].mean())
        chenh = p1 - p2 if p2 is not None else None
        dao = he_so * math.sqrt(p1 * (1 - p1) / n1 + p2 * (1 - p2) / n2) if n2 else None
        if n1 < min_so_ban or n2 < min_so_ban:
            kl = f"Chưa đủ số liệu để so sánh (cần mỗi bên từ {min_so_ban} trở lên)."
        elif chenh > dao:
            kl = f"Khác biệt có thật: nhóm này CAO hơn {so_vn(100 * chenh, 1)} điểm % so với các nhóm còn lại, vượt mức dao động ±{so_vn(100 * dao, 1)} điểm %."
        elif chenh < -dao:
            kl = f"Khác biệt có thật: nhóm này THẤP hơn {so_vn(100 * abs(chenh), 1)} điểm % so với các nhóm còn lại, vượt mức dao động ±{so_vn(100 * dao, 1)} điểm %."
        else:
            kl = (f"Chưa kết luận được: nhóm này {'cao' if chenh >= 0 else 'thấp'} hơn {so_vn(100 * abs(chenh), 1)} điểm % so với các nhóm còn lại "
                  f"nhưng chỉ ngang mức dao động ±{so_vn(100 * dao, 1)} điểm % do ít mẫu.")
        dt_nhom = float(g.loc[~g["DaHuy"], "TongTien"].sum())
        tb = (f"{nhom}: {n1} câu trả lời ({so_vn(ty_le, 1)}%), hào hứng TB {so_vn(hao, 1)}/5, giá trị đơn TB {so_vn(gia_tri / 1000000.0, 1)} triệu. "
              f"Có trả hàng ở {so_vn(100 * p1, 1)}% đơn (các nhóm còn lại: {so_vn(100 * p2, 1) if p2 is not None else '–'}%). {kl}")
        kq.append(dict(NhomLyDoMua=nhom, KieuLyDoMua=cach["KieuLyDo"].get(nhom, "Không rõ"), SoCauTraLoi=n1, PhanTramCauTraLoi=lam_tron(ty_le, 1),
                       HaoHungTB=lam_tron(hao, 2), GiaTriDonTB=lam_tron(gia_tri, 2), DoanhThuCacDon=dt_nhom,
                       PhanTramDoanhThu=lam_tron(100 * dt_nhom / doanh_thu_ks, 1) if doanh_thu_ks else None,
                       PhanTramDonCoTra=lam_tron(100 * p1, 1), PhanTramDonCoTra_NhomKhac=lam_tron(100 * p2, 1) if p2 is not None else None,
                       KetLuan=kl, CachBanHop=cach["CachBanHop"].get(nhom, ""), ThongBao=tb, _ThuTu=int(g["ThuTuNhom"].min())))
    kq = pd.DataFrame(kq).sort_values(["SoCauTraLoi", "_ThuTu"], ascending=[False, True]).drop(columns="_ThuTu").reset_index(drop=True)
    return kq, doanh_thu_ks


# ---------------------------------------------------------------- lọc dữ liệu thô từ sàn

def chuan_hoa_sdt(s):
    if s is None or (isinstance(s, float) and math.isnan(s)):
        return None
    d = re.sub(r"[^0-9]", "", str(s))
    if len(d) == 11 and d.startswith("84"):
        d = "0" + d[2:]
    if len(d) == 10 and d[0] == "0" and d[-8:] != d[-1] * 8:
        return d
    return None


def viet_hoa_tu(s):
    return " ".join(w[:1].upper() + w[1:].lower() for w in s.split(" "))


def chuan_hoa_ten(s):
    if s is None or (isinstance(s, float) and math.isnan(s)):
        return None
    s = re.sub(r"\s+", " ", str(s)).strip()
    return viet_hoa_tu(s) if (s == s.upper() or s == s.lower()) else s


def chuan_hoa_dia_chi(s):
    if s is None or (isinstance(s, float) and math.isnan(s)):
        return None
    s = re.sub(r"\s+", " ", str(s)).strip().rstrip(".").strip()
    s = re.sub(r"\s*,\s*", ", ", s)
    phan = [p.strip() for p in s.split(",") if p.strip()]
    kq = []
    for p in phan:
        thuong = p.lower()
        if re.fullmatch(r"(tp\.?\s*hcm|tp\.?\s*hồ chí minh|thành phố hồ chí minh|hcm|hồ chí minh)", thuong):
            kq.append("TP.HCM")
            continue
        m = re.fullmatch(r"tp\.?\s*(.+)", thuong)
        if m:
            kq.append("TP." + viet_hoa_tu(m.group(1)))
            continue
        m = re.fullmatch(r"(quận|q\.|q(?=\d))\s*(.+)", thuong)
        if m:
            kq.append("Quận " + viet_hoa_tu(m.group(2)))
            continue
        m = re.fullmatch(r"(phường|p\.|p(?=\d))\s*(.+)", thuong)
        if m:
            kq.append("Phường " + viet_hoa_tu(m.group(2)))
            continue
        kq.append(viet_hoa_tu(p) if (p == p.upper() or p == p.lower()) else p)
    return ", ".join(kq)


def loc_du_lieu(b):
    nhat_ky = []

    def ghi(bang, kiem_tra, mask_hoac_so, xu_ly, vi_du=None):
        so_dong = int(mask_hoac_so.sum()) if hasattr(mask_hoac_so, "sum") else int(mask_hoac_so)
        nhat_ky.append(dict(Bang=bang, KiemTra=kiem_tra, SoDong=so_dong, XuLy=xu_ly, ViDu=vi_du if so_dong else None))

    dh, ct = b["DonHang"], b["ChiTietDonHang"]
    khong_mon = ~dh["MaDH"].isin(ct["MaDH"])
    ghi("DonHang", "Đơn không có món hàng nào", khong_mon, "Bỏ khỏi phân tích bán hàng", ", ".join(dh.loc[khong_mon, "MaDH"].head(3)))
    sai = ct["SoLuong"].isna() | (ct["SoLuong"] <= 0) | ct["DonGia"].isna() | (ct["DonGia"] <= 0)
    ghi("ChiTietDonHang", "Món có số lượng hoặc đơn giá không hợp lệ (trống, bằng 0, âm)", sai, "Bỏ món đó")
    px = b["PhieuXuat"]
    ghi("PhieuXuat", "Phiếu xuất không gắn đơn hàng", px["MaDH"].isna(), "Giữ lại, đưa vào màn Kho xuất bất thường để kiểm",
        ", ".join(px.loc[px["MaDH"].isna(), "MaPX"].head(4)))
    kh = b["KhachHang"]
    sdt_sai = kh["SoDT"].map(chuan_hoa_sdt).isna()
    ghi("KhachHang", "Số điện thoại sai hoặc số giả (8 số cuối giống nhau)", sdt_sai, "Không dùng số này để tìm khách khi lập đơn",
        ", ".join(kh.loc[sdt_sai, "MaKH"].head(5)))
    dc = kh["DiaChi"].fillna("").str.strip()
    dc_sai = (dc.str.len() < 10) | (dc == "TP.HCM")
    ghi("KhachHang", "Địa chỉ trống hoặc quá ngắn", dc_sai, "Lấy địa chỉ giao của đơn gần nhất (thủ tục sp_PhatHienVaSua_DuLieuThieu)",
        ", ".join(kh.loc[dc_sai, "MaKH"].head(5)))

    tk = b["DuLieuTho_KhachHang"].copy()
    tk["TenKH_DaSua"] = tk["TenKH"].map(chuan_hoa_ten)
    tk["SoDT_DaSua"] = tk["SoDT"].map(chuan_hoa_sdt)
    tk["DiaChi_DaSua"] = tk["DiaChi"].map(chuan_hoa_dia_chi)
    ghi("DuLieuTho_KhachHang", "Tên viết hoa hết / thường hết / thừa khoảng trắng", tk["TenKH"].fillna("") != tk["TenKH_DaSua"].fillna(""),
        "Viết hoa chữ cái đầu mỗi chữ, bỏ khoảng trắng thừa")
    ghi("DuLieuTho_KhachHang", "Số điện thoại có dấu chấm, gạch, khoảng trắng hoặc +84", tk["SoDT_DaSua"].notna() & (tk["SoDT"].astype(str) != tk["SoDT_DaSua"]),
        "Đưa về 10 số bắt đầu bằng 0")
    ghi("DuLieuTho_KhachHang", "Số điện thoại sai không sửa được", tk["SoDT_DaSua"].isna(), "Đánh dấu cần gọi xác nhận",
        ", ".join(tk.loc[tk["SoDT_DaSua"].isna(), "SoDT"].fillna("(trống)").astype(str).head(3)))
    ghi("DuLieuTho_KhachHang", "Địa chỉ viết tắt / viết hoa lộn xộn (Q.3, TPHCM, Thành phố Hồ Chí Minh...)",
        tk["DiaChi"].fillna("") != tk["DiaChi_DaSua"].fillna(""), "Viết đầy đủ: Quận 3, TP.HCM")
    tk["CanGoiXacNhan"] = (tk["SoDT_DaSua"].isna() | (tk["DiaChi_DaSua"].fillna("").str.len() < 10)).map({True: "Có", False: "Không"})
    co_sdt = tk[tk["SoDT_DaSua"].notna()].sort_values(["SoDT_DaSua", "NgayTao", "Id"], ascending=[True, False, False])
    trung = co_sdt.duplicated("SoDT_DaSua")
    ghi("DuLieuTho_KhachHang", "Một số điện thoại xuất hiện nhiều lần (nhiều nguồn, nhiều lần nhập)", trung, "Giữ dòng nhập mới nhất")
    kh_loc = pd.concat([co_sdt[~trung], tk[tk["SoDT_DaSua"].isna()]]).sort_values("Id")
    kh_loc = kh_loc[["Id", "TenKH_DaSua", "SoDT_DaSua", "DiaChi_DaSua", "CanGoiXacNhan", "NguonDuLieu", "TenKH", "SoDT", "DiaChi"]]

    td = b["DuLieuTho_DonHang"].copy()
    td = td.sort_values(["MaDonSan", "NgayTao", "Id"], ascending=[True, False, False])
    trung_don = td.duplicated("MaDonSan")
    ghi("DuLieuTho_DonHang", "Một mã đơn sàn có nhiều dòng (sàn xuất file nhiều lần, trạng thái đổi dần)", trung_don,
        "Giữ dòng có NgayTao mới nhất (trạng thái mới nhất)", ", ".join(td.loc[trung_don, "MaDonSan"].head(3)))
    td = td[~trung_don].copy()
    thieu = td["NgayDat"].isna() | td["TongTien"].isna() | (td["TongTien"] <= 0)
    ghi("DuLieuTho_DonHang", "Đơn thiếu ngày đặt hoặc tổng tiền", thieu, "Bỏ đơn đó")
    td = td[~thieu].copy()
    td["TenKH_DaSua"] = td["TenKH"].map(chuan_hoa_ten)
    td["SoDT_DaSua"] = td["SoDT"].map(chuan_hoa_sdt)
    td["DiaChi_DaSua"] = td["DiaChi"].map(chuan_hoa_dia_chi)
    td["TrangThai"] = td["TrangThai"].replace({"Đang giao hàng": "Đang giao", "Hoàn thành": "Đã giao", "Đã hoàn thành": "Đã giao", "Hủy": "Đã hủy"})
    td["LaDonHuy"] = td["TrangThai"].fillna("").str.contains("hủy").map({True: "Có", False: "Không"})
    don_loc = td.sort_values("NgayDat")[["MaDonSan", "NgayDat", "TenKH_DaSua", "SoDT_DaSua", "DiaChi_DaSua", "TongTien", "TrangThai", "LaDonHuy", "NguonDuLieu"]]
    return pd.DataFrame(nhat_ky), kh_loc, don_loc


# ---------------------------------------------------------------- dựng các trang tính

def tao_bang_ngay(b, ban_hang):
    dh = b["DonHang"]
    tu, den = dh["NgayDat"].min().normalize(), dh["NgayDat"].max().normalize()
    ngay = pd.DataFrame({"Ngay": pd.date_range(tu, den, freq="D")})
    ngay["Nam"] = ngay["Ngay"].dt.year
    ngay["Thang"] = ngay["Ngay"].dt.month
    ngay["ThangNam"] = ngay["Ngay"].dt.strftime("%Y-%m")
    ngay["ThuSo"] = ngay["Ngay"].dt.dayofweek + 1
    ngay["Thu"] = ngay["Ngay"].dt.dayofweek.map(lambda x: THU[x])
    ngay["LaCuoiTuan"] = (ngay["Ngay"].dt.dayofweek >= 5).map({True: "Có", False: "Không"})
    ngay["TenDotSale"] = None
    for d in b["DotSale"].sort_values("NgayBatDau").itertuples():
        ngay.loc[(ngay["Ngay"] >= d.NgayBatDau) & (ngay["Ngay"] <= d.NgayKetThuc), "TenDotSale"] = d.TenDot
    ngay["LaNgaySale"] = ngay["TenDotSale"].notna().map({True: "Có", False: "Không"})
    le = b["NgayLe"].set_index("Ngay")["TenNgayLe"]
    ngay["TenNgayLe"] = ngay["Ngay"].map(le)
    ngay["LaNgayLe"] = ngay["TenNgayLe"].notna().map({True: "Có", False: "Không"})
    d = dh.assign(Ngay=dh["NgayDat"].dt.normalize(), Huy=dh["TrangThai"].fillna("").str.contains("hủy"))
    ngay["SoDon"] = ngay["Ngay"].map(d[~d["Huy"]].groupby("Ngay").size()).fillna(0).astype(int)
    ngay["SoDonHuy"] = ngay["Ngay"].map(d[d["Huy"]].groupby("Ngay").size()).fillna(0).astype(int)
    ngay["DoanhThu"] = ngay["Ngay"].map(ban_hang.groupby("Ngay")["DoanhThu"].sum()).fillna(0)
    return ngay


def tao_ban_hang(b, ks_nhom):
    dh, ct, sp = b["DonHang"], b["ChiTietDonHang"], b["SanPham"]
    bh = ct.merge(dh[["MaDH", "NgayDat", "TrangThai", "MaKH", "MaDoiTac", "HinhThucNhan"]], on="MaDH")
    bh = bh.merge(sp[["MaSP", "TenSP", "MaDanhMuc", "GiaBan"]], on="MaSP", how="left").merge(b["DanhMuc"], on="MaDanhMuc", how="left")
    bh = bh.merge(b["KhachHang"][["MaKH", "TenKH"]], on="MaKH", how="left")
    bh = bh.merge(b["DoiTac"][["MaDoiTac", "TenDoiTac"]].rename(columns={"TenDoiTac": "KenhBan"}), on="MaDoiTac", how="left")
    sale = {}
    for d in b["DotSale"].sort_values("NgayBatDau").itertuples():
        x = d.NgayBatDau
        while x <= d.NgayKetThuc:
            sale[x.normalize()] = d.TenDot
            x += timedelta(days=1)
    bh["Ngay"] = bh["NgayDat"].dt.normalize()
    bh["ThangNam"] = bh["NgayDat"].dt.strftime("%Y-%m")
    bh["Thu"] = bh["NgayDat"].dt.dayofweek.map(lambda x: THU[x])
    bh["Gio"] = bh["NgayDat"].dt.hour
    bh["TenDotSale"] = bh["Ngay"].map(sale)
    bh["LaNgaySale"] = bh["TenDotSale"].notna().map({True: "Có", False: "Không"})
    huy = bh["TrangThai"].fillna("").str.contains("hủy")
    bh["LaDonHuy"] = huy.map({True: "Có", False: "Không"})
    bh["PhanTramGiam"] = ((1 - bh["DonGia"] / bh["GiaBan"]) * 100).map(lambda x: lam_tron(x, 1))
    bh["ThanhTien"] = bh["SoLuong"] * bh["DonGia"]
    bh["DoanhThu"] = bh["ThanhTien"].where(~huy, 0)
    bh["GiaNhapTB"] = bh["GiaVon"]
    bh["TienVon"] = (bh["SoLuong"] * bh["GiaVon"]).where(~huy, 0)
    bh["LaiGop"] = bh["DoanhThu"] - bh["TienVon"]
    tra = b["TraHang"].groupby(["MaDH", "MaSP"]).agg(SoLuongTra=("SoLuong", "sum"), TienHoan=("SoTienHoan", "sum")).reset_index()
    bh = bh.merge(tra, on=["MaDH", "MaSP"], how="left")
    bh[["SoLuongTra", "TienHoan"]] = bh[["SoLuongTra", "TienHoan"]].fillna(0)
    bh = bh.merge(ks_nhom[["MaDH", "NhomLyDoMua", "KieuLyDoMua"]], on="MaDH", how="left")
    bh["NhomLyDoMua"] = bh["NhomLyDoMua"].fillna("Chưa khảo sát")
    bh["KieuLyDoMua"] = bh["KieuLyDoMua"].fillna("Chưa khảo sát")
    cot = ["MaDH", "NgayDat", "Ngay", "ThangNam", "Thu", "Gio", "TrangThai", "LaDonHuy", "KenhBan", "HinhThucNhan", "LaNgaySale", "TenDotSale",
           "MaKH", "TenKH", "MaSP", "TenSP", "MaDanhMuc", "TenDanhMuc", "SoLuong", "GiaBan", "DonGia", "PhanTramGiam", "ThanhTien", "DoanhThu",
           "GiaNhapTB", "TienVon", "LaiGop", "SoLuongTra", "TienHoan", "NhomLyDoMua", "KieuLyDoMua"]
    return bh[cot].sort_values(["NgayDat", "MaDH", "MaSP"]).reset_index(drop=True)


def tao_gio_vang(bh):
    d = bh[bh["LaDonHuy"] == "Không"].copy()
    d["ThuSo"] = d["NgayDat"].dt.dayofweek + 1
    g = d.groupby(["ThuSo", "Thu", "Gio"]).agg(SoDon=("MaDH", "nunique"), DoanhThu=("DoanhThu", "sum")).reset_index()
    return g.sort_values(["ThuSo", "Gio"]).reset_index(drop=True)


def tao_bang_diem_san_pham(b, bh, db, bcx, mhs, ly_do_sp):
    sp = b["SanPham"].merge(b["DanhMuc"], on="MaDanhMuc", how="left")
    ok = bh[bh["LaDonHuy"] == "Không"]
    den = b["DonHang"]["NgayDat"].max().normalize()
    g = ok.groupby("MaSP").agg(SoLuongBan=("SoLuong", "sum"), SoDon=("MaDH", "nunique"), DoanhThu=("DoanhThu", "sum"), LaiGop=("LaiGop", "sum"))
    g90 = ok[ok["Ngay"] > den - timedelta(days=90)].groupby("MaSP")["DoanhThu"].sum()
    don_co = bh.groupby("MaSP")["MaDH"].nunique()
    don_huy = bh[bh["LaDonHuy"] == "Có"].groupby("MaSP")["MaDH"].nunique()
    db = db.set_index("MaSP")
    mhs = mhs.set_index("MaSP")
    kq = []
    for r in sp.itertuples():
        m = r.MaSP
        dt = float(g.loc[m, "DoanhThu"]) if m in g.index else 0.0
        lai = float(g.loc[m, "LaiGop"]) if m in g.index else 0.0
        c = bcx.loc[m]
        d = db.loc[m]
        top_ly_do = ly_do_sp[ly_do_sp["MaSP"] == m].sort_values(["SoDon", "NhomLyDoMua"], ascending=[False, True])
        top_ly_do = top_ly_do[top_ly_do["NhomLyDoMua"] != "Khác / không rõ"]
        kq.append(dict(MaSP=m, TenSP=r.TenSP, TenDanhMuc=r.TenDanhMuc, GiaBan=r.GiaBan,
                       SoLuongBan=int(g.loc[m, "SoLuongBan"]) if m in g.index else 0, SoDon=int(g.loc[m, "SoDon"]) if m in g.index else 0,
                       DoanhThu=dt, DoanhThu_90Ngay=float(g90.get(m, 0)), LaiGop=lam_tron(lai, 2),
                       PhanTramLai=lam_tron(100 * lai / dt, 1) if dt else None,
                       PhanTramDonHuy=lam_tron(100 * don_huy.get(m, 0) / don_co.get(m, 1), 1),
                       SoDanhGia=int(c.SoDanhGia), SaoTB=lam_tron(c.SaoTB, 2) if c.SaoTB is not None and not pd.isna(c.SaoTB) else None,
                       PhanTramKhachHaiLong=lam_tron(c.CSAT, 1) if c.CSAT is not None and not pd.isna(c.CSAT) else None, CamXuc=c.TrangThai,
                       PhanTramTraHang=lam_tron(c.TyLeTra, 1) if c.TyLeTra is not None and not pd.isna(c.TyLeTra) else None,
                       LyDoTraNhieuNhat=c.LyDoTraNhieuNhat,
                       TonHienTai=int(d.TonHienTai), BanMoiNgay=lam_tron(d.ADS, 2), XuHuongBan=d.Cau, DuDoanBan30NgayToi=int(d.DuDoan30Ngay),
                       DuBanMayNgay=d.SoNgayConLai, MucPhaiDatHang=int(d.ROP), NenNhapThem=int(d.SoNenNhap), TinhTrangKho=d.TrangThai,
                       NgaySaleBanGapMayLan=mhs.loc[m, "GapMayLan"], KieuMuaNgaySale=mhs.loc[m, "KieuMua"],
                       LyDoMuaNhieuNhat=top_ly_do["NhomLyDoMua"].iloc[0] if len(top_ly_do) else None, HayMuaChungVoi=d.MonHayMuaChung))
    kq = pd.DataFrame(kq).sort_values("DoanhThu", ascending=False).reset_index(drop=True)
    tong = kq["DoanhThu"].sum()
    kq["PhanTramDoanhThuCongDon"] = (100 * kq["DoanhThu"].cumsum() / tong).map(lambda x: lam_tron(x, 1))
    truoc = (kq["PhanTramDoanhThuCongDon"].shift(1).fillna(0))
    kq["MonChuLuc"] = (truoc < 80).map({True: "Có", False: "Không"})
    cach = b["CachBan_TheoLyDoMua"].set_index("NhomLyDo")["CachBanHop"]
    viec = []
    for r in kq.itertuples():
        v = []
        if r.TinhTrangKho in ("KHẨN CẤP", "ĐẶT HÀNG NGAY"):
            v.append(f"{r.TinhTrangKho}: nhập {r.NenNhapThem} cái.")
        if r.CamXuc == "ĐỎ":
            v.append(f"Khách không hài lòng (chỉ {so_vn(r.PhanTramKhachHaiLong, 0)}% từ 4 sao): đọc lời chê, sửa trước khi đẩy bán.")
        elif r.CamXuc == "VÀNG" and r.MonChuLuc == "Có":
            v.append(f"Món chủ lực nhưng mới {so_vn(r.PhanTramKhachHaiLong, 0)}% khách hài lòng: đọc lời chê để sửa sớm.")
        if r.PhanTramTraHang is not None and not pd.isna(r.PhanTramTraHang) and r.PhanTramTraHang >= 8 and r.LyDoTraNhieuNhat:
            v.append(f"Trả hàng {so_vn(r.PhanTramTraHang, 1)}%, nhiều nhất vì \"{r.LyDoTraNhieuNhat}\".")
        if r.PhanTramLai is not None and not pd.isna(r.PhanTramLai) and r.PhanTramLai < 10:
            v.append(f"Lãi gộp chỉ {so_vn(r.PhanTramLai, 1)}% (chưa trừ phí sàn): xem lại giá nhập hoặc giảm bớt khuyến mãi.")
        if r.XuHuongBan == "ĐANG HOT - bán nhanh lên":
            v.append("Đang bán nhanh lên: giữ hàng đủ, đẩy quảng cáo" + (f", gợi ý mua kèm \"{r.HayMuaChungVoi}\"." if r.HayMuaChungVoi else "."))
        if r.LyDoMuaNhieuNhat:
            v.append(f"Khách mua món này chủ yếu vì \"{r.LyDoMuaNhieuNhat}\": {cach.get(r.LyDoMuaNhieuNhat, '')}")
        viec.append(" | ".join(v) if v else "Ổn, giữ như hiện tại.")
    kq["ViecNenLam"] = viec
    cot = ["MaSP", "TenSP", "TenDanhMuc", "GiaBan", "SoLuongBan", "SoDon", "DoanhThu", "DoanhThu_90Ngay", "LaiGop", "PhanTramLai",
           "PhanTramDoanhThuCongDon", "MonChuLuc", "PhanTramDonHuy", "SoDanhGia", "SaoTB", "PhanTramKhachHaiLong", "CamXuc", "PhanTramTraHang",
           "LyDoTraNhieuNhat", "TonHienTai", "BanMoiNgay", "XuHuongBan", "DuDoanBan30NgayToi", "DuBanMayNgay", "MucPhaiDatHang", "NenNhapThem",
           "TinhTrangKho", "NgaySaleBanGapMayLan", "KieuMuaNgaySale", "LyDoMuaNhieuNhat", "HayMuaChungVoi", "ViecNenLam"]
    return kq[cot]


def tao_khach_hang_nhom(b, rfm, bh, ks_nhom, ngay_tinh):
    kh = b["KhachHang"]
    rfm = rfm.set_index("MaKH")
    ok = bh[bh["LaDonHuy"] == "Không"]
    loai = ok.groupby(["MaKH", "TenDanhMuc"])["DoanhThu"].sum().reset_index().sort_values(["MaKH", "DoanhThu", "TenDanhMuc"], ascending=[True, False, True])
    loai = loai.drop_duplicates("MaKH").set_index("MaKH")["TenDanhMuc"]
    ld = ks_nhom[ks_nhom["NhomLyDoMua"] != "Khác / không rõ"].groupby(["MaKH", "NhomLyDoMua"]).size().reset_index(name="N")
    ld = ld.sort_values(["MaKH", "N", "NhomLyDoMua"], ascending=[True, False, True]).drop_duplicates("MaKH").set_index("MaKH")["NhomLyDoMua"]
    sao = b["DanhGiaSanPham"].groupby("MaKH")["SoSao"].mean()
    tra = b["TraHang"].merge(b["DonHang"][["MaDH", "MaKH"]], on="MaDH").groupby("MaKH").size()
    huy = b["DonHang"][b["DonHang"]["TrangThai"] == TRANG_THAI_HUY].groupby("MaKH").size()
    kq = []
    for r in kh.itertuples():
        m = r.MaKH
        co = m in rfm.index
        hang = int(rfm.loc[m, "Hang"]) if co else None
        kq.append(dict(MaKH=m, TenKH=r.TenKH, SoDT_DaSua=chuan_hoa_sdt(r.SoDT),
                       NhomKhach=f"Hạng {hang} - {TEN_HANG[hang]}" if co else "Chưa mua đơn nào thành công",
                       Hang=hang, SoNgayChuaQuayLai=int(rfm.loc[m, "R"]) if co else None, SoDon=int(rfm.loc[m, "F"]) if co else 0,
                       TongTienMua=float(rfm.loc[m, "M"]) if co else 0.0, LanMuaCuoi=rfm.loc[m, "Cuoi"] if co else pd.NaT,
                       DiemGanDay=int(rfm.loc[m, "DiemR"]) if co else None, DiemSoLan=int(rfm.loc[m, "DiemF"]) if co else None,
                       DiemTien=int(rfm.loc[m, "DiemM"]) if co else None, HayMuaLoai=loai.get(m), LyDoMuaHayGap=ld.get(m),
                       SaoTBDaCham=lam_tron(sao.get(m), 1) if m in sao.index else None, SoLanTraHang=int(tra.get(m, 0)),
                       SoDonHuy=int(huy.get(m, 0)),
                       ViecNenLam=VIEC_THEO_HANG[hang] if co else "Chưa có đơn thành công: gọi xác nhận lại thông tin trước khi nhận đơn COD."))
    return pd.DataFrame(kq).sort_values(["Hang", "TongTienMua"], ascending=[False, False], na_position="last").reset_index(drop=True)


def tao_tu_khach_hay_noi(cx, b):
    ma_sp = dict(zip(b["DanhGiaSanPham"]["MaDanhGia"], b["DanhGiaSanPham"]["MaSP"]))
    ma_sp.update(dict(zip(b["TraHang"]["MaTraHang"], b["TraHang"]["MaSP"])))
    dem = defaultdict(Counter)
    for r in cx[cx["Loai"] == "Đánh giá"].itertuples():
        for t in r.TuKhen:
            dem[(t, "Khen")][ma_sp.get(r.Ma)] += 1
        for t in r.TuChe:
            dem[(t, "Chê")][ma_sp.get(r.Ma)] += 1
    kq = []
    for (tu, loai), c in dem.items():
        top = sorted(c.items(), key=lambda x: (-x[1], str(x[0])))[:3]
        kq.append(dict(TuKhachNoi=tu, Loai=loai, SoLan=sum(c.values()), NhieuNhatO=", ".join(f"{m} ({n})" for m, n in top)))
    return pd.DataFrame(kq).sort_values(["Loai", "SoLan", "TuKhachNoi"], ascending=[True, False, True]).reset_index(drop=True)


# ---------------------------------------------------------------- vw_LaiRongTheoThang, vw_DoiSoatTienSan, vw_CongNoNhaCungCap

def thang_cua(s):
    return s.dt.to_period("M").astype(str)


def tao_lai_rong_theo_thang(b, tien_don):
    gia_von = b["SanPham"].set_index("MaSP")["GiaVon"].fillna(0)
    don = tien_don[tien_don["TrangThai"] != TRANG_THAI_HUY].assign(Thang=thang_cua(tien_don["NgayDat"]))
    kq = don.groupby("Thang")[["TienHang", "GiamGia", "PhiSan", "TienHoanKhach", "TienShopNhan", "GiaVonHangBan", "LaiGop"]].sum()

    cp = b["ChiPhiVanHanh"].assign(Thang=thang_cua(b["ChiPhiVanHanh"]["Thang"]))
    chi_phi = cp.groupby("Thang")["SoTien"].sum()

    px, cx = b["PhieuXuat"], b["ChiTietPhieuXuat"]
    x = cx.merge(px[["MaPX", "MaDH", "NgayXuat"]], on="MaPX")
    mat = []
    for r in x[x["MaDH"].isna()].itertuples():
        mat.append((r.NgayXuat, r.SoLuong * gia_von.get(r.MaSP, 0)))
    da_dat = b["ChiTietDonHang"].groupby(["MaDH", "MaSP"])["SoLuong"].sum()
    g = x[x["MaDH"].notna()].groupby(["MaDH", "MaSP"]).agg(DaXuat=("SoLuong", "sum"), NgayXuat=("NgayXuat", "min"))
    for (m, s), r in g.iterrows():
        du = r["DaXuat"] - da_dat.get((m, s), 0)
        if du > 0:
            mat.append((r["NgayXuat"], du * gia_von.get(s, 0)))
    kk = b["PhieuKiemKe"]
    for r in b["ChiTietKiemKe"].merge(kk[kk["TrangThai"] == "Đã chốt"][["MaKK", "NgayChot"]], on="MaKK").itertuples():
        if r.ThucTe != r.SoSach:
            mat.append((r.NgayChot, -(r.ThucTe - r.SoSach) * gia_von.get(r.MaSP, 0)))
    mat = pd.DataFrame(mat, columns=["Ngay", "Tien"])
    hang_mat = mat.groupby(thang_cua(mat["Ngay"]))["Tien"].sum() if len(mat) else pd.Series(dtype=float)

    bh = b["BaoHanh"][b["BaoHanh"]["NgayTraKhach"].notna()]
    bao_hanh = bh.groupby(thang_cua(bh["NgayTraKhach"]))["ChiPhiShopChiu"].sum()

    thang = sorted(set(kq.index) | set(chi_phi.index) | set(hang_mat.index) | set(bao_hanh.index))
    kq = kq.reindex(thang).fillna(0)
    kq["ChiPhiVanHanh"] = chi_phi.reindex(thang).fillna(0)
    kq["HangMatHong"] = hang_mat.reindex(thang).fillna(0).map(lambda v: lam_tron(v, 2))
    kq["ChiPhiBaoHanh"] = bao_hanh.reindex(thang).fillna(0)
    kq["LaiRong"] = kq["LaiGop"] - kq["ChiPhiVanHanh"] - kq["HangMatHong"] - kq["ChiPhiBaoHanh"]
    kq["LaiRongTrenTienHang"] = (100 * kq["LaiRong"] / kq["TienHang"].where(kq["TienHang"] != 0)).map(lambda v: lam_tron(v, 1))
    return kq.reset_index().rename(columns={"index": "Thang"})


def tao_doi_soat_tien_san(b, tien_don):
    dh = b["DonHang"].set_index("MaDH")
    san = set(b["DoiTac"].loc[b["DoiTac"]["LoaiKenh"] == "Sàn thương mại điện tử", "MaDoiTac"]) if "LoaiKenh" in b["DoiTac"] else {"DT002", "DT003"}
    moc = b["DonHang"]["NgayDat"].max().normalize()
    tra_cuoi = b["TraHang"].groupby("MaDH")["NgayTra"].max()
    ve = b["TienSanChuyenVe"].set_index("MaDH")
    kq = []
    for r in tien_don[tien_don["TrangThai"].isin(TRANG_THAI_DA_GIAO)].itertuples():
        if dh.at[r.MaDH, "MaDoiTac"] not in san:
            continue
        giao = dh.at[r.MaDH, "NgayGiao"]
        tc = tra_cuoi.get(r.MaDH, pd.NaT)
        co = r.MaDH in ve.index
        tra = ve.at[r.MaDH, "SoTienSanTra"] if co else None
        if not co:
            tinh = "Quá 14 ngày sàn chưa chuyển tiền" if (moc.normalize() - (tc if pd.notna(tc) else giao).normalize()).days > 14 else "Sàn chưa chuyển (chưa tới hạn)"
        elif tra == r.TienShopNhan:
            tinh = "Khớp"
        else:
            tinh = "Sàn trả thiếu" if tra < r.TienShopNhan else "Sàn trả dư"
        kq.append(dict(MaDH=r.MaDH, KenhBan=r.KenhBan, NgayGiao=giao, NgayTraCuoi=tc, ShopTinhDuocNhan=r.TienShopNhan,
                       SoTienSanTra=tra, NgayChuyen=ve.at[r.MaDH, "NgayChuyen"] if co else pd.NaT, MaDot=ve.at[r.MaDH, "MaDot"] if co else None,
                       ChenhLech=(tra - r.TienShopNhan) if co else None, TinhTrang=tinh))
    return pd.DataFrame(kq)


def tao_cong_no_ncc(b):
    pn = b["PhieuNhap"]
    ncc = b["NhaCungCap"].set_index("MaNCC")
    tong = b["ChiTietPhieuNhap"].assign(T=b["ChiTietPhieuNhap"]["SoLuong"] * b["ChiTietPhieuNhap"]["DonGia"]).groupby("MaPN")["T"].sum()
    tt = b["ThanhToanNCC"].groupby("MaPN").agg(DaTra=("SoTien", "sum"), LanTraCuoi=("NgayTra", "max"))
    moc = b["DonHang"]["NgayDat"].max().normalize()
    kq = []
    for r in pn.itertuples():
        t = float(tong.get(r.MaPN, 0))
        da = float(tt["DaTra"].get(r.MaPN, 0))
        han = r.NgayNhap.normalize() + timedelta(days=int(ncc.at[r.MaNCC, "SoNgayCongNo"]))
        tinh = "Đã trả đủ" if t - da <= 0 else ("Quá hạn" if han < moc else "Chưa tới hạn")
        kq.append(dict(MaPN=r.MaPN, NgayNhap=r.NgayNhap, MaNCC=r.MaNCC, TenNCC=ncc.at[r.MaNCC, "TenNCC"], MaKho=r.MaKho, TongTien=t,
                       DaTra=da, ConNo=t - da, SoNgayCongNo=int(ncc.at[r.MaNCC, "SoNgayCongNo"]), HanTra=han,
                       LanTraCuoi=tt["LanTraCuoi"].get(r.MaPN, pd.NaT), TinhTrang=tinh))
    return pd.DataFrame(kq).sort_values("NgayNhap").reset_index(drop=True)


DOC_TRUOC = [
    ("BanHang", "Mỗi dòng là 1 món trong 1 đơn. DoanhThu = SoLuong x DonGia, đơn hủy thì 0. LaiGop = DoanhThu - SoLuong x giá nhập bình quân (chưa trừ phí sàn). NhomLyDoMua lấy từ khảo sát \"vì sao bạn mua\"."),
    ("TienDonHang", "Mỗi dòng là 1 đơn, tính giống view vw_TienDonHang và màn Báo cáo doanh thu: tiền shop thực nhận = tiền hàng - giảm giá của shop - phí sàn - tiền hoàn; lãi sau phí sàn = thực nhận - giá vốn."),
    ("Ngay", "Lịch từng ngày trong khoảng dữ liệu, đánh dấu ngày sale (bảng DotSale) và ngày lễ (bảng NgayLe)."),
    ("SanPham_BangDiem", "Mỗi sản phẩm 1 dòng, gom số bán, cảm xúc, trả hàng, kho, lý do mua. Món chủ lực = các món xếp từ doanh thu cao xuống, cộng dồn tới khi đủ 80% doanh thu."),
    ("KhachHang_Nhom", "Chia khách thành 5 hạng giống thủ tục sp_ChiaNhomKhachHang: chấm 1-5 điểm cho độ gần đây, số đơn, số tiền rồi cộng lại chia 5 hạng."),
    ("CamXuc_DanhGia", "Mỗi đánh giá 1 dòng. Đếm từ khen / từ chê theo bảng TuDien_CamXuc (đứng sau \"không\", \"chưa\", \"chẳng\" thì đổi chiều), giống thủ tục sp_PhanTichCamXuc_DanhGia."),
    ("CamXuc_TraHang", "Mỗi phiếu trả hàng 1 dòng, xếp lý do trả vào nhóm giống thủ tục sp_PhanTichChuDeKhieuNai."),
    ("TuKhachHayNoi", "Các từ khen / chê khách hay viết trong đánh giá (đứng sau \"không\", \"chưa\", \"chẳng\" thì ghi cả chữ đó), kèm 3 sản phẩm bị nhắc nhiều nhất."),
    ("LyDoMua_TongHop", "Mỗi nhóm lý do mua 1 dòng, giống thủ tục sp_BangThongBao_LyDoMua. DoanhThuCacDon chỉ tính đơn không hủy."),
    ("LyDoMua_ChiTiet", "Mỗi câu trả lời khảo sát 1 dòng, xếp nhóm giống thủ tục sp_PhanTichLyDoMua."),
    ("LyDoMua_TheoSanPham", "Với mỗi sản phẩm: trong các đơn có khảo sát, bao nhiêu % đơn thuộc từng nhóm lý do mua."),
    ("MuaTheoHungSale", "Mỗi sản phẩm: bán mỗi ngày sale so với ngày thường, tỉ lệ trả hàng, giống bảng 1 của thủ tục sp_BangThongBao_MuaTheoCamXuc."),
    ("MuaChung", "Các cặp món hay nằm chung 1 đơn, giống thủ tục sp_TimMonHayMuaChung. NenLamCombo = Có khi cặp xuất hiện ở ít nhất 1% đơn, mua A thì 30% có B, và gấp ít nhất 1 lần bình thường."),
    ("GioVang", "Số đơn không hủy và doanh thu theo thứ trong tuần và giờ đặt."),
    ("KhoHang", "Tồn kho, tốc độ bán, dự đoán và mức phải đặt hàng, giống view vw_DuBaoSanPham (xét 90 ngày, 30 ngày gần, hệ số tồn dự phòng 1,65)."),
    ("CungCau_SanPham", "Hàng nhiều hay khách nhiều, giai đoạn bán, tiền nằm trong kho, giống view vw_CungCau."),
    ("CungCau_TheoThang", "Từng tháng của từng sản phẩm: khách đặt (Cau) so với hàng có để bán (Cung = tồn đầu tháng + nhập trong tháng), giống hàm fn_CungCauTheoThang."),
    ("LocDuLieu_NhatKy", "Những lỗi dữ liệu đã tìm thấy và cách xử lý."),
    ("KhachHang_DaLoc", "Danh sách khách lấy từ file xuất của sàn và nhập tay (bảng DuLieuTho_KhachHang) sau khi sửa tên, số điện thoại, địa chỉ và bỏ trùng."),
    ("DonSan_DaLoc", "Đơn lấy từ file xuất của sàn (bảng DuLieuTho_DonHang) sau khi bỏ trùng mã đơn và sửa thông tin."),
    ("LaiRongTheoThang", "Mỗi tháng 1 dòng, giống view vw_LaiRongTheoThang: lãi sau phí sàn - chi phí vận hành - hàng mất, hỏng (xuất kho không gắn đơn, xuất dư, thiếu khi kiểm kê, tính theo giá nhập bình quân) - chi phí bảo hành shop chịu = lãi ròng. Chi phí vận hành là số mô phỏng, xem GHI_CHU_DU_LIEU_MO_PHONG.txt."),
    ("ChiPhiVanHanh", "Bảng ChiPhiVanHanh: từng khoản chi mỗi tháng và cách tính (số mô phỏng)."),
    ("DoiSoatTienSan", "Mỗi đơn sàn đã giao 1 dòng, giống view vw_DoiSoatTienSan: tiền phần mềm tính shop được nhận so với tiền sàn thật sự chuyển về."),
    ("CongNoNhaCungCap", "Mỗi phiếu nhập 1 dòng, giống view vw_CongNoNhaCungCap: đã trả bao nhiêu, còn nợ bao nhiêu, hạn trả, quá hạn hay chưa."),
]


def tao_viec_can_lam(so_lieu):
    s = so_lieu
    d = []
    d.append("=" * 100)
    d.append(f"VIỆC CẦN LÀM - số liệu từ {s['tu']:%d/%m/%Y} đến {s['den']:%d/%m/%Y} (nguồn: {s['nguon']})")
    d.append("=" * 100)
    d.append("")
    d.append("1. BÁN ĐƯỢC BAO NHIÊU")
    d.append(f"   Doanh thu (không tính đơn hủy): {tien_vn(s['doanh_thu'])} đ từ {s['so_don']} đơn.")
    d.append(f"   Đơn hủy: {s['don_huy']} / {s['so_don'] + s['don_huy']} đơn ({so_vn(100 * s['don_huy'] / (s['so_don'] + s['don_huy']), 1)}%), "
             f"trong đó {s['giao_that_bai']} đơn giao không thành công (hàng đã xuất rồi quay về).")
    d.append(f"   Giá trị 1 đơn trung bình: {tien_vn(s['doanh_thu'] / s['so_don'])} đ.")
    d.append(f"   Lãi gộp theo giá nhập trung bình (chưa trừ phí sàn): {tien_vn(s['lai_gop'])} đ ({so_vn(100 * s['lai_gop'] / s['doanh_thu'], 1)}% doanh thu).")
    d.append(f"   Phí sàn đã trả cho các đơn đã giao: {tien_vn(s['phi_san'])} đ. Tiền hoàn cho khách: {tien_vn(s['tien_hoan'])} đ.")
    d.append(f"   Lãi sau phí sàn và tiền hoàn (giống màn Báo cáo doanh thu): {tien_vn(s['lai_sau_phi'])} đ "
             f"({so_vn(100 * s['lai_sau_phi'] / s['doanh_thu'], 1)}% doanh thu).")
    for k in s["theo_kenh"]:
        d.append(f"     - {k['KenhBan']}: tiền hàng {tien_vn(k['TienHang'])} đ, phí sàn {tien_vn(k['PhiSan'])} đ, lãi sau phí {tien_vn(k['LaiGop'])} đ "
                 f"({so_vn(100 * k['LaiGop'] / k['TienHang'], 1) if k['TienHang'] else '–'}% tiền hàng).")
    d.append(f"   Tháng bán tốt nhất: {s['thang_cao']} ({so_vn(s['thang_cao_tien'] / 1e6, 1)} triệu); tháng thấp nhất: {s['thang_thap']} "
             f"({so_vn(s['thang_thap_tien'] / 1e6, 1)} triệu).")
    d.append(f"   {len(s['chu_luc'])} / {s['so_sp']} sản phẩm làm ra 80% doanh thu (món chủ lực): {', '.join(s['chu_luc'])}.")
    if s["hot"]:
        d.append("   Món đang hot (30 ngày gần bán nhanh hơn trung bình 90 ngày từ 20%): "
                 + ", ".join(f"{t} (dự đoán bán {n} cái trong 30 ngày tới)" for t, n in s["hot"]) + ".")
    d.append("")
    d.append("2. KHÁCH MUA VÌ CẢM XÚC HAY VÌ CẦN DÙNG (khảo sát \"Vì sao bạn mua?\" sau khi chốt đơn)")
    for k in s["kieu_ly_do"]:
        d.append(f"   {k['Kieu']}: {k['SoCau']} câu trả lời ({so_vn(k['PT'], 1)}%), các đơn này mang về {so_vn(k['DT'] / 1e6, 1)} triệu "
                 f"({so_vn(k['PTDT'], 1)}% doanh thu có khảo sát).")
    d.append("   Chi tiết từng nhóm và cách bán hợp với nhóm đó:")
    for r in s["ly_do"].itertuples():
        d.append(f"   - {r.NhomLyDoMua} ({r.KieuLyDoMua}): {r.SoCauTraLoi} câu, đơn TB {so_vn(r.GiaTriDonTB / 1e6, 1)} triệu, hào hứng {so_vn(r.HaoHungTB, 1)}/5.")
        d.append(f"       Cách bán: {r.CachBanHop}")
    d.append("")
    d.append("3. KHÁCH VUI HAY BUỒN")
    d.append(f"   {so_vn(s['csat'], 1)}% đánh giá từ 4 sao trở lên. {s['sao_cao_loi_che']} đánh giá 4-5 sao nhưng lời viết lại chê.")
    for r in s["cam_xuc_do"]:
        d.append(f"   - {r}")
    d.append("   Lý do trả hàng nhiều nhất: " + ", ".join(f"{t} ({n} phiếu)" for t, n in s["ly_do_tra"]) + ".")
    d.append("")
    d.append("4. KHO HÀNG")
    for r in s["kho"]:
        d.append(f"   - {r}")
    d.append(f"   Tiền đang nằm trong kho (tính theo giá nhập trung bình): {tien_vn(s['tien_kho'])} đ.")
    d.append("")
    d.append("5. KHÁCH HÀNG")
    for r in s["nhom_khach"]:
        d.append(f"   - {r}")
    d.append("")
    d.append("6. MÓN NÊN BÁN KÈM (combo)")
    for r in s["combo"]:
        d.append(f"   - {r}")
    d.append("")
    d.append("7. LÃI THẬT SAU KHI TRỪ CHI PHÍ (chi phí vận hành là số mô phỏng, sửa ở màn Chi phí vận hành và lãi ròng)")
    d.append(f"   Lãi sau phí sàn {tien_vn(s['lai_rong_lai_gop'])} đ - chi phí vận hành {tien_vn(s['chi_phi'])} đ - hàng mất, hỏng {tien_vn(s['hang_mat'])} đ"
             f" - bảo hành shop chịu {tien_vn(s['chi_bao_hanh'])} đ = lãi ròng {tien_vn(s['lai_rong'])} đ.")
    d.append(f"   Số tháng có lãi ròng dương: {s['thang_lai']} / {s['so_thang']}. Chi phí vận hành bằng {so_vn(100 * s['chi_phi'] / s['doanh_thu'], 1)}% doanh thu, "
             f"trong khi lãi sau phí sàn chỉ bằng {so_vn(100 * s['lai_sau_phi'] / s['doanh_thu'], 1)}% doanh thu.")
    for t, v in s["chi_phi_nhom"]:
        d.append(f"     - {t}: {tien_vn(v)} đ")
    d.append(f"   Muốn hòa vốn với mức chi phí này, mỗi tháng cần lãi sau phí sàn thêm khoảng {tien_vn(s['can_them'])} đ.")
    d.append("")
    d.append("8. TIỀN SÀN CHUYỂN VỀ VÀ NỢ NHÀ CUNG CẤP")
    for t, n in s["doi_soat"]:
        d.append(f"   - {t}: {n} đơn")
    d.append(f"   Nợ nhà cung cấp còn {tien_vn(s['con_no'])} đ ({s['so_phieu_no']} phiếu nhập), trong đó quá hạn {tien_vn(s['qua_han'])} đ.")
    return "\n".join(d) + "\n"


def main():
    p = argparse.ArgumentParser(description="Tạo file Excel cho Power BI từ database QuanLyThuongMaiDienTu.")
    nguon = p.add_mutually_exclusive_group(required=True)
    nguon.add_argument("--sqlserver", help="Chuỗi kết nối ODBC tới database QuanLyThuongMaiDienTu")
    nguon.add_argument("--sqlfile", help="Đường dẫn file QuanLyThuongMaiDienTu_Database.sql")
    p.add_argument("--thumuc", default=os.path.join(os.path.dirname(os.path.abspath(__file__)), "KetQua_PowerBI"))
    a = p.parse_args()

    if a.sqlserver:
        in_buoc(1, "Đọc các bảng từ SQL Server")
        b = chuan_hoa(doc_tu_sql_server(a.sqlserver), False)
        b_nsl = nsl.chuan_hoa_kieu(nsl.doc_tu_sql_server(a.sqlserver))
        ten_nguon = "SQL Server"
    else:
        in_buoc(1, "Đọc các bảng từ các lệnh INSERT trong file .sql và làm lại những việc SQL Server tự làm (tổng tiền đơn, giá vốn, tồn kho)")
        b = chuan_hoa(doc_tu_file_sql(a.sqlfile), True)
        b_nsl = nsl.chuan_hoa_kieu(nsl.lam_lai_buoc_sql_server_tu_lam(nsl.doc_tu_file_sql(a.sqlfile)))
        ten_nguon = "file .sql"
    for t in ["DonHang", "ChiTietDonHang", "TraHang", "DanhGiaSanPham", "KhaoSat_LyDoMua", "DuLieuTho_KhachHang", "DuLieuTho_DonHang"]:
        print(f"        {t}: {len(b[t])} dòng")
    teen = ds_teencode(b)
    ngay_tinh = b["DonHang"]["NgayDat"].max().normalize()

    in_buoc(2, "Làm sạch lý do trả hàng và xếp nhóm lý do trả")
    th = nhom_ly_do_tra(b, teen)
    b["TraHang"] = th

    in_buoc(3, "Đọc cảm xúc trong đánh giá và lý do trả hàng")
    cx = doc_cam_xuc(b, teen)

    in_buoc(4, "Xếp câu trả lời khảo sát \"vì sao bạn mua\" vào nhóm")
    ks = b["KhaoSat_LyDoMua"].merge(xep_ly_do_mua(b, teen), left_on="Id", right_on="KhaoSatId")
    cach = b["CachBan_TheoLyDoMua"].set_index("NhomLyDo")
    ks["NhomLyDoMua"] = ks["NhomLyDo"]
    ks["KieuLyDoMua"] = ks["NhomLyDo"].map(cach["KieuLyDo"]).fillna("Không rõ")

    in_buoc(5, "Tạo bảng BanHang, Ngay, GioVang, TienDonHang")
    bh = tao_ban_hang(b, ks)
    ngay = tao_bang_ngay(b, bh)
    gio_vang = tao_gio_vang(bh)
    tien_don = nsl.tao_tien_don_hang(b_nsl)
    tien_don.insert(2, "Ngay", tien_don["NgayDat"].dt.normalize())

    in_buoc(6, "Tính tốc độ bán, dự đoán và mức phải đặt hàng (giống vw_DuBaoSanPham)")
    db, den_px = du_bao(b)
    kho = pd.DataFrame(dict(MaSP=db["MaSP"], TenSP=db["TenSP"], TenNCC=db["TenNCC"], TonHienTai=db["TonHienTai"],
                            BanMoiNgay_90Ngay=db["ADS"].map(lambda x: lam_tron(x, 2)), BanMoiNgay_30Ngay=db["ADS30"].map(lambda x: lam_tron(x, 2)),
                            TangGiamPhanTram=db["XuHuongPT"], XuHuongBan=db["Cau"], DuDoanBan30NgayToi=db["DuDoan30Ngay"],
                            SoNgayHangVe=db["LeadTime"].map(lambda x: lam_tron(x, 2)), TonDuPhong=db["TonAnToan"], MucPhaiDatHang=db["ROP"],
                            DuBanMayNgay=db["SoNgayConLai"], NenNhapThem=db["SoNenNhap"], TinhTrang=db["TrangThai"],
                            HayMuaChungVoi=db["MonHayMuaChung"], NenLamGi=db["ViecNenLam"], ThongBao=db["ThongBao"]))

    in_buoc(7, "Tính cung - cầu (giống vw_CungCau và fn_CungCauTheoThang)")
    cc = cung_cau(b, db)
    cc_thang = cung_cau_theo_thang(b)

    in_buoc(8, "Tìm món hay mua chung")
    mc = mua_chung(b)

    in_buoc(9, "Chia nhóm khách hàng")
    rfm = chia_nhom_khach(b, ngay_tinh)
    kh_nhom = tao_khach_hang_nhom(b, rfm, bh, ks, ngay_tinh)

    in_buoc(10, "Bảng cảm xúc, mua theo hứng sale, lý do mua")
    bcx = bang_cam_xuc(b, th, cx)
    mhs = mua_theo_hung_sale(b, th)
    ly_do, doanh_thu_ks = ly_do_mua_tong_hop(b, ks)
    dg = b["DanhGiaSanPham"].merge(b["SanPham"][["MaSP", "TenSP"]], on="MaSP", how="left")
    dg = dg.merge(cx[cx["Loai"] == "Đánh giá"][["Ma", "SoTuKhen", "SoTuChe", "NhomCamXuc"]], left_on="MaDanhGia", right_on="Ma", how="left")
    dg["CamXucTheoLoi"] = dg["NhomCamXuc"]
    dg["CamXucTheoSao"] = dg["SoSao"].map(lambda x: "Vui (4-5 sao)" if x >= 4 else ("Bình thường (3 sao)" if x == 3 else "Buồn (1-2 sao)"))
    dg["SaoCaoMaLoiChe"] = ((dg["SoSao"] >= 4) & dg["NoiDung"].fillna("").str.strip().ne("") & (dg["CamXucTheoLoi"] != "Khen")).map({True: "Có", False: "Không"})
    cam_xuc_dg = dg[["MaDanhGia", "NgayDanhGia", "MaSP", "TenSP", "MaKH", "MaDH", "SoSao", "NoiDung", "SoTuKhen", "SoTuChe",
                     "CamXucTheoLoi", "CamXucTheoSao", "SaoCaoMaLoiChe"]].sort_values("NgayDanhGia")
    tra = th.merge(b["SanPham"][["MaSP", "TenSP"]], on="MaSP", how="left").merge(
        cx[cx["Loai"] == "Trả hàng"][["Ma", "NhomCamXuc"]], left_on="MaTraHang", right_on="Ma", how="left")
    cam_xuc_tra = tra.rename(columns={"NhomLyDoChinh": "NhomLyDoTra", "NhomCamXuc": "CamXucTheoLoi"})[
        ["MaTraHang", "NgayTra", "MaDH", "MaSP", "TenSP", "SoLuong", "SoTienHoan", "TinhTrangHang", "LyDoTra", "NhomLyDoTra", "CamXucTheoLoi"]]
    tu_noi = tao_tu_khach_hay_noi(cx, b)
    co_tra = set(th["MaDH"])
    ly_do_ct = ks.assign(GiaTriDon=ks["MaDH"].map(b["DonHang"].set_index("MaDH")["TongTien"]),
                         CoTraHang=ks["MaDH"].isin(co_tra).map({True: "Có", False: "Không"}),
                         CachBanHop=ks["NhomLyDo"].map(cach["CachBanHop"]))[
        ["Id", "MaDH", "MaKH", "NgayTraLoi", "LoiKhachNoi", "MucHaoHung", "NhomLyDoMua", "KieuLyDoMua", "GiaTriDon", "CoTraHang", "CachBanHop"]]
    don_sp = b["ChiTietDonHang"][["MaDH", "MaSP"]].drop_duplicates().merge(ks[["MaDH", "NhomLyDoMua"]], on="MaDH")
    ly_do_sp = don_sp.groupby(["MaSP", "NhomLyDoMua"]).size().reset_index(name="SoDon")
    ly_do_sp["PhanTramCuaSanPham"] = (100 * ly_do_sp["SoDon"] / ly_do_sp.groupby("MaSP")["SoDon"].transform("sum")).map(lambda x: lam_tron(x, 1))
    ly_do_sp = ly_do_sp.merge(b["SanPham"][["MaSP", "TenSP"]], on="MaSP")[["MaSP", "TenSP", "NhomLyDoMua", "SoDon", "PhanTramCuaSanPham"]]
    ly_do_sp = ly_do_sp.sort_values(["MaSP", "SoDon"], ascending=[True, False]).reset_index(drop=True)

    in_buoc(11, "Gom bảng điểm sản phẩm và lọc dữ liệu thô")
    bang_diem = tao_bang_diem_san_pham(b, bh, db, bcx, mhs, ly_do_sp)
    nhat_ky, kh_loc, don_loc = loc_du_lieu(b)

    lai_rong = tao_lai_rong_theo_thang(b, tien_don)
    doi_soat = tao_doi_soat_tien_san(b, tien_don)
    cong_no = tao_cong_no_ncc(b)
    chi_phi = b["ChiPhiVanHanh"].sort_values(["Thang", "NhomChiPhi", "TenChiPhi"]).reset_index(drop=True)

    in_buoc(12, "Ghi file Excel, CSV và ViecCanLam.txt")
    trang = {
        "BanHang": bh, "TienDonHang": tien_don, "Ngay": ngay, "SanPham_BangDiem": bang_diem, "KhachHang_Nhom": kh_nhom,
        "CamXuc_DanhGia": cam_xuc_dg, "CamXuc_TraHang": cam_xuc_tra, "TuKhachHayNoi": tu_noi, "LyDoMua_TongHop": ly_do,
        "LyDoMua_ChiTiet": ly_do_ct, "LyDoMua_TheoSanPham": ly_do_sp, "MuaTheoHungSale": mhs, "MuaChung": mc, "GioVang": gio_vang,
        "KhoHang": kho, "CungCau_SanPham": cc, "CungCau_TheoThang": cc_thang, "LocDuLieu_NhatKy": nhat_ky,
        "KhachHang_DaLoc": kh_loc, "DonSan_DaLoc": don_loc, "LaiRongTheoThang": lai_rong, "ChiPhiVanHanh": chi_phi,
        "DoiSoatTienSan": doi_soat, "CongNoNhaCungCap": cong_no,
    }
    os.makedirs(a.thumuc, exist_ok=True)
    file_excel = os.path.join(a.thumuc, "DuLieu_PowerBI.xlsx")
    with pd.ExcelWriter(file_excel, engine="openpyxl") as w:
        pd.DataFrame(DOC_TRUOC, columns=["TrangTinh", "NoiDung"]).to_excel(w, sheet_name="DocTruoc", index=False)
        for ten, df in trang.items():
            df.to_excel(w, sheet_name=ten, index=False)
            df.to_csv(os.path.join(a.thumuc, ten + ".csv"), index=False, encoding="utf-8-sig")

    ok = bh[bh["LaDonHuy"] == "Không"]
    don_ok = b["DonHang"][b["DonHang"]["TrangThai"] != TRANG_THAI_HUY]
    theo_thang = ok.groupby("ThangNam")["DoanhThu"].sum()
    giao = tien_don[tien_don["TrangThai"].isin(TRANG_THAI_DA_GIAO)]
    theo_kenh = giao.groupby("KenhBan")[["TienHang", "PhiSan", "LaiGop"]].sum().reset_index().to_dict("records")
    kieu = []
    for k, g in ly_do.groupby("KieuLyDoMua", sort=False):
        kieu.append(dict(Kieu=k, SoCau=int(g["SoCauTraLoi"].sum()), PT=100 * g["SoCauTraLoi"].sum() / ly_do["SoCauTraLoi"].sum(),
                         DT=g["DoanhThuCacDon"].sum(), PTDT=100 * g["DoanhThuCacDon"].sum() / doanh_thu_ks))
    kieu.sort(key=lambda x: -x["SoCau"])
    nhom_kh = kh_nhom[kh_nhom["Hang"].notna()].groupby("NhomKhach").agg(N=("MaKH", "count"), T=("TongTienMua", "sum")).reset_index()
    nhom_kh = nhom_kh.sort_values("NhomKhach", ascending=False)
    so_lieu = dict(
        tu=b["DonHang"]["NgayDat"].min(), den=b["DonHang"]["NgayDat"].max(), nguon=ten_nguon,
        doanh_thu=ok["DoanhThu"].sum(), so_don=len(don_ok), don_huy=int((b["DonHang"]["TrangThai"] == TRANG_THAI_HUY).sum()),
        giao_that_bai=int(b["DonHang"]["LyDoHuy"].fillna("").str.startswith("Giao không thành công").sum()),
        lai_gop=ok["LaiGop"].sum(), phi_san=giao["PhiSan"].sum(), tien_hoan=giao["TienHoanKhach"].sum(), lai_sau_phi=giao["LaiGop"].sum(),
        theo_kenh=theo_kenh, thang_cao=theo_thang.idxmax(), thang_cao_tien=theo_thang.max(), thang_thap=theo_thang.idxmin(),
        thang_thap_tien=theo_thang.min(), chu_luc=bang_diem[bang_diem["MonChuLuc"] == "Có"]["TenSP"].tolist(), so_sp=len(bang_diem),
        hot=[(r.TenSP, r.DuDoan30Ngay) for r in db[db["Cau"] == "ĐANG HOT - bán nhanh lên"].itertuples()],
        kieu_ly_do=kieu, ly_do=ly_do,
        csat=100 * (b["DanhGiaSanPham"]["SoSao"] >= 4).mean(), sao_cao_loi_che=int((cam_xuc_dg["SaoCaoMaLoiChe"] == "Có").sum()),
        cam_xuc_do=[f"{r.TenSP}: {r.ThongBao}" for r in bcx.join(b["SanPham"].set_index("MaSP")["TenSP"]).itertuples() if r.TrangThai in ("ĐỎ", "VÀNG")],
        ly_do_tra=list(th["NhomLyDoChinh"].value_counts().head(3).items()),
        kho=[f"{r.TenSP} - {r.TrangThai}: {r.ViecNenLam}" for r in db.itertuples() if r.TrangThai != "ỔN" or r.Cau == "ĐANG HOT - bán nhanh lên"],
        tien_kho=cc["TienNamTrongKho"].sum(),
        nhom_khach=[f"{r.NhomKhach}: {r.N} khách, tổng mua {so_vn(r.T / 1e6, 1)} triệu. {VIEC_THEO_HANG[int(r.NhomKhach[5])]}" for r in nhom_kh.itertuples()],
        combo=[f"{r.TenSPChinh} + {r.TenSPKem}: {so_vn(r.MuaAThiMuaKemB, 1)}% khách mua món đầu có mua kèm món sau, gấp {so_vn(r.GapMayLanBinhThuong, 1)} lần bình thường."
               for r in mc[mc["NenLamCombo"] == "Có"].head(5).itertuples()],
        lai_rong_lai_gop=lai_rong["LaiGop"].sum(), chi_phi=lai_rong["ChiPhiVanHanh"].sum(), hang_mat=lai_rong["HangMatHong"].sum(),
        chi_bao_hanh=lai_rong["ChiPhiBaoHanh"].sum(), lai_rong=lai_rong["LaiRong"].sum(),
        thang_lai=int((lai_rong["LaiRong"] > 0).sum()), so_thang=len(lai_rong),
        chi_phi_nhom=list(chi_phi.groupby("NhomChiPhi")["SoTien"].sum().sort_values(ascending=False).items()),
        can_them=max(0, -lai_rong["LaiRong"].sum()) / max(1, len(lai_rong)),
        doi_soat=list(doi_soat["TinhTrang"].value_counts().items()) if len(doi_soat) else [],
        con_no=cong_no["ConNo"].sum(), so_phieu_no=int((cong_no["ConNo"] > 0).sum()),
        qua_han=cong_no.loc[cong_no["TinhTrang"] == "Quá hạn", "ConNo"].sum(),
    )
    with open(os.path.join(a.thumuc, "ViecCanLam.txt"), "w", encoding="utf-8-sig") as f:
        f.write(tao_viec_can_lam(so_lieu))

    print()
    print(f"Doanh thu {tien_vn(so_lieu['doanh_thu'])} đ | {so_lieu['so_don']} đơn | {so_lieu['don_huy']} đơn hủy | "
          f"lãi sau phí sàn {tien_vn(so_lieu['lai_sau_phi'])} đ | tiền nằm trong kho {tien_vn(so_lieu['tien_kho'])} đ")
    print(f"Đã lưu: {file_excel} ({len(trang)} trang tính + DocTruoc), các file .csv và ViecCanLam.txt")
    print()
    print(tao_viec_can_lam(so_lieu))


if __name__ == "__main__":
    main()
