using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SoThuChiCaNhan;

public partial class frm_DangNhap : Form
{
    #region Hàm khởi tạo
    public frm_DangNhap()
    {
        InitializeComponent();
        run();
    }
    #endregion
    private void run()
    {
        //
        //
        //
        run_SuKien();

        //
        // Hiển thị trang UserControl Đăng nhập (không phải frm_DangNhap)
        //
        HienThiDangNhap(null, null);
    }
}
