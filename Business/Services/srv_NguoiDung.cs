namespace SoThuChiCaNhan.Business.Services;

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

    public async Task<srv_KetQuaDangKy> DangKyAsync(dto_DangKy d)
    {
        if (string.IsNullOrWhiteSpace(d.hoTen) || string.IsNullOrWhiteSpace(d.tenDangNhap) || string.IsNullOrWhiteSpace(d.matKhau))
            return srv_KetQuaDangKy.DuLieuKhongHopLe;

        // Kiểm tra độ dài chuỗi
        if (d.tenDangNhap.Length > 50 || d.hoTen.Length > 100 || d.matKhau.Length > 255)
            return srv_KetQuaDangKy.DuLieuKhongHopLe;

        // Kiểm tra tồn tại người dùng trên hệ thống
        if (await dalNguoiDung.TimTheoTenDangNhap(d.tenDangNhap) != null)
            return srv_KetQuaDangKy.TaiKhoanDaTonTai;

        // Tạo đối tượng người dùng mới để phục vụ cho DAL
        mod_NguoiDung ngd = new()
        {
            HoTen = d.hoTen,
            TenDangNhap = d.tenDangNhap,
            MatKhau = BCrypt.Net.BCrypt.HashPassword(d.matKhau),
            SoDu = 0,
            AnhDaiDien = "default_avatar.png",
            NgayTao = DateTime.Now
        };

        return await dalNguoiDung.ThemNguoiDung(ngd)
            ? srv_KetQuaDangKy.ThanhCong
            : srv_KetQuaDangKy.ThatBai;
    }

    public async Task<(srv_KetQuaDangNhap kq, dto_NguoiDung? ngd)> DangNhapAsync(dto_DangNhap d)
    {
        if (string.IsNullOrWhiteSpace(d.tenDangNhap) || string.IsNullOrWhiteSpace(d.matKhau))
            return (srv_KetQuaDangNhap.DuLieuKhongHopLe, null);

        // Tìm kiếm người dùng trên csdl
        var ngd = await dalNguoiDung.TimTheoTenDangNhap(d.tenDangNhap);

        // Kiểm tra tài khoản tồn tại và khớp mật khẩu
        if (ngd == null || !BCrypt.Net.BCrypt.Verify(d.matKhau, ngd.MatKhau)) 
            return (srv_KetQuaDangNhap.SaiThongTin, null);

        // Khởi tạo DTO Người dùng cho UI sử dụng
        var dto_ngd = new dto_NguoiDung(
            ngd.MaNguoiDung,
            ngd.TenDangNhap,
            ngd.HoTen,
            ngd.SoDu,
            ngd.AnhDaiDien
        );

        // Trả về dữ liệu người dùng
        return (srv_KetQuaDangNhap.ThanhCong, dto_ngd);
    }
}
