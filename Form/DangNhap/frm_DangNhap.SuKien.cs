using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoThuChiCaNhan;

public partial class frm_DangNhap
{
    private void run_SuKien()
    {
        //
        // Sự kiện hiển thị qua lại trang đăng nhập, đăng ký
        //
        TrDangNhap.OnChuyenSangDangKy += HienThiDangKy;
        TrDangKy.OnChuyenSangDangNhap += HienThiDangNhap;

        //
        // Sự kiện đăng nhập, đăng ký
        //
        TrDangNhap.OnDangNhap += DangNhapTaiKhoan;
        TrDangKy.OnDangKy += DangKyTaiKhoan;

    }

    private void DangKyTaiKhoan(string hoTen, string taiKhoan, string matKhau)
    {
        srv_KetQuaDangKy ketQua = srvNguoiDung.DangKy(hoTen, taiKhoan, matKhau);

        switch (ketQua)
        {
            case srv_KetQuaDangKy.ThanhCong:
                MessageBox.Show("Đăng ký tài khoản thành công! Vui lòng đăng nhập.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                HienThiDangNhap(null, null);
                break;

            case srv_KetQuaDangKy.DuLieuKhongHopLe:
                MessageBox.Show("Vui lòng nhập đúng thông tin đăng ký. Tên đăng nhập tối đa 50 ký tự, họ tên tối đa 100 ký tự và mật khẩu tối đa 255 ký tự.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                break;

            case srv_KetQuaDangKy.TaiKhoanDaTonTai:
                MessageBox.Show("Tên đăng nhập này đã tồn tại. Vui lòng chọn tên khác!", "Lỗi đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Error);
                break;

            case srv_KetQuaDangKy.ThatBai:
            default:
                MessageBox.Show("Đăng ký thất bại. Đã có lỗi xảy ra!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                break;
        }
    }
    private void DangNhapTaiKhoan(string taiKhoan, string matKhau)
    {
        srv_KetQuaDangNhap ketQua = srvNguoiDung.DangNhap(taiKhoan, matKhau, out mod_NguoiDung ngd);

        switch (ketQua)
        {
            case srv_KetQuaDangNhap.ThanhCong:
                Session.ngd = ngd; // Lưu trữ dữ liệu toàn cục để sử dụng
                MessageBox.Show($"Đăng nhập thành công! Chào mừng {ngd.HoTen}.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                VaoGiaoDienChinh();
                break;

            case srv_KetQuaDangNhap.DuLieuKhongHopLe:
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                break;

            case srv_KetQuaDangNhap.SaiThongTin:
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                break;

            default:
                MessageBox.Show("Đã xảy ra lỗi không xác định. Vui lòng thử lại sau!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                break;
        }
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
