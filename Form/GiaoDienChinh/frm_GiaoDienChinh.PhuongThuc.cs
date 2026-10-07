using Guna.UI2.WinForms;
using SoThuChiCaNhan;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoThuChiCaNhan;

public partial class frm_GiaoDienChinh
{
    private void ChuyenTrang(UserControl ucCanHien)
    {
        TrangDon.HienTrang(pnl_HienThi, ucCanHien);
    }

    private void ChuanHoaTieuDe(string x)
    {
        string ans = "";

        for (int i = 0; i < x.Count(); i++)
            ans += $"<span style='padding-left: 3px'>{x[i]}</span>";

        hlb_TieuDeTrang.Text = ans.ToUpper();
    }
}
