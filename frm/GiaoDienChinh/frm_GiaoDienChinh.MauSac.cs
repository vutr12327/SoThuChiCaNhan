using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SoThuChiCaNhan.frm.GiaoDienChinh;

/*
 * 
 *  MÀU SẮC
 * 
*/

public partial class frm_GiaoDienChinh
{
    #region Bộ màu sắc
    private readonly Color MauChinh = ColorTranslator.FromHtml("#4F46E5");
    #endregion

    #region Tuỳ chỉnh màu sắc thực thể
    private void run_MauSac()
    {
        //
        //  pnl_TrenCung
        //
        pnl_TrenCung.BackColor = MauChinh;

        //
        //  Bộ đóng/ thu nhỏ/ mở rộng cửa sổ
        //
        guna2ControlBox3.FillColor = MauChinh;
        guna2ControlBox2.FillColor = MauChinh;
        guna2ControlBox1.FillColor = MauChinh;

        //
        // Bộ nút (thực đơn) thanh bên bên trái
        //
        foreach(Control crl  in guna2Panel2.Controls)
        {
            if(crl is Guna2Button btn)
            {
                crl.ForeColor = MauChinh;
            }
        }

        //
        // htmllb_TenNguoiDung
        //
        htmllb_TenNguoiDung.Text = $"<div style=\"color: gray;\">Người dùng</div>\r\n<div style=\"font-weight: bold; color: {ColorTranslator.ToHtml(MauChinh)};\">Trần Nguyên Vũ</div>\r\n";
    }
    #endregion
}
