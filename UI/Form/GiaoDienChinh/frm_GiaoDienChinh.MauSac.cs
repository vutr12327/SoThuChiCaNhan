using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection.Emit;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace SoThuChiCaNhan;

/*
 * 
 *  MÀU SẮC / HIỆU ỨNG / PHONG CÁCH
 * 
*/

public partial class frm_GiaoDienChinh
{
    #region Bộ màu sắc (https://rgb.vn/color-schemes-53-bang-phoi-mau-dep-goi-y-cho-cac-thiet-ke-cua-designer/)
    private readonly Color MauChinh = ColorTranslator.FromHtml("MidnightBlue");
    private readonly Color MauPhu = ColorTranslator.FromHtml("#1974D3");
    #endregion

    #region Tuỳ chỉnh màu sắc thực thể
    private void run_MauSac()
    {
        //
        // Màu chính
        //
        pnl_ThanhBen.BackColor = MauChinh;
        btn_TroLyCuaBan.FillColor = MauChinh;

        //
        // Màu phụ
        //
        pnl_TrenCung.BackColor = MauPhu;
        btn_ThongTinNguoiDung.FillColor = MauPhu;

        //
        // Hiệu ứng Active menu thanh bên
        //
        foreach (Control c in guna2Panel2.Controls)
        {
            if (c is Guna2Button btn)
            {
                btn.Click -= KichHoatNut;
                btn.Click += KichHoatNut;
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
                    btn.ForeColor = Color.LightGray;
                }
            }
        }
    }
    #endregion
}
