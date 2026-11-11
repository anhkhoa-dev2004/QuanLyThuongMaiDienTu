USE QuanLyThuongMaiDienTu;
GO
SET NOEXEC OFF;
GO

DECLARE @Thieu NVARCHAR(MAX) = N'';
SELECT @Thieu = @Thieu + N'  - ' + Ten + NCHAR(13) + NCHAR(10)
FROM (VALUES
    (N'dbo.vw_TienDonHang'), (N'dbo.vw_XuatKhoBatThuong'), (N'dbo.vw_LaiRongTheoThang'), (N'dbo.vw_CongNoNhaCungCap'),
    (N'dbo.vw_DoiSoatTienSan'), (N'dbo.fn_NgayDuLieuMoiNhat'), (N'dbo.HoaDon'), (N'dbo.PhieuKiemKe'), (N'dbo.ChiTietKiemKe'),
    (N'dbo.ChiPhiVanHanh'), (N'dbo.BaoHanh'), (N'dbo.TienSanChuyenVe'), (N'dbo.ThanhToanNCC')
) d(Ten)
WHERE OBJECT_ID(Ten) IS NULL;

IF LEN(@Thieu) > 0 OR COL_LENGTH('dbo.HoaDon', 'LoaiHoaDon') IS NULL OR COL_LENGTH('dbo.NguoiDung', 'TrangThai') IS NULL
BEGIN
    PRINT N'=== CHƯA KIỂM TRA ĐƯỢC: database đang mở là bản CŨ, chưa phải bản mới nhất.';
    IF LEN(@Thieu) > 0 PRINT N'Database đang thiếu:' + NCHAR(13) + NCHAR(10) + @Thieu;
    PRINT N'Cách làm:';
    PRINT N'  1. Mở file QuanLyThuongMaiDienTu_Database.sql trong file zip mới nhất và bấm Execute.';
    PRINT N'  2. Đợi chạy hết (2 - 5 phút). Tab Messages phải có dòng "Đã tạo xong database QuanLyThuongMaiDienTu" và "=== XONG".';
    PRINT N'  3. Chạy lại file KiemTra_SauKhiChay.sql này.';
    SET NOEXEC ON;
END
GO

SET NOCOUNT ON;

DECLARE @KQ TABLE (STT INT IDENTITY(1,1), Muc NVARCHAR(200), GiaTriTrongDatabase DECIMAL(20,2), GiaTriPhaiRa DECIMAL(20,2), ChoPhepLech DECIMAL(20,2));

INSERT INTO @KQ (Muc, GiaTriTrongDatabase, GiaTriPhaiRa, ChoPhepLech)
SELECT N'Doanh thu (đơn không hủy)', SUM(TongTien), 3712302000, 0 FROM DonHang WHERE ISNULL(TrangThai, N'') NOT LIKE N'%hủy%'
UNION ALL SELECT N'Số đơn không hủy', COUNT(*), 1467, 0 FROM DonHang WHERE ISNULL(TrangThai, N'') NOT LIKE N'%hủy%'
UNION ALL SELECT N'Số đơn hủy', COUNT(*), 149, 0 FROM DonHang WHERE TrangThai = N'Đã hủy'
UNION ALL SELECT N'Đơn Đã giao', COUNT(*), 1361, 0 FROM DonHang WHERE TrangThai = N'Đã giao'
UNION ALL SELECT N'Đơn Trả hàng/Hoàn tiền', COUNT(*), 92, 0 FROM DonHang WHERE TrangThai = N'Trả hàng/Hoàn tiền'
UNION ALL SELECT N'Đơn Đang giao', COUNT(*), 10, 0 FROM DonHang WHERE TrangThai = N'Đang giao'
UNION ALL SELECT N'Đơn Chờ lấy hàng', COUNT(*), 3, 0 FROM DonHang WHERE TrangThai = N'Chờ lấy hàng'
UNION ALL SELECT N'Đơn Chờ xác nhận', COUNT(*), 1, 0 FROM DonHang WHERE TrangThai = N'Chờ xác nhận'
UNION ALL SELECT N'Phí sàn (vw_TienDonHang)', SUM(PhiSan), 286300555, 0 FROM dbo.vw_TienDonHang
UNION ALL SELECT N'Tiền hoàn khách', SUM(TienHoanKhach), 128361919, 0 FROM dbo.vw_TienDonHang
UNION ALL SELECT N'Shop thực nhận', SUM(TienShopNhan), 3268209526, 0 FROM dbo.vw_TienDonHang
UNION ALL SELECT N'Lãi sau phí sàn', SUM(LaiGop), 231114431.85, 1 FROM dbo.vw_TienDonHang
UNION ALL SELECT N'Hóa đơn bán hàng (số tờ)', COUNT(*), 1453, 0 FROM HoaDon WHERE LoaiHoaDon = N'Hóa đơn bán hàng'
UNION ALL SELECT N'Hóa đơn điều chỉnh giảm (số tờ)', COUNT(*), 92, 0 FROM HoaDon WHERE LoaiHoaDon = N'Điều chỉnh giảm'
UNION ALL SELECT N'Doanh thu theo hóa đơn (bán - điều chỉnh)', SUM(CASE WHEN LoaiHoaDon = N'Điều chỉnh giảm' THEN -TongTien ELSE TongTien END), 3554510081, 0 FROM HoaDon
UNION ALL SELECT N'Hóa đơn có tiền chưa thuế + thuế khác tổng tiền', COUNT(*), 0, 0 FROM HoaDon WHERE TienChuaThue + TienThue <> TongTien
UNION ALL SELECT N'Tổng tồn kho (cái, sau kiểm kê)', SUM(SoLuongTon), 202, 0 FROM TonKho
UNION ALL SELECT N'Dòng tồn kho bị âm', COUNT(*), 0, 0 FROM TonKho WHERE SoLuongTon < 0
UNION ALL SELECT N'Phiếu kiểm kê đã chốt', COUNT(*), 2, 0 FROM PhieuKiemKe WHERE TrangThai = N'Đã chốt'
UNION ALL SELECT N'Tổng lệch khi kiểm kê (cái)', SUM(ChenhLech), -3, 0 FROM ChiTietKiemKe
UNION ALL SELECT N'Phiếu xuất kho bất thường (vw_XuatKhoBatThuong, số dòng)', COUNT(*), (SELECT COUNT(*) FROM dbo.vw_XuatKhoBatThuong), 0 FROM dbo.vw_XuatKhoBatThuong
UNION ALL SELECT N'Chi phí vận hành cả năm (mô phỏng)', SUM(SoTien), 633613500, 0 FROM ChiPhiVanHanh
UNION ALL SELECT N'Hàng mất, hỏng (vw_LaiRongTheoThang)', SUM(HangMatHong), 55558386.01, 1 FROM dbo.vw_LaiRongTheoThang
UNION ALL SELECT N'Bảo hành shop chịu', SUM(ChiPhiBaoHanh), 840000, 0 FROM dbo.vw_LaiRongTheoThang
UNION ALL SELECT N'Lãi ròng cả năm', SUM(LaiRong), -458897454.16, 1 FROM dbo.vw_LaiRongTheoThang
UNION ALL SELECT N'Số tháng có lãi ròng dương', COUNT(*), 0, 0 FROM dbo.vw_LaiRongTheoThang WHERE LaiRong > 0
UNION ALL SELECT N'Phiếu bảo hành', COUNT(*), 32, 0 FROM BaoHanh
UNION ALL SELECT N'Phiếu bảo hành đang xử lý', COUNT(*), 2, 0 FROM BaoHanh WHERE TrangThai = N'Đang xử lý'
UNION ALL SELECT N'Còn nợ nhà cung cấp', SUM(ConNo), 218795000, 0 FROM dbo.vw_CongNoNhaCungCap
UNION ALL SELECT N'Đã trả nhà cung cấp', SUM(DaTra), 3167336000, 0 FROM dbo.vw_CongNoNhaCungCap
UNION ALL SELECT N'Tiền sàn đã chuyển về', SUM(SoTienSanTra), 2235282091, 0 FROM TienSanChuyenVe
UNION ALL SELECT N'Đối soát: đơn khớp', COUNT(*), 1044, 0 FROM dbo.vw_DoiSoatTienSan WHERE TinhTrang = N'Khớp'
UNION ALL SELECT N'Đối soát: sàn trả thiếu', COUNT(*), 2, 0 FROM dbo.vw_DoiSoatTienSan WHERE TinhTrang = N'Sàn trả thiếu'
UNION ALL SELECT N'Đối soát: quá 14 ngày chưa chuyển', COUNT(*), 1, 0 FROM dbo.vw_DoiSoatTienSan WHERE TinhTrang = N'Quá 14 ngày sàn chưa chuyển tiền'
UNION ALL SELECT N'Đối soát: chưa tới hạn', COUNT(*), 23, 0 FROM dbo.vw_DoiSoatTienSan WHERE TinhTrang = N'Sàn chưa chuyển (chưa tới hạn)'
UNION ALL SELECT N'Tài khoản đang dùng', COUNT(*), 3, 0 FROM NguoiDung WHERE TrangThai = N'Đang dùng'
UNION ALL SELECT N'Mốc dữ liệu mới nhất (năm*10000 + tháng*100 + ngày)', YEAR(dbo.fn_NgayDuLieuMoiNhat()) * 10000 + MONTH(dbo.fn_NgayDuLieuMoiNhat()) * 100 + DAY(dbo.fn_NgayDuLieuMoiNhat()), 20260930, 0;

DECLARE @Sai INT = (SELECT COUNT(*) FROM @KQ WHERE GiaTriTrongDatabase IS NULL OR ABS(GiaTriTrongDatabase - GiaTriPhaiRa) > ChoPhepLech);

SELECT STT, Muc AS [Mục kiểm tra], GiaTriTrongDatabase AS [Trong database], GiaTriPhaiRa AS [Phải ra],
       CASE WHEN GiaTriTrongDatabase IS NOT NULL AND ABS(GiaTriTrongDatabase - GiaTriPhaiRa) <= ChoPhepLech THEN N'ĐÚNG' ELSE N'SAI' END AS [Kết quả]
FROM @KQ ORDER BY STT;

DECLARE @Dong NVARCHAR(400), @i INT = 1, @n INT = (SELECT COUNT(*) FROM @KQ);
WHILE @i <= @n
BEGIN
    SELECT @Dong = CASE WHEN GiaTriTrongDatabase IS NOT NULL AND ABS(GiaTriTrongDatabase - GiaTriPhaiRa) <= ChoPhepLech THEN N'ĐÚNG  ' ELSE N'SAI   ' END
                   + Muc + N': ' + ISNULL(FORMAT(GiaTriTrongDatabase, 'N2', 'vi-VN'), N'(trống)') + N' (phải ra ' + FORMAT(GiaTriPhaiRa, 'N2', 'vi-VN') + N')'
    FROM @KQ WHERE STT = @i;
    PRINT @Dong;
    SET @i = @i + 1;
END

PRINT N'';
IF @Sai = 0
    PRINT N'=== TẤT CẢ ' + CAST(@n AS NVARCHAR(10)) + N' MỤC ĐỀU ĐÚNG.';
ELSE
    PRINT N'=== CÓ ' + CAST(@Sai AS NVARCHAR(10)) + N' MỤC SAI. Xem tab Messages lúc chạy file QuanLyThuongMaiDienTu_Database.sql có dòng lỗi nào không.';
GO
SET NOEXEC OFF;
GO