namespace SoThuChiCaNhan.Business.Services;

public enum srv_KetQuaGiaoDich
{
    ThanhCong,
    DuLieuKhongHopLe,
    ThatBai
}

public class srv_GiaoDich
{
    private readonly dal_GiaoDich dalGiaoDich = new dal_GiaoDich();

    /// <summary>
    /// Kiểm tra dữ liệu giao dịch trước khi thêm hoặc cập nhật.
    /// </summary>
    /// <returns>Trả về <c>true</c> nếu dữ liệu hợp lệ; ngược lại trả về <c>false</c>.</returns>
    private bool KiemTraDuLieuHopLe(mod_GiaoDich? gd, bool yeuCauMaGiaoDich = false)
    {
        if (gd == null 
            || (yeuCauMaGiaoDich && gd.MaGiaoDich <= 0)
            || gd.MaNguoiDung <= 0 
            || gd.MaDanhMuc <= 0 
            || gd.SoTien <= 0 
            || (gd.GhiChu != null && gd.GhiChu.Length > 500))
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// Kiểm tra và thêm giao dịch mới.
    /// </summary>
    /// <returns>Trả về kết quả thêm giao dịch.</returns>
    public async Task<srv_KetQuaGiaoDich> ThemGiaoDichAsync(dto_ThemGiaoDich d)
    {
        if (d == null)
            return srv_KetQuaGiaoDich.DuLieuKhongHopLe;

        var gd = new mod_GiaoDich
        {
            MaNguoiDung = d.MaNguoiDung,
            MaDanhMuc = d.MaDanhMuc,
            GhiChu = d.GhiChu,
            SoTien = d.SoTien
        };

        if (!KiemTraDuLieuHopLe(gd)) return srv_KetQuaGiaoDich.DuLieuKhongHopLe;

        return await dalGiaoDich.ThemGiaoDichAsync(gd)
            ? srv_KetQuaGiaoDich.ThanhCong
            : srv_KetQuaGiaoDich.ThatBai;
    }

    /// <summary>
    /// Lấy danh sách giao dịch mới nhất của người dùng.
    /// </summary>
    /// <returns>Trả về danh sách giao dịch; nếu mã người dùng không hợp lệ thì trả về danh sách rỗng.</returns>
    public async Task<List<dto_GiaoDich>> LayDanhSachGiaoDichMoiNhatAsync(int maNguoiDung)
    {
        if (maNguoiDung <= 0) return new List<dto_GiaoDich>();

        var giaoDichs = await dalGiaoDich.LayDanhSachGiaoDichMoiNhatAsync(maNguoiDung);
        return giaoDichs.Select(ChuyenSangDto).ToList();
    }

    /// <summary>
    /// Kiểm tra và cập nhật giao dịch hiện có.
    /// </summary>
    /// <returns>Trả về kết quả cập nhật giao dịch.</returns>
    public async Task<srv_KetQuaGiaoDich> SuaGiaoDichAsync(dto_SuaGiaoDich dto)
    {
        if (dto == null)
            return srv_KetQuaGiaoDich.DuLieuKhongHopLe;

        var gd = new mod_GiaoDich
        {
            MaGiaoDich = dto.MaGiaoDich,
            SoTien = dto.SoTien,
            MaDanhMuc = dto.MaDanhMuc,
            GhiChu = dto.GhiChu,
            MaNguoiDung = dto.MaNguoiDung
        };

        if (!KiemTraDuLieuHopLe(gd, true))
            return srv_KetQuaGiaoDich.DuLieuKhongHopLe;

        return await dalGiaoDich.SuaGiaoDichAsync(gd)
            ? srv_KetQuaGiaoDich.ThanhCong
            : srv_KetQuaGiaoDich.ThatBai;
    }

    /// <summary>
    /// Xóa giao dịch của người dùng.
    /// </summary>
    /// <returns>Trả về kết quả xóa giao dịch.</returns>
    public async Task<srv_KetQuaGiaoDich> XoaGiaoDichAsync(dto_XoaGiaoDich d)
    {
        if (d == null || d.MaGiaoDich <= 0 || d.MaNguoiDung <= 0)
            return srv_KetQuaGiaoDich.DuLieuKhongHopLe;

        return await dalGiaoDich.XoaGiaoDichAsync(d.MaGiaoDich, d.MaNguoiDung)
            ? srv_KetQuaGiaoDich.ThanhCong
            : srv_KetQuaGiaoDich.ThatBai;
    }

    /// <summary>
    /// Chuyển entity giao dịch sang DTO để trả về cho tầng bên ngoài.
    /// </summary>
    private static dto_GiaoDich ChuyenSangDto(mod_GiaoDich gd)
    {
        return new dto_GiaoDich(
            gd.MaGiaoDich,
            gd.SoTien,
            gd.NgayGiaoDich,
            gd.GhiChu,
            gd.DanhMuc?.TenDanhMuc ?? string.Empty,
            gd.DanhMuc?.Loai ?? string.Empty
        );
    }
}
