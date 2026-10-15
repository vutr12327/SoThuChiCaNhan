namespace SoThuChiCaNhan.Business.Services;

public enum srv_KetQuaDanhMuc
{
    ThanhCong,
    DuLieuKhongHopLe,
    ThatBai
}

public class srv_DanhMuc
{
    private readonly dal_DanhMuc dalDanhMuc = new dal_DanhMuc();

    /// <summary>
    /// Lấy danh sách danh mục của người dùng, bao gồm danh mục hệ thống.
    /// </summary>
    /// <returns>Trả về danh sách danh mục; nếu mã người dùng không hợp lệ thì trả về danh sách rỗng.</returns>
    public async Task<List<dto_DanhMuc>> LayDanhSachDanhMucAsync(int maNguoiDung)
    {
        return maNguoiDung <= 0
            ? new List<dto_DanhMuc>()
            : (await dalDanhMuc.LayTatCaDanhMucAsync(maNguoiDung))
                .Select(ChuyenSangDto)
                .ToList();
    }

    /// <summary>
    /// Kiểm tra và thêm danh mục cá nhân mới.
    /// </summary>
    /// <returns>Trả về kết quả thêm danh mục.</returns>
    public async Task<srv_KetQuaDanhMuc> ThemDanhMucAsync(dto_ThemDanhMuc dto)
    {
        if (dto == null || dto.MaNguoiDung <= 0 || !KiemTraDuLieu(dto.TenDanhMuc, dto.Loai))
            return srv_KetQuaDanhMuc.DuLieuKhongHopLe;

        var dm = new mod_DanhMuc
        {
            TenDanhMuc = dto.TenDanhMuc.Trim(),
            Loai = dto.Loai,
            MaNguoiDung = dto.MaNguoiDung
        };

        bool ketQua = await dalDanhMuc.ThemDanhMucAsync(dm);
        return ketQua ? srv_KetQuaDanhMuc.ThanhCong : srv_KetQuaDanhMuc.ThatBai;
    }

    /// <summary>
    /// Kiểm tra và cập nhật danh mục cá nhân.
    /// </summary>
    /// <returns>Trả về kết quả cập nhật danh mục.</returns>
    public async Task<srv_KetQuaDanhMuc> SuaDanhMucAsync(dto_SuaDanhMuc dto)
    {
        if (dto == null || dto.MaDanhMuc <= 0 || dto.MaNguoiDung <= 0 || !KiemTraDuLieu(dto.TenDanhMuc, dto.Loai))
            return srv_KetQuaDanhMuc.DuLieuKhongHopLe;

        var dm = new mod_DanhMuc
        {
            MaDanhMuc = dto.MaDanhMuc,
            TenDanhMuc = dto.TenDanhMuc.Trim(),
            Loai = dto.Loai,
            MaNguoiDung = dto.MaNguoiDung
        };

        bool ketQua = await dalDanhMuc.SuaDanhMucAsync(dm);
        return ketQua ? srv_KetQuaDanhMuc.ThanhCong : srv_KetQuaDanhMuc.ThatBai;
    }

    /// <summary>
    /// Xóa danh mục cá nhân của người dùng.
    /// </summary>
    /// <returns>Trả về kết quả xóa danh mục.</returns>
    public async Task<srv_KetQuaDanhMuc> XoaDanhMucAsync(dto_XoaDanhMuc dto)
    {
        if (dto == null || dto.MaDanhMuc <= 0 || dto.MaNguoiDung <= 0)
            return srv_KetQuaDanhMuc.DuLieuKhongHopLe;

        bool ketQua = await dalDanhMuc.XoaDanhMucAsync(dto.MaDanhMuc, dto.MaNguoiDung);
        return ketQua ? srv_KetQuaDanhMuc.ThanhCong : srv_KetQuaDanhMuc.ThatBai;
    }

    /// <summary>
    /// Kiểm tra tên và loại danh mục.
    /// </summary>
    /// <returns>Trả về <c>true</c> nếu dữ liệu hợp lệ; ngược lại trả về <c>false</c>.</returns>
    private bool KiemTraDuLieu(string tenDanhMuc, string loai)
    {
        return !string.IsNullOrWhiteSpace(tenDanhMuc) && tenDanhMuc.Length <= 100 && (loai == "Thu" || loai == "Chi");
    }

    /// <summary>
    /// Chuyển entity danh mục sang DTO để trả về cho tầng bên ngoài.
    /// </summary>
    private static dto_DanhMuc ChuyenSangDto(mod_DanhMuc dm)
    {
        return new dto_DanhMuc(
            dm.MaDanhMuc,
            dm.TenDanhMuc,
            dm.Loai,
            dm.MaNguoiDung
        );
    }
}