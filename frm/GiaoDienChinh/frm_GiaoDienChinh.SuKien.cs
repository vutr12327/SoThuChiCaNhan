using SoThuChiCaNhan.frm.HoTro;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoThuChiCaNhan.frm.GiaoDienChinh;

/*
 * 
 *  SỰ KIỆN
 * 
*/

public partial class frm_GiaoDienChinh
{
    private void btn_TongQuan_Click(object sender, EventArgs e)
    {
        ChuanHoaTieuDe("Tổng quan");
        ChuyenTrang(TongQuan);
    }

    private void btn_TroLyCuaBan_Click(object sender, EventArgs e)
    {
        ChuanHoaTieuDe("Trợ lý của bạn");
        KichHoatNut(btn_TroChuyenVoiTroLy, EventArgs.Empty);
        ChuyenTrang(TroLyAI);
    }

    private void btn_TroChuyenVoiTroLy_Click(object sender, EventArgs e)
    {
        ChuanHoaTieuDe("Trợ lý của bạn");
        ChuyenTrang(TroLyAI);
    }

    private void btn_LichSuGiaoDich_Click(object sender, EventArgs e)
    {
        ChuanHoaTieuDe("Lịch sử giao dịch");
        ChuyenTrang(LichSuGiaoDich);
    }
}
