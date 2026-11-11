using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLy_BanHang_Kho_Shopee.System_Module;
using QuanLy_BanHang_Kho_Shopee.Service_Module;
using QuanLy_BanHang_Kho_Shopee.Report_Module;
using QuanLy_BanHang_Kho_Shopee.Warehouse_Module;
using QuanLy_BanHang_Kho_Shopee.Sales_Module;
using QuanLy_BanHang_Kho_Shopee.Category_Module;

namespace QuanLy_BanHang_Kho_Shopee
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (s, e) => QuanLy_BanHang_Kho_Shopee.AI_Module.AiHelper.BaoLoi(e.Exception);
            Application.Run(new Form_DangNhap());
        }
    }
}
