using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SoThuChiCaNhan.frm.HoTro;

namespace SoThuChiCaNhan.frm.GiaoDienChinh;

public partial class frm_GiaoDienChinh : Form
{
    #region Hàm khởi tạo
    public frm_GiaoDienChinh()
    {
        InitializeComponent();
        run(); // Luồng chạy chính
    }
    #endregion

    #region Luồng chạy chính ứng dụng
    private void run()
    {
        run_MauSac(); // Màu sắc .MauSac
        run_KhoiTao(); // Khởi tạo chạy lần đầu
    }
    #endregion

    #region Khởi tạo mở ứng dụng lần đầu
    private void run_KhoiTao()
    {
        //
        // Mở trang mặc định
        //
        ChuanHoaTieuDe("Tổng quan");
        ChuyenTrang(TongQuan);

        //
        // Tiêu đề form
        //
        this.Text = "Sổ thu chi cá nhân thông minh";

        //
        // Kích hoạt nút của trang tổng quan
        //
        KichHoatNut(btn_TongQuan, EventArgs.Empty);
    }
    #endregion
}
