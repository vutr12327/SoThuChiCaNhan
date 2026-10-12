using System.Collections.Generic;
namespace SoThuChiCaNhan;

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
    public async Task<List<mod_DanhMuc>> LayDanhSachDanhMucAsync(int maNguoiDung)
    {
        return maNguoiDung <= 0
            ? new List<mod_DanhMuc>()
            : await dalDanhMuc.LayTatCaDanhMucAsync(maNguoiDung);
    }

    /// <summary>
    /// Kiểm tra và thêm danh mục cá nhân mới.
    /// </summary>
    /// <returns>Trả về kết quả thêm danh mục.</returns>
    public async Task<srv_KetQuaDanhMuc> ThemDanhMucAsync(mod_DanhMuc dm)
    {
        if (dm == null || dm.MaNguoiDung <= 0 || !KiemTraDuLieu(dm.TenDanhMuc, dm.Loai))
            return srv_KetQuaDanhMuc.DuLieuKhongHopLe;

        bool ketQua = await dalDanhMuc.ThemDanhMucAsync(dm);
        return ketQua ? srv_KetQuaDanhMuc.ThanhCong : srv_KetQuaDanhMuc.ThatBai;
    }

    /// <summary>
    /// Kiểm tra và cập nhật danh mục cá nhân.
    /// </summary>
    /// <returns>Trả về kết quả cập nhật danh mục.</returns>
    public async Task<srv_KetQuaDanhMuc> SuaDanhMucAsync(mod_DanhMuc dm)
    {
        if (dm == null || dm.MaDanhMuc <= 0 || dm.MaNguoiDung <= 0 || !KiemTraDuLieu(dm.TenDanhMuc, dm.Loai))
            return srv_KetQuaDanhMuc.DuLieuKhongHopLe;

        bool ketQua = await dalDanhMuc.SuaDanhMucAsync(dm);
        return ketQua ? srv_KetQuaDanhMuc.ThanhCong : srv_KetQuaDanhMuc.ThatBai;
    }

    /// <summary>
    /// Xóa danh mục cá nhân của người dùng.
    /// </summary>
    /// <returns>Trả về kết quả xóa danh mục.</returns>
    public async Task<srv_KetQuaDanhMuc> XoaDanhMucAsync(int maDanhMuc, int maNguoiDung)
    {
        if (maDanhMuc <= 0 || maNguoiDung <= 0)
            return srv_KetQuaDanhMuc.DuLieuKhongHopLe;

        bool ketQua = await dalDanhMuc.XoaDanhMucAsync(maDanhMuc, maNguoiDung);
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
}