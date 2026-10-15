using Guna.UI2.WinForms;
using SoThuChiCaNhan.UI.User_Control.frm_GiaoDienChinh.TroLyAI.KhungTinNhan;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoThuChiCaNhan;

public partial class uc_TroLyAI
{
    public static int i = 0;
    private void ThemTinNhan(string TinNhan, bool NguoiDung)
    {
        UserControl p;
        if(NguoiDung) p = new uc_KhungTinNhanNguoiDung(TinNhan);
        else p = new uc_KhungTinNhanAI(TinNhan);

        p.Dock = DockStyle.Top;
        pnl_HienThi.Controls.Add(p);
        pnl_HienThi.Controls.SetChildIndex(p, 0);
    }
}
