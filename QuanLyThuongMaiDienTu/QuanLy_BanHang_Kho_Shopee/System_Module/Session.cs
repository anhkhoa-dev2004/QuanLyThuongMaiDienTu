namespace QuanLy_BanHang_Kho_Shopee.System_Module
{
    public static class Session
    {
        public static string MaND { get; set; }
        public static string HoTen { get; set; }
        public static string SoDT { get; set; }
        public static string TenQuyen { get; set; }

        public static bool QuyenKho { get; set; }
        public static bool QuyenBanHang { get; set; }
        public static bool QuyenBaoCao { get; set; }

        public static bool DaDangNhap { get; set; }

        public static void Clear()
        {
            MaND = null;
            HoTen = null;
            SoDT = null;
            TenQuyen = null;
            QuyenKho = false;
            QuyenBanHang = false;
            QuyenBaoCao = false;
            DaDangNhap = false;
        }
    }
}
