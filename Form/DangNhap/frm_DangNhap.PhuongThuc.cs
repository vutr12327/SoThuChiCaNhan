using SoThuChiCaNhan;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoThuChiCaNhan;

public partial class frm_DangNhap
{
    private void ChuyenTrang(UserControl ucCanHien)
    {
        TrangDon.HienTrang(pnl_HienThi, ucCanHien);
    }

    private void VaoGiaoDienChinh()
    {
        frm_GiaoDienChinh main = new frm_GiaoDienChinh();
        this.Hide();
        main.ShowDialog();
        this.Close();
    }
}
