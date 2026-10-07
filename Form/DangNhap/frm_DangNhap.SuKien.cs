using System;
using System.Collections.Generic;
using System.Text;

namespace SoThuChiCaNhan;

public partial class frm_DangNhap
{
    private void run_SuKien()
    {
        //
        // Đăng ký sự kiện hiển thị qua lại trang đăng nhập, đăng ký
        //
        TrDangNhap.OnChuyenSangDangKy += HienThiDangKy;
        TrDangKy.OnChuyenSangDangNhap += HienThiDangNhap;
    }
    private void HienThiDangNhap(object sender, EventArgs e)
    {
        ChuyenTrang(TrDangNhap);
    }

    private void HienThiDangKy(object sender, EventArgs e)
    {
        ChuyenTrang(TrDangKy);
    }
}
