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
/// Kết quả xử lý đổi một thông tin người dùng
/// </summary>
public enum srv_ThayDoiThongTin
{
    ThanhCong,
    ThatBai,
    DuLieuKhongHopLe,
    SaiThongTin,
    KhongTonTaiNguoiDung
}

public class srv_NguoiDung
{
    // Đăng ký lớp lấy dữ liệu từ csdl
    private readonly dal_NguoiDung dalNguoiDung = new();

    /// <summary>
    /// Phương thức đăng ký người dùng
    /// </summary>
    /// <returns></returns>
    public async Task<srv_KetQuaDangKy> DangKyAsync(dto_DangKy d)
    {
        if (string.IsNullOrWhiteSpace(d.HoTen) || string.IsNullOrWhiteSpace(d.TenDangNhap) || string.IsNullOrWhiteSpace(d.MatKhau))
            return srv_KetQuaDangKy.DuLieuKhongHopLe;

        // Kiểm tra độ dài chuỗi
        if (d.TenDangNhap.Length > 50 || d.HoTen.Length > 100 || d.MatKhau.Length > 255)
            return srv_KetQuaDangKy.DuLieuKhongHopLe;

        // Kiểm tra tồn tại người dùng trên hệ thống
        if (await dalNguoiDung.TimTheoTenDangNhap(d.TenDangNhap) != null)
            return srv_KetQuaDangKy.TaiKhoanDaTonTai;

        // Tạo đối tượng người dùng mới để phục vụ cho DAL
        mod_NguoiDung ngd = new()
        {
            HoTen = d.HoTen,
            TenDangNhap = d.TenDangNhap,
            MatKhau = BCrypt.Net.BCrypt.HashPassword(d.MatKhau),
            SoDu = 0,
            AnhDaiDien = "default_avatar.png",
            NgayTao = DateTime.Now
        };

        return await dalNguoiDung.ThemNguoiDung(ngd)
            ? srv_KetQuaDangKy.ThanhCong
            : srv_KetQuaDangKy.ThatBai;
    }

    /// <summary>
    /// Phương thức đăng nhập
    /// </summary>
    /// <param name="d"></param>
    /// <returns></returns>
    public async Task<(srv_KetQuaDangNhap kq, dto_NguoiDung? ngd)> DangNhapAsync(dto_DangNhap d)
    {
        if (string.IsNullOrWhiteSpace(d.TenDangNhap) || string.IsNullOrWhiteSpace(d.MatKhau))
            return (srv_KetQuaDangNhap.DuLieuKhongHopLe, null);

        // Tìm kiếm người dùng trên csdl
        var ngd = await dalNguoiDung.TimTheoTenDangNhap(d.TenDangNhap);

        // Kiểm tra tài khoản tồn tại và khớp mật khẩu
        if (ngd == null || !BCrypt.Net.BCrypt.Verify(d.MatKhau, ngd.MatKhau)) 
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
    /// <summary>
    /// Phương thức đổi mật khẩu
    /// </summary>
    /// <param name="d"></param>
    /// <returns></returns>
    public async Task<srv_ThayDoiThongTin> DoiMatKhauAsync(dto_DoiMatKhau d)
    {
        if (d.MaNguoiDung <= 0 || string.IsNullOrWhiteSpace(d.MatKhauCu) || string.IsNullOrWhiteSpace(d.MatKhauMoi))
            return srv_ThayDoiThongTin.DuLieuKhongHopLe;

        // Lấy thông tin người dùng
        var ngd = await dalNguoiDung.TimTheoMaNguoiDung(d.MaNguoiDung);
        if (ngd == null) return srv_ThayDoiThongTin.SaiThongTin;

        // Kiểm tra khớp mật khẩu cũ -> Để xác nhận chính xác chủ tài khoản
        if (!BCrypt.Net.BCrypt.Verify(d.MatKhauCu, ngd.MatKhau))
            return srv_ThayDoiThongTin.SaiThongTin;

        // Cập nhật mật khẩu người dùng
        return await dalNguoiDung.DoiMatKhau(d.MaNguoiDung, BCrypt.Net.BCrypt.HashPassword(d.MatKhauMoi))
            ? srv_ThayDoiThongTin.ThanhCong
            : srv_ThayDoiThongTin.ThatBai;
    }
    /// <summary>
    /// Phương thức đổi họ tên
    /// </summary>
    /// <param name="d"></param>
    /// <returns></returns>
    public async Task<srv_ThayDoiThongTin> DoiHoTenAsync(dto_DoiHoTen d)
    {
        if (string.IsNullOrWhiteSpace(d.HoTen))
            return srv_ThayDoiThongTin.DuLieuKhongHopLe;

        if (d.HoTen.Length > 100) 
            return srv_ThayDoiThongTin.DuLieuKhongHopLe;

        if (await dalNguoiDung.TimTheoMaNguoiDung(d.MaNguoiDung) == null)
            return srv_ThayDoiThongTin.KhongTonTaiNguoiDung;

        return await dalNguoiDung.DoiHoTen(d.MaNguoiDung, d.HoTen)
            ? srv_ThayDoiThongTin.ThanhCong
            : srv_ThayDoiThongTin.ThatBai;
    }
    /// <summary>
    /// Phương thức đổi ảnh đại diện
    /// </summary>
    /// <param name="d"></param>
    /// <returns></returns>
    public async Task<srv_ThayDoiThongTin> DoiAnhDaiDienAsync(dto_DoiAnhDaiDien d)
    {
        if (string.IsNullOrWhiteSpace(d.AnhDaiDien))
            return srv_ThayDoiThongTin.DuLieuKhongHopLe;

        if (await dalNguoiDung.TimTheoMaNguoiDung(d.MaNguoiDung) == null)
            return srv_ThayDoiThongTin.KhongTonTaiNguoiDung;

        return await dalNguoiDung.DoiHoTen(d.MaNguoiDung, d.AnhDaiDien)
            ? srv_ThayDoiThongTin.ThanhCong
            : srv_ThayDoiThongTin.ThatBai;
    }
}
