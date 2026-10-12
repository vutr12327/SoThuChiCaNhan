using System;
using System.Collections.Generic;
using System.Text;

namespace SoThuChiCaNhan;

/*
 * 
 *  SỰ KIỆN
 * 
*/

public partial class uc_DangNhap
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

    public event EventHandler OnChuyenSangDangKy;
    private void btn_csDangKy_Click(object sender, EventArgs e)
    {
        OnChuyenSangDangKy.Invoke(this, EventArgs.Empty);
        tb_MatKhau.Text = "";
    }

    public event Action<string, string> OnDangNhap;
    private void btn_DangNhap_Click(object sender, EventArgs e)
    {
        OnDangNhap.Invoke(tb_TaiKhoan.Text, tb_MatKhau.Text);
    }
}
