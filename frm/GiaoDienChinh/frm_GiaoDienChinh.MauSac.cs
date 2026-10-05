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
        // Tiêu đề trang (usercontrol title)
        //
        hlb_TieuDeTrang.Text = string.Join("\t", hlb_TieuDeTrang.Text.ToUpper().ToCharArray());

        //
        // htmllb_TenNguoiDung
        //
        //htmllb_TenNguoiDung.Text = $"<div style=\"color: gray;\">Người dùng</div>\r\n<div style=\"font-weight: bold; color: {ColorTranslator.ToHtml(MauChinh)};\">Trần Nguyên Vũ</div>\r\n";
    }
    #endregion

    #region Sự kiện: Hiệu ứng

    #endregion
}
