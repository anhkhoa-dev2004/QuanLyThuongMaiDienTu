using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace QuanLy_BanHang_Kho_Shopee.System_Module
{
    public static class DbHelper
    {
        private static readonly string _connectionString =
            ConfigurationManager.ConnectionStrings["QuanLyThuongMaiDienTu"]?.ConnectionString
            ?? @"Data Source=.\SQLPHANTAN1;Initial Catalog=QuanLyThuongMaiDienTu;Integrated Security=True;TrustServerCertificate=True";

        public static SqlConnection TaoKetNoi()
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            conn.StateChange += GhiNguoiDangLam;
            return conn;
        }

        private static void GhiNguoiDangLam(object sender, StateChangeEventArgs e)
        {
            if (e.CurrentState != ConnectionState.Open || string.IsNullOrEmpty(Session.MaND)) return;
            SqlConnection conn = (SqlConnection)sender;
            using (SqlCommand cmd = new SqlCommand("EXEC sp_set_session_context @key = N'CurrentUserID', @value = @ma", conn))
            {
                cmd.Parameters.Add("@ma", SqlDbType.VarChar, 20).Value = Session.MaND;
                cmd.ExecuteNonQuery();
            }
        }
    }
}
