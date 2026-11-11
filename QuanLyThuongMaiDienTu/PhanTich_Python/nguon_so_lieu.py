import argparse
import os
import re
import sys
from datetime import date, datetime
from decimal import Decimal, ROUND_HALF_UP

import pandas as pd

TRANG_THAI_HUY = "Đã hủy"
TRANG_THAI_DA_GIAO = ["Đã giao", "Trả hàng/Hoàn tiền"]
THU = ["Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7", "Chủ nhật"]

BANG_CAN_DOC = {
    "DonHang": ["MaDH", "NgayDat", "MaKH", "MaDoiTac", "TrangThai", "GiamGiaCuaShop", "PhiVanChuyenKhachTra"],
    "ChiTietDonHang": ["MaDH", "MaSP", "SoLuong", "DonGia", "GiaVon"],
    "SanPham": ["MaSP", "TenSP", "MaDanhMuc", "GiaBan"],
    "DanhMuc": ["MaDanhMuc", "TenDanhMuc"],
    "KhachHang": ["MaKH", "TenKH"],
    "DoiTac": ["MaDoiTac", "TenDoiTac"],
    "PhieuNhap": ["MaPN", "NgayNhap"],
    "ChiTietPhieuNhap": ["MaPN", "MaSP", "SoLuong", "DonGia"],
    "PhieuXuat": ["MaPX", "MaDH"],
    "TraHang": ["MaTraHang", "MaDH", "MaSP", "SoLuong", "SoTienHoan", "TinhTrangHang"],
    "DotSale": ["MaDot", "TenDot", "NgayBatDau", "NgayKetThuc"],
    "PhiSan": ["MaDoiTac", "MaDanhMuc", "LoaiPhi", "CachTinh", "GiaTri", "TuNgay", "DenNgay"],
}

SO_KIEM_TRA = {
    "Doanh thu": 3712302000,
    "Số đơn": 1467,
    "Đơn hủy": 149,
    "Giá trị 1 đơn": 2530540,
    "% lãi gộp": 14.8,
    "Doanh thu mỗi ngày sale": 15831327,
    "Doanh thu mỗi ngày thường": 9166384,
    "Số ngày sale": 55,
    "Số ngày thường": 310,
}


def in_buoc(so, tong, noi_dung):
    print(f"Bước {so}/{tong}: {noi_dung}")


def tach_gia_tri(dong):
    ket_qua, i, n = [], 0, len(dong)
    while i < n:
        c = dong[i]
        if c in " \t\r\n,":
            i += 1
            continue
        if c in "Nn" and i + 1 < n and dong[i + 1] == "'":
            i += 1
            c = "'"
        if c == "'":
            i += 1
            chu = []
            while i < n:
                if dong[i] == "'":
                    if i + 1 < n and dong[i + 1] == "'":
                        chu.append("'")
                        i += 2
                        continue
                    i += 1
                    break
                chu.append(dong[i])
                i += 1
            ket_qua.append("".join(chu))
            continue
        j = i
        sau = 0
        while j < n and not (dong[j] == "," and sau == 0):
            if dong[j] == "(":
                sau += 1
            elif dong[j] == ")":
                sau -= 1
            j += 1
        tu = dong[i:j].strip()
        ket_qua.append(None if tu.upper() == "NULL" else tu)
        i = j
    return ket_qua


def tach_cac_dong(khoi):
    cac_dong, i, n = [], 0, len(khoi)
    while i < n:
        if khoi[i] != "(":
            if khoi[i] == ";":
                break
            i += 1
            continue
        sau, j, trong_chu = 0, i, False
        while j < n:
            c = khoi[j]
            if trong_chu:
                if c == "'":
                    if j + 1 < n and khoi[j + 1] == "'":
                        j += 2
                        continue
                    trong_chu = False
            elif c == "'":
                trong_chu = True
            elif c == "(":
                sau += 1
            elif c == ")":
                sau -= 1
                if sau == 0:
                    break
            j += 1
        cac_dong.append(tach_gia_tri(khoi[i + 1:j]))
        i = j + 1
    return cac_dong


def doc_tu_file_sql(duong_dan):
    with open(duong_dan, encoding="utf-8-sig") as f:
        noi_dung = f.read().replace("\r\n", "\n")
    bang = {}
    mau = re.compile(r"INSERT INTO (\w+)\s*\(([^)]*)\)\s*VALUES\s*\n", re.I)
    for m in mau.finditer(noi_dung):
        ten = m.group(1)
        if ten not in BANG_CAN_DOC:
            continue
        cot = [c.strip() for c in m.group(2).split(",")]
        bat_dau = m.end()
        ket_thuc = bat_dau
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
            elif c == ";" or noi_dung.startswith("\nGO", ket_thuc) or noi_dung.startswith("\n\n", ket_thuc):
                break
            ket_thuc += 1
        for dong in tach_cac_dong(noi_dung[bat_dau:ket_thuc]):
            bang.setdefault(ten, []).append(dict(zip(cot, dong)))
    ket_qua = {}
    for ten, cot in BANG_CAN_DOC.items():
        df = pd.DataFrame(bang.get(ten, []))
        for c in cot:
            if c not in df.columns:
                df[c] = None
        ket_qua[ten] = df[cot].copy()
    return ket_qua


def doc_ngay(s):
    if s is None or (isinstance(s, float) and pd.isna(s)):
        return pd.NaT
    s = str(s).strip()
    for dinh_dang in ("%Y%m%d %H:%M:%S", "%Y%m%d", "%Y-%m-%d %H:%M:%S", "%Y-%m-%d"):
        try:
            return datetime.strptime(s, dinh_dang)
        except ValueError:
            pass
    return pd.to_datetime(s)


def lam_lai_buoc_sql_server_tu_lam(b):
    dh, ct, ctpn, px, th, sp = (b["DonHang"], b["ChiTietDonHang"], b["ChiTietPhieuNhap"],
                                b["PhieuXuat"], b["TraHang"], b["SanPham"])
    for c in ["SoLuong", "DonGia"]:
        ct[c] = pd.to_numeric(ct[c])
        ctpn[c] = pd.to_numeric(ctpn[c])
    th["SoLuong"] = pd.to_numeric(th["SoLuong"])
    th["SoTienHoan"] = pd.to_numeric(th["SoTienHoan"]).fillna(0)
    th["TinhTrangHang"] = th["TinhTrangHang"].fillna("Còn bán được")
    dh["GiamGiaCuaShop"] = pd.to_numeric(dh["GiamGiaCuaShop"]).fillna(0)
    dh["PhiVanChuyenKhachTra"] = pd.to_numeric(dh["PhiVanChuyenKhachTra"]).fillna(0)

    ctpn["TienNhap"] = ctpn["SoLuong"] * ctpn["DonGia"]
    gia_von = ctpn.groupby("MaSP").agg(TienNhap=("TienNhap", "sum"), SoLuongNhap=("SoLuong", "sum"))
    gia_von["GiaVon"] = (gia_von["TienNhap"] / gia_von["SoLuongNhap"]).round(2)
    sp["GiaVon"] = sp["MaSP"].map(gia_von["GiaVon"])
    ct["GiaVon"] = ct["MaSP"].map(gia_von["GiaVon"])

    return b


def doc_tu_sql_server(chuoi_ket_noi):
    import pyodbc
    ket_qua = {}
    with pyodbc.connect(chuoi_ket_noi) as conn:
        for ten, cot in BANG_CAN_DOC.items():
            ket_qua[ten] = pd.read_sql(f"SELECT {', '.join(cot)} FROM dbo.{ten}", conn)
    return ket_qua


def chuan_hoa_kieu(b):
    b["DonHang"]["NgayDat"] = b["DonHang"]["NgayDat"].map(doc_ngay)
    for c in ["SoLuong", "DonGia", "GiaVon"]:
        b["ChiTietDonHang"][c] = pd.to_numeric(b["ChiTietDonHang"][c])
    b["SanPham"]["GiaBan"] = pd.to_numeric(b["SanPham"]["GiaBan"])
    for c in ["NgayBatDau", "NgayKetThuc"]:
        b["DotSale"][c] = b["DotSale"][c].map(doc_ngay)
    ps = b["PhiSan"]
    ps["GiaTri"] = pd.to_numeric(ps["GiaTri"])
    ps["TuNgay"] = ps["TuNgay"].map(doc_ngay)
    ps["DenNgay"] = ps["DenNgay"].map(doc_ngay)
    for c in ["SoLuong", "SoTienHoan"]:
        b["TraHang"][c] = pd.to_numeric(b["TraHang"][c]).fillna(0)
    b["TraHang"]["TinhTrangHang"] = b["TraHang"]["TinhTrangHang"].fillna("Còn bán được")
    for c in ["GiamGiaCuaShop", "PhiVanChuyenKhachTra"]:
        b["DonHang"][c] = pd.to_numeric(b["DonHang"][c]).fillna(0)
    return b


def tao_bang_ngay(b, tu, den):
    ngay = pd.DataFrame({"Ngay": pd.date_range(tu, den, freq="D")})
    ngay["ThangNam"] = ngay["Ngay"].dt.strftime("%Y-%m")
    ngay["Thu"] = ngay["Ngay"].dt.dayofweek.map(lambda x: THU[x])
    ngay["TenDotSale"] = ""
    for _, d in b["DotSale"].sort_values("NgayBatDau").iterrows():
        trong = (ngay["Ngay"] >= d["NgayBatDau"]) & (ngay["Ngay"] <= d["NgayKetThuc"])
        ngay.loc[trong, "TenDotSale"] = d["TenDot"]
    ngay["LaNgaySale"] = ngay["TenDotSale"].map(lambda x: "Có" if x else "Không")
    return ngay


def tao_ban_hang(b, ngay):
    dh, ct, sp = b["DonHang"], b["ChiTietDonHang"], b["SanPham"]
    bh = ct.merge(dh, on="MaDH", how="inner")
    bh = bh.merge(sp[["MaSP", "TenSP", "MaDanhMuc", "GiaBan"]], on="MaSP", how="left")
    bh = bh.merge(b["DanhMuc"], on="MaDanhMuc", how="left")
    bh = bh.merge(b["KhachHang"], on="MaKH", how="left")
    bh = bh.merge(b["DoiTac"].rename(columns={"TenDoiTac": "KenhBan"}), on="MaDoiTac", how="left")
    bh["Ngay"] = bh["NgayDat"].dt.normalize()
    bh = bh.merge(ngay[["Ngay", "TenDotSale", "LaNgaySale"]], on="Ngay", how="left")
    bh["LaDonHuy"] = bh["TrangThai"].fillna("").str.contains("hủy").map({True: "Có", False: "Không"})
    khong_huy = bh["LaDonHuy"] == "Không"
    bh["ThanhTien"] = bh["SoLuong"] * bh["DonGia"]
    bh["DoanhThu"] = bh["ThanhTien"].where(khong_huy, 0)
    bh["TienVon"] = (bh["SoLuong"] * bh["GiaVon"]).where(khong_huy, 0)
    bh["LaiGop"] = bh["DoanhThu"] - bh["TienVon"]
    tra = b["TraHang"].groupby(["MaDH", "MaSP"]).agg(SoLuongTra=("SoLuong", "sum"), TienHoan=("SoTienHoan", "sum")).reset_index()
    bh = bh.merge(tra, on=["MaDH", "MaSP"], how="left")
    bh[["SoLuongTra", "TienHoan"]] = bh[["SoLuongTra", "TienHoan"]].fillna(0)
    cot = ["MaDH", "NgayDat", "Ngay", "TrangThai", "LaDonHuy", "KenhBan", "LaNgaySale", "TenDotSale", "MaKH", "TenKH",
           "MaSP", "TenSP", "TenDanhMuc", "SoLuong", "GiaBan", "DonGia", "ThanhTien", "DoanhThu", "GiaVon", "TienVon",
           "LaiGop", "SoLuongTra", "TienHoan"]
    return bh[cot].sort_values(["NgayDat", "MaDH", "MaSP"]).reset_index(drop=True)


def so(x):
    return Decimal(str(x)) if x is not None and not pd.isna(x) else Decimal(0)


def lam_tron(x):
    return float(x.quantize(Decimal("1"), rounding=ROUND_HALF_UP))


def phi_san_cua_don(don, phi_san, tien_danh_muc):
    ngay = don["NgayDat"].normalize()
    dong = phi_san[(phi_san["MaDoiTac"] == don["MaDoiTac"]) & (phi_san["TuNgay"] <= ngay) &
                   (phi_san["DenNgay"].isna() | (phi_san["DenNgay"] >= ngay))]
    ket_qua = {"PhiXuLyGiaoDich": 0.0, "PhiCoDinh": 0.0, "PhiKhac": 0.0}
    tien_hang, giam, ship = so(don["TienHangGoc"]), so(don["GiamGiaCuaShop"]), so(don["PhiVanChuyenKhachTra"])
    for _, p in dong.iterrows():
        gia_tri = so(p["GiaTri"])
        if p["CachTinh"] == "Phần trăm tiền hàng":
            if pd.isna(p["MaDanhMuc"]) or p["MaDanhMuc"] is None:
                co_so = tien_hang - giam
            else:
                co_so = so(tien_danh_muc.get((don["MaDH"], p["MaDanhMuc"]), 0))
            tien = lam_tron(co_so * gia_tri / 100)
        elif p["CachTinh"] == "Phần trăm tiền khách trả":
            tien = lam_tron((tien_hang - giam + ship) * gia_tri / 100)
        else:
            tien = lam_tron(gia_tri)
        if p["LoaiPhi"] == "Phí xử lý giao dịch":
            ket_qua["PhiXuLyGiaoDich"] += tien
        elif p["LoaiPhi"] == "Phí cố định":
            ket_qua["PhiCoDinh"] += tien
        else:
            ket_qua["PhiKhac"] += tien
    return pd.Series(ket_qua)


def tao_tien_don_hang(b):
    dh = b["DonHang"].copy()
    ct = b["ChiTietDonHang"]
    tien_hang = (ct["SoLuong"] * ct["DonGia"]).groupby(ct["MaDH"]).sum()
    gia_von = (ct["SoLuong"] * ct["GiaVon"].fillna(0)).groupby(ct["MaDH"]).sum()
    th = b["TraHang"].merge(ct[["MaDH", "MaSP", "GiaVon"]], on=["MaDH", "MaSP"], how="left")
    th["GiaVonNhapLai"] = (th["SoLuong"] * th["GiaVon"].fillna(0)).where(th["TinhTrangHang"] == "Còn bán được", 0)
    hoan = th.groupby("MaDH").agg(TienHoan=("SoTienHoan", "sum"), GiaVonNhapLai=("GiaVonNhapLai", "sum"))

    dh["TienHangGoc"] = dh["MaDH"].map(tien_hang).fillna(0)
    dh = dh.merge(b["DoiTac"].rename(columns={"TenDoiTac": "KenhBan"}), on="MaDoiTac", how="left")
    dh["DaHuy"] = dh["TrangThai"].fillna("").str.contains("hủy")
    dh["DaGiao"] = dh["TrangThai"].isin(TRANG_THAI_DA_GIAO)
    ct_dm = ct.merge(b["SanPham"][["MaSP", "MaDanhMuc"]], on="MaSP", how="left")
    tien_danh_muc = (ct_dm["SoLuong"] * ct_dm["DonGia"]).groupby([ct_dm["MaDH"], ct_dm["MaDanhMuc"]]).sum().to_dict()
    phi = dh.apply(lambda d: phi_san_cua_don(d, b["PhiSan"], tien_danh_muc), axis=1)
    dh = pd.concat([dh, phi], axis=1)
    dh["PhiSan"] = dh["PhiXuLyGiaoDich"] + dh["PhiCoDinh"] + dh["PhiKhac"]

    giao = dh["DaGiao"]
    huy = dh["DaHuy"]
    dh["TienHang"] = dh["TienHangGoc"].where(~huy, 0)
    dh["GiamGia"] = dh["GiamGiaCuaShop"].where(~huy, 0)
    dh["KhachThanhToan"] = (dh["TienHangGoc"] - dh["GiamGiaCuaShop"] + dh["PhiVanChuyenKhachTra"]).where(~huy, 0)
    for c in ["PhiXuLyGiaoDich", "PhiCoDinh", "PhiKhac", "PhiSan"]:
        dh[c] = dh[c].where(giao, 0)
    dh["TienHoanKhach"] = dh["MaDH"].map(hoan["TienHoan"]).fillna(0).where(giao, 0)
    dh["TienShopNhan"] = (dh["TienHangGoc"] - dh["GiamGiaCuaShop"] - dh["PhiSan"] - dh["TienHoanKhach"]).where(giao, 0)
    dh["GiaVonHangBan"] = (dh["MaDH"].map(gia_von).fillna(0) - dh["MaDH"].map(hoan["GiaVonNhapLai"]).fillna(0)).where(giao, 0)
    dh["LaiGop"] = dh["TienShopNhan"] - dh["GiaVonHangBan"]
    cot = ["MaDH", "NgayDat", "KenhBan", "TrangThai", "TienHang", "GiamGia", "PhiVanChuyenKhachTra", "KhachThanhToan",
           "PhiXuLyGiaoDich", "PhiCoDinh", "PhiKhac", "PhiSan", "TienHoanKhach", "TienShopNhan", "GiaVonHangBan", "LaiGop"]
    return dh[cot].sort_values("NgayDat").reset_index(drop=True)


def tinh_so_kiem_tra(bh, ngay, tien_don):
    khong_huy = bh[bh["LaDonHuy"] == "Không"]
    doanh_thu = khong_huy["DoanhThu"].sum()
    so_don = khong_huy["MaDH"].nunique()
    don_huy = bh.loc[bh["LaDonHuy"] == "Có", "MaDH"].nunique()
    lai = khong_huy["LaiGop"].sum()
    theo_ngay = ngay.merge(khong_huy.groupby("Ngay")["DoanhThu"].sum().rename("DT"), on="Ngay", how="left").fillna({"DT": 0})
    sale = theo_ngay[theo_ngay["LaNgaySale"] == "Có"]
    thuong = theo_ngay[theo_ngay["LaNgaySale"] == "Không"]
    dong = [
        ("Doanh thu", doanh_thu, "BanHang: cộng cột DoanhThu (= SoLuong x DonGia của ChiTietDonHang), bỏ đơn có TrangThai chứa chữ \"hủy\""),
        ("Số đơn", so_don, "BanHang: đếm MaDH khác nhau của đơn không hủy"),
        ("Đơn hủy", don_huy, "BanHang: đếm MaDH khác nhau của đơn có TrangThai chứa chữ \"hủy\""),
        ("Giá trị 1 đơn", round(doanh_thu / so_don), "Doanh thu chia Số đơn"),
        ("% lãi gộp", round(100 * lai / doanh_thu, 1), "Cộng cột LaiGop (= DoanhThu - SoLuong x giá nhập bình quân) chia Doanh thu"),
        ("Doanh thu mỗi ngày sale", round(sale["DT"].sum() / len(sale)), "Doanh thu các ngày nằm trong 1 đợt của bảng DotSale, chia số ngày sale"),
        ("Doanh thu mỗi ngày thường", round(thuong["DT"].sum() / len(thuong)), "Doanh thu các ngày còn lại, chia số ngày thường"),
        ("Số ngày sale", len(sale), "Đếm ngày từ NgayBatDau đến NgayKetThuc của 12 đợt trong bảng DotSale"),
        ("Số ngày thường", len(thuong), "Số ngày trong khoảng dữ liệu trừ số ngày sale"),
    ]
    giao = tien_don[tien_don["TrangThai"].isin(TRANG_THAI_DA_GIAO)]
    dong += [
        ("Phí sàn (đơn đã giao)", tien_don["PhiSan"].sum(), "TienDonHang: cộng cột PhiSan, tính theo bảng PhiSan, chỉ cho đơn đã giao"),
        ("Tiền hoàn cho khách", tien_don["TienHoanKhach"].sum(), "TienDonHang: cộng SoTienHoan của bảng TraHang"),
        ("Shop thực nhận", tien_don["TienShopNhan"].sum(), "TienDonHang: tiền hàng - giảm giá của shop - phí sàn - tiền hoàn, chỉ đơn đã giao"),
        ("Lãi gộp sau phí sàn", tien_don["LaiGop"].sum(), "TienDonHang: shop thực nhận - giá vốn (không tính giá vốn hàng khách trả còn bán được)"),
        ("Số đơn đã giao", len(giao), "TienDonHang: đếm đơn có TrangThai là Đã giao hoặc Trả hàng/Hoàn tiền"),
    ]
    kq = pd.DataFrame(dong, columns=["ConSo", "GiaTri", "LayTuDau"])
    kq["SoTrongHuongDan"] = kq["ConSo"].map(SO_KIEM_TRA)

    def so_sanh(r):
        if pd.isna(r["SoTrongHuongDan"]):
            return "Số mới, so với màn Báo cáo doanh thu trên WinForms"
        return "Khớp" if abs(float(r["GiaTri"]) - float(r["SoTrongHuongDan"])) < 0.051 else "KHÔNG KHỚP"
    kq["SoSanh"] = kq.apply(so_sanh, axis=1)
    return kq


GIAI_THICH_COT = [
    ("BanHang", "MaDH, NgayDat, TrangThai", "Bảng DonHang"),
    ("BanHang", "KenhBan", "Bảng DoiTac, cột TenDoiTac (nối theo DonHang.MaDoiTac)"),
    ("BanHang", "MaKH, TenKH", "Bảng KhachHang (nối theo DonHang.MaKH)"),
    ("BanHang", "MaSP, SoLuong, DonGia", "Bảng ChiTietDonHang (mỗi dòng là 1 món trong đơn)"),
    ("BanHang", "TenSP, GiaBan, TenDanhMuc", "Bảng SanPham và DanhMuc"),
    ("BanHang", "LaNgaySale, TenDotSale", "Bảng DotSale: ngày đặt nằm giữa NgayBatDau và NgayKetThuc"),
    ("BanHang", "LaDonHuy", "\"Có\" khi DonHang.TrangThai chứa chữ \"hủy\""),
    ("BanHang", "ThanhTien", "SoLuong x DonGia"),
    ("BanHang", "DoanhThu", "Bằng ThanhTien, đơn hủy thì bằng 0"),
    ("BanHang", "GiaVon", "ChiTietDonHang.GiaVon = giá nhập bình quân = tổng (SoLuong x DonGia) của ChiTietPhieuNhap chia tổng SoLuong nhập"),
    ("BanHang", "TienVon, LaiGop", "TienVon = SoLuong x GiaVon (đơn hủy bằng 0); LaiGop = DoanhThu - TienVon"),
    ("BanHang", "SoLuongTra, TienHoan", "Bảng TraHang: cộng theo đơn và sản phẩm"),
    ("TienDonHang", "TienHang", "Cộng SoLuong x DonGia các món của đơn, đơn hủy bằng 0"),
    ("TienDonHang", "GiamGia, PhiVanChuyenKhachTra", "DonHang.GiamGiaCuaShop và DonHang.PhiVanChuyenKhachTra (dữ liệu mô phỏng: voucher shop ngày sale, phí ship khi chưa được miễn)"),
    ("TienDonHang", "PhiXuLyGiaoDich, PhiCoDinh, PhiKhac", "Bảng PhiSan: dòng cùng kênh bán, ngày đặt nằm trong TuNgay - DenNgay; dòng có MaDanhMuc chỉ tính trên tiền các món thuộc danh mục đó; chỉ tính khi đơn đã giao; làm tròn tới đồng"),
    ("TienDonHang", "TienHoanKhach", "Bảng TraHang, cột SoTienHoan"),
    ("TienDonHang", "TienShopNhan", "TienHang - GiamGia - PhiSan - TienHoanKhach, chỉ đơn đã giao"),
    ("TienDonHang", "GiaVonHangBan, LaiGop", "Giá vốn các món trừ giá vốn hàng khách trả còn bán được; LaiGop = TienShopNhan - GiaVonHangBan. Giống view vw_TienDonHang trong SQL"),
    ("Ngay", "LaNgaySale, TenDotSale", "Bảng DotSale"),
]


def main():
    p = argparse.ArgumentParser(description="Chỉ ra từng con số báo cáo lấy từ bảng nào, cột nào, tính ra sao.")
    nguon = p.add_mutually_exclusive_group(required=True)
    nguon.add_argument("--sqlserver", help="Chuỗi kết nối ODBC tới database QuanLyThuongMaiDienTu")
    nguon.add_argument("--sqlfile", help="Đường dẫn file QuanLyThuongMaiDienTu_Database.sql")
    p.add_argument("--thumuc", default=os.path.join(os.path.dirname(os.path.abspath(__file__)), "KetQua_NguonSoLieu"))
    a = p.parse_args()

    tong = 5
    if a.sqlserver:
        in_buoc(1, tong, "Đọc 12 bảng từ SQL Server")
        b = doc_tu_sql_server(a.sqlserver)
        cach_doc = "SQL Server"
    else:
        in_buoc(1, tong, "Đọc 12 bảng từ các lệnh INSERT trong file .sql")
        b = doc_tu_file_sql(a.sqlfile)
        print("        Không có SQL Server nên làm lại những việc SQL Server tự làm sau khi nạp dữ liệu:")
        print("        giá vốn = giá nhập bình quân, như phần \"TÍNH GIÁ VỐN CHO DỮ LIỆU MẪU\" trong file .sql.")
        b = lam_lai_buoc_sql_server_tu_lam(b)
        cach_doc = "file .sql"
    b = chuan_hoa_kieu(b)
    for ten in BANG_CAN_DOC:
        print(f"        {ten}: {len(b[ten])} dòng")

    in_buoc(2, tong, "Tạo bảng Ngay (lịch từng ngày, đánh dấu ngày sale)")
    tu = b["DonHang"]["NgayDat"].min().normalize()
    den = b["DonHang"]["NgayDat"].max().normalize()
    ngay = tao_bang_ngay(b, tu, den)

    in_buoc(3, tong, "Tạo bảng BanHang (mỗi dòng là 1 món trong 1 đơn)")
    bh = tao_ban_hang(b, ngay)

    in_buoc(4, tong, "Tạo bảng TienDonHang (tiền thực nhận và lãi từng đơn, giống view vw_TienDonHang)")
    tien_don = tao_tien_don_hang(b)

    in_buoc(5, tong, "Tính các con số kiểm tra và so với HUONG_DAN_POWER_BI.txt")
    kq = tinh_so_kiem_tra(bh, ngay, tien_don)

    os.makedirs(a.thumuc, exist_ok=True)
    file_excel = os.path.join(a.thumuc, "NguonSoLieu.xlsx")
    with pd.ExcelWriter(file_excel, engine="openpyxl") as w:
        kq.to_excel(w, sheet_name="SoKiemTra", index=False)
        pd.DataFrame(GIAI_THICH_COT, columns=["Bang", "Cot", "LayTuDau"]).to_excel(w, sheet_name="GiaiThichCot", index=False)
        bh.to_excel(w, sheet_name="BanHang", index=False)
        tien_don.to_excel(w, sheet_name="TienDonHang", index=False)
        ngay.to_excel(w, sheet_name="Ngay", index=False)
        b["PhiSan"].to_excel(w, sheet_name="PhiSan", index=False)

    print()
    print("=" * 100)
    print(f"CÁC CON SỐ VÀ NGUỒN - dữ liệu {tu:%d/%m/%Y} đến {den:%d/%m/%Y}, đọc từ {cach_doc}")
    print("=" * 100)
    for _, r in kq.iterrows():
        gia_tri = f"{r['GiaTri']:,.1f}" if r["ConSo"].startswith("%") else f"{r['GiaTri']:,.0f}"
        gia_tri = gia_tri.translate(str.maketrans(",.", ".,"))
        print(f"{r['ConSo']:<28}{gia_tri:>20}   [{r['SoSanh']}]")
        print(f"{'':<28}lấy từ: {r['LayTuDau']}")
    print()
    print("Trạng thái đơn:", ", ".join(f"{k} {v}" for k, v in b["DonHang"]["TrangThai"].value_counts().items()))
    print(f"Đã lưu: {file_excel}")
    if (kq["SoSanh"] == "KHÔNG KHỚP").any():
        print("Có số không khớp hướng dẫn: dữ liệu đã đổi (thêm/sửa đơn trên phần mềm) hoặc database chưa chạy lại file SQL.")
        sys.exit(1)


if __name__ == "__main__":
    main()
