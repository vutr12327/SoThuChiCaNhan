using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SoThuChiCaNhan;

public class mainAppContext : ApplicationContext
{
    private static frm_DangNhap frmDangNhap;
    private static frm_GiaoDienChinh frmGiaoDienChinh;

    public mainAppContext()
    {
        HienThiFormDangNhap();
    }

    public static void HienThiFormDangNhap()
    {
        // Đóng giao diện chính nếu đang mở
        if (frmGiaoDienChinh != null && !frmGiaoDienChinh.IsDisposed)
        {
            frmGiaoDienChinh.FormClosed -= FrmGiaoDienChinh_FormClosed;
            frmGiaoDienChinh.Close();
            frmGiaoDienChinh.Dispose();
        }
        
        Session.DangXuat();
        frmDangNhap = new frm_DangNhap();
        frmDangNhap.FormClosed += ThoatChuongTrinh;
        frmDangNhap.Show();
    }

    public static void HienThiGiaoDienChinh()
    {
        // Đóng Đăng nhập nếu đang mở
        if (frmDangNhap != null && !frmDangNhap.IsDisposed)
        {
            frmDangNhap.FormClosed -= ThoatChuongTrinh;
            frmDangNhap.Close();
            frmDangNhap.Dispose();
        }
        frmGiaoDienChinh = new frm_GiaoDienChinh();
        frmGiaoDienChinh.FormClosed += FrmGiaoDienChinh_FormClosed;
        frmGiaoDienChinh.Show();
    }

    private static void ThoatChuongTrinh(object sender, FormClosedEventArgs e)
    {
        Application.ExitThread();
    }

    private static void FrmGiaoDienChinh_FormClosed(object sender, FormClosedEventArgs e)
    {
        HienThiFormDangNhap();
    }
}