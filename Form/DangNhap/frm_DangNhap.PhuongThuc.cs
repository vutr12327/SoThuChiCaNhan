using System;
using System.Collections.Generic;
using System.Text;

namespace SoThuChiCaNhan;

public partial class frm_DangNhap
{
    private void ChuyenTrang(UserControl ucCanHien)
    {
        if (ucCanHien == null) return;
        if (pnl_HienThi.Controls.Contains(ucCanHien)) return;
        TrangDon.HienTrang(pnl_HienThi, ucCanHien);
    }
}
