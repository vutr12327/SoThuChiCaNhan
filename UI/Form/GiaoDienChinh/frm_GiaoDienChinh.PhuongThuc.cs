using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoThuChiCaNhan;

public partial class frm_GiaoDienChinh
{
    private void NapDuLieuTuSession()
    {
        hlb_TenNguoiDung.Text = Session.NguoiDung.HoTen;
        hlb_idNguoiDung.Text = $"UID: {Session.NguoiDung.MaNguoiDung}";
    }

    private void ChuyenTrang(UserControl ucCanHien, string tieuDe)
    {
        string ans = "";

        for (int i = 0; i < tieuDe.Count(); i++)
            ans += $"<span style='padding-left: 3px'>{tieuDe[i]}</span>";

        hlb_TieuDeTrang.Text = ans.ToUpper();

        TrangDon.HienTrang(pnl_HienThi, ucCanHien);
    }
}
