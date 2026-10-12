using System;
using System.Collections.Generic;
using System.Text;

namespace SoThuChiCaNhan;

/*
 * 
 *  SỰ KIỆN
 * 
*/

public partial class frm_GiaoDienChinh
{

    void run_SuKien()
    {

    }

    //
    // Các sự kiện chuyển trang
    //
    private void btn_TongQuan_Click(object sender, EventArgs e) => ChuyenTrang(TongQuan, "Tổng quan");
    private void btn_LichSuGiaoDich_Click(object sender, EventArgs e) => ChuyenTrang(LichSuGiaoDich, "Lịch sử giao dịch");
    private void btn_ThongTinNguoiDung_Click(object sender, EventArgs e) => ChuyenTrang(CaNhan, "Thông tin cá nhân");
    private void btn_TroChuyenVoiTroLy_Click(object sender, EventArgs e) => ChuyenTrang(TroLyAI, "Trợ lý của bạn");
    private void btn_DanhMucCuaBan_Click(object sender, EventArgs e) => ChuyenTrang(DanhMucCuaBan, "Danh mục của bạn");
    private void btn_TroLyCuaBan_Click(object sender, EventArgs e)
    {
        KichHoatNut(btn_TroChuyenVoiTroLy, EventArgs.Empty);
        ChuyenTrang(TroLyAI, "Trợ lý của bạn");
    }

    //
    // Sự kiện đăng xuất
    //
    private void btn_DangXuat_Click(object sender, EventArgs e) => mainAppContext.HienThiFormDangNhap();
}
