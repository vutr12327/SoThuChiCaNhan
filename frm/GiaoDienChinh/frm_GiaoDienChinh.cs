#region Thư viện
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
#endregion
namespace SoThuChiCaNhan.frm.GiaoDienChinh;

public partial class frm_GiaoDienChinh : Form
{
    #region Hàm khởi tạo
    public frm_GiaoDienChinh()
    {
        InitializeComponent();
        run_MauSac(); // Màu sắc .MauSac
        run_SuKien(); // Đăng ký Sự kiện .SuKien
        run(); // Luồng chạy chính
    }
    #endregion

    #region Luồng chạy chính ứng dụng
    private void run()
    {

    }
    #endregion
}
