using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection.Emit;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace SoThuChiCaNhan.frm.GiaoDienChinh;

/*
 * 
 *  MÀU SẮC / HIỆU ỨNG / PHONG CÁCH
 * 
*/

public partial class frm_GiaoDienChinh
{
    #region Bộ màu sắc (https://rgb.vn/color-schemes-53-bang-phoi-mau-dep-goi-y-cho-cac-thiet-ke-cua-designer/)
    private readonly Color MauChinh = ColorTranslator.FromHtml("#000181");
    private readonly Color MauPhu = ColorTranslator.FromHtml("#1974D3");
    #endregion

    #region Tuỳ chỉnh màu sắc thực thể
    private void run_MauSac()
    {
        //
        // Thanh Bên (pnl_ThanhBen)
        //
        pnl_ThanhBen.BackColor = MauChinh;

        //
        // Thanh trên cùng (pnl_TrenCung)
        //
        pnl_TrenCung.BackColor = MauPhu;

        //
        // Hiệu ứng Active menu thanh bên
        //
        foreach (Control c in guna2Panel2.Controls)
        {
            if (c is Guna2Button btn)
            {
                btn.Click -= KichHoatNut; // Huỷ đăng ký
                btn.Click += KichHoatNut; // Đăng ký
            }
        }
    }
    #endregion

    #region Sự kiện hiệu ứng
    //
    // Sự kiện hiệu ứng Active menu thanh bên
    //
    private void KichHoatNut(object sender, EventArgs e)
    {
        foreach (Control c in guna2Panel2.Controls)
        {
            if (c is Guna2Button btn)
            {
                if(btn == (Guna2Button)sender)
                {
                    btn.FillColor = Color.FromArgb(90, 0, 0, 0);
                    btn.ForeColor = Color.White;
                } 
                else
                {
                    btn.FillColor = Color.Transparent;
                    btn.ForeColor = Color.FromArgb(200, 255, 255, 255);
                }
            }
        }
    }

    #endregion
}
