using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SoThuChiCaNhan;

public partial class uc_CaNhan : UserControl
{
    public uc_CaNhan()
    {
        InitializeComponent();
        run_KhoiTao();
    }
    private void run_KhoiTao()
    {
        //
        //
        //
        lbl_HoTen.Text = $"Xin chào {Session.NguoiDung.HoTen}!";
        lbl_UID.Text = $"UID: {Session.NguoiDung.MaNguoiDung}";
        tb_HoTen.Text = Session.NguoiDung.HoTen;
        tb_TenDangNhap.Text = Session.NguoiDung.TenDangNhap;

    }
    private void btn_ThayDoiMatKhau_Click(object sender, EventArgs e)
    {
        ThayDoiMatKhau t = new ThayDoiMatKhau();
        t.Show();
    }
}
