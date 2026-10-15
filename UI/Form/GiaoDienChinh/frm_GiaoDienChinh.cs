using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SoThuChiCaNhan;

public partial class frm_GiaoDienChinh : Form
{
    public frm_GiaoDienChinh()
    {
        InitializeComponent();
        run();
    }

    private void run()
    {
        //
        //
        //
        run_MauSac();
        run_SuKien();

        //
        // Nạp dữ liệu người dùng từ Session
        //
        NapDuLieuTuSession();

        //
        // Mở trang mặc định
        //
        ChuyenTrang(TroLyAI, "Trợ lý của bạn");

        //
        // Tiêu đề form
        //
        this.Text = "Sổ thu chi cá nhân thông minh";

        //
        // Kích hoạt nút của trang tổng quan
        //
        KichHoatNut(btn_TongQuan, EventArgs.Empty);
    }
}