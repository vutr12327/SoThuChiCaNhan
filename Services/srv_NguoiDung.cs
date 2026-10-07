namespace SoThuChiCaNhan;

public enum srv_KetQuaDangKy // enum để trả kết quả tuỳ chỉnh
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

public class srv_NguoiDung
{
    private readonly dal_NguoiDung dalNguoiDung = new();

    public srv_KetQuaDangKy DangKy(string hoTen, string tenDangNhap, string matKhau)
    {
        if (string.IsNullOrWhiteSpace(hoTen) ||
            string.IsNullOrWhiteSpace(tenDangNhap) ||
            string.IsNullOrWhiteSpace(matKhau))
        {
            return srv_KetQuaDangKy.DuLieuKhongHopLe;
        }

        hoTen = hoTen.Trim(); // Trim() Xoá bỏ khoảng trắng
        tenDangNhap = tenDangNhap.Trim();

        if (tenDangNhap.Length > 50 || hoTen.Length > 100 || matKhau.Length > 255)
        {
            return srv_KetQuaDangKy.DuLieuKhongHopLe;
        }

        if (dal_HoTro.TimKiemDon(tenDangNhap, "TenDangNhap", "NguoiDung"))
        {
            return srv_KetQuaDangKy.TaiKhoanDaTonTai;
        }

        mod_NguoiDung nguoiDung = new()
        {
            HoTen = hoTen,
            TenDangNhap = tenDangNhap,
            MatKhau = matKhau,
            SoDu = 0,
            AnhDaiDien = "default.png",
            NgayTao = DateTime.Now
        };

        return dalNguoiDung.DangKy(nguoiDung)
            ? srv_KetQuaDangKy.ThanhCong
            : srv_KetQuaDangKy.ThatBai;
    }

    public srv_KetQuaDangNhap DangNhap(string tenDangNhap, string matKhau, out mod_NguoiDung nguoiDung)
    {
        nguoiDung = null;

        if (string.IsNullOrWhiteSpace(tenDangNhap) || string.IsNullOrWhiteSpace(matKhau))
        {
            return srv_KetQuaDangNhap.DuLieuKhongHopLe;
        }

        nguoiDung = dalNguoiDung.DangNhap(tenDangNhap.Trim(), matKhau);

        return nguoiDung == null
            ? srv_KetQuaDangNhap.SaiThongTin
            : srv_KetQuaDangNhap.ThanhCong;
    }
}
