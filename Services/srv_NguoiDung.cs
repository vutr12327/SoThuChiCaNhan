namespace SoThuChiCaNhan;

public enum srv_KetQuaDangKy
{
    ThanhCong,
    DuLieuKhongHopLe,
    TaiKhoanDaTonTai,
    ThatBai
}

public enum srv_KetQuaDangNhap
{
    ThanhCong,
    DuLieuKhongHopLe,
    SaiThongTin
}

/// <summary>
/// Kết quả xử lý đổi mật khẩu.
/// </summary>
public enum srv_KetQuaDoiMatKhau
{
    ThanhCong,
    ThatBai,
    DuLieuKhongHopLe,
    SaiThongTin
}

public class srv_NguoiDung
{
    // Đăng ký lớp lấy dữ liệu từ csdl
    private readonly dal_NguoiDung dalNguoiDung = new();

    public async Task<srv_KetQuaDoiMatKhau> DoiMatKhauAsync(int maNguoiDung, string matKhauCu, string matKhauMoi)
    {
        if (maNguoiDung <= 0 || string.IsNullOrWhiteSpace(matKhauCu) || string.IsNullOrWhiteSpace(matKhauMoi))
            return srv_KetQuaDoiMatKhau.DuLieuKhongHopLe;

        // Lấy thông tin người dùng
        var ngd = await dalNguoiDung.TimTheoMaNguoiDung(maNguoiDung);
        if (ngd == null) return srv_KetQuaDoiMatKhau.SaiThongTin;

        // Kiểm tra khớp mật khẩu cũ -> Để xác nhận chính xác chủ tài khoản
        if (!BCrypt.Net.BCrypt.Verify(matKhauCu, ngd.MatKhau))
            return srv_KetQuaDoiMatKhau.SaiThongTin;

        // Cập nhật mật khẩu người dùng
        return await dalNguoiDung.DoiMatKhau(maNguoiDung, BCrypt.Net.BCrypt.HashPassword(matKhauMoi))
            ? srv_KetQuaDoiMatKhau.ThanhCong
            : srv_KetQuaDoiMatKhau.ThatBai;
    }

    public async Task<srv_KetQuaDangKy> DangKyAsync(string hoTen, string tenDangNhap, string matKhau)
    {
        if (string.IsNullOrWhiteSpace(hoTen) || string.IsNullOrWhiteSpace(tenDangNhap) || string.IsNullOrWhiteSpace(matKhau))
            return srv_KetQuaDangKy.DuLieuKhongHopLe;

        // Kiểm tra độ dài chuỗi
        if (tenDangNhap.Length > 50 || hoTen.Length > 100 || matKhau.Length > 255)
            return srv_KetQuaDangKy.DuLieuKhongHopLe;

        // Kiểm tra tồn tại người dùng trên hệ thống
        if (await dalNguoiDung.TimTheoTenDangNhap(tenDangNhap) != null)
            return srv_KetQuaDangKy.TaiKhoanDaTonTai;

        // Tạo đối tượng người dùng mới
        mod_NguoiDung ngd = new()
        {
            HoTen = hoTen,
            TenDangNhap = tenDangNhap,
            MatKhau = BCrypt.Net.BCrypt.HashPassword(matKhau),
            SoDu = 0,
            AnhDaiDien = "default_avatar.png",
            NgayTao = DateTime.Now
        };

        return await dalNguoiDung.ThemNguoiDung(ngd)
            ? srv_KetQuaDangKy.ThanhCong
            : srv_KetQuaDangKy.ThatBai;
    }

    public async Task<(srv_KetQuaDangNhap kq, mod_NguoiDung? ngd)> DangNhapAsync(string tenDangNhap, string matKhau)
    {
        if (string.IsNullOrWhiteSpace(tenDangNhap) || string.IsNullOrWhiteSpace(matKhau))
            return (srv_KetQuaDangNhap.DuLieuKhongHopLe, null);

        // Tìm kiếm người dùng trên csdl
        var ngd = await dalNguoiDung.TimTheoTenDangNhap(tenDangNhap);

        // Trả về dữ liệu người dùng
        return (ngd == null || !BCrypt.Net.BCrypt.Verify(matKhau, ngd.MatKhau)) 
            ? (srv_KetQuaDangNhap.SaiThongTin, null)
            : (srv_KetQuaDangNhap.ThanhCong, ngd);
    }
}
