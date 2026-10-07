using System;
using System.Collections.Generic;
using System.Text;

/*
 * 
 *  SỰ KIỆN
 * 
*/

namespace SoThuChiCaNhan;

public partial class uc_DangKy
{
    private void tb_MatKhau_IconRightClick(object sender, EventArgs e)
    {
        if (tb_MatKhau.Tag == "hidden")
        {
            tb_MatKhau.IconRight = Properties.Resources.eye;
            tb_MatKhau.UseSystemPasswordChar = false;
            tb_MatKhau.Tag = "shown";
        }
        else
        {
            tb_MatKhau.IconRight = Properties.Resources.hidden;
            tb_MatKhau.UseSystemPasswordChar = true;
            tb_MatKhau.Tag = "hidden";
        }
    }
    public event EventHandler OnChuyenSangDangNhap;
    private void btn_csDangNhap_Click(object sender, EventArgs e)
    {
        OnChuyenSangDangNhap.Invoke(this, EventArgs.Empty);
        tb_MatKhau.Text = "";
    }

    public event Action<string, string, string> OnDangKy;
    private void btn_DangKy_Click(object sender, EventArgs e)
    {
        OnDangKy.Invoke(tb_TenNguoiDung.Text, tb_TaiKhoan.Text, tb_MatKhau.Text);
    }
}
