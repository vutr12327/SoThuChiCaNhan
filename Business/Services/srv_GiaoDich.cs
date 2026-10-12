using System.Collections.Generic;

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
    public async Task<srv_KetQuaGiaoDich> ThemGiaoDichAsync(mod_GiaoDich gd)
    {
        if (!KiemTraDuLieuHopLe(gd)) return srv_KetQuaGiaoDich.DuLieuKhongHopLe;

        return await dalGiaoDich.ThemGiaoDichAsync(gd)
            ? srv_KetQuaGiaoDich.ThanhCong
            : srv_KetQuaGiaoDich.ThatBai;
    }

    /// <summary>
    /// Lấy danh sách giao dịch mới nhất của người dùng.
    /// </summary>
    /// <returns>Trả về danh sách giao dịch; nếu mã người dùng không hợp lệ thì trả về danh sách rỗng.</returns>
    public async Task<List<mod_GiaoDich>> LayDanhSachGiaoDichMoiNhatAsync(int maNguoiDung)
    {
        if (maNguoiDung <= 0) return new List<mod_GiaoDich>();

        return await dalGiaoDich.LayDanhSachGiaoDichMoiNhatAsync(maNguoiDung);
    }

    /// <summary>
    /// Kiểm tra và cập nhật giao dịch hiện có.
    /// </summary>
    /// <returns>Trả về kết quả cập nhật giao dịch.</returns>
    public async Task<srv_KetQuaGiaoDich> SuaGiaoDichAsync(mod_GiaoDich gd)
    {
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
    public async Task<srv_KetQuaGiaoDich> XoaGiaoDichAsync(int maGiaoDich, int maNguoiDung)
    {
        if (maGiaoDich <= 0 || maNguoiDung <= 0)
            return srv_KetQuaGiaoDich.DuLieuKhongHopLe;

        return await dalGiaoDich.XoaGiaoDichAsync(maGiaoDich, maNguoiDung)
            ? srv_KetQuaGiaoDich.ThanhCong
            : srv_KetQuaGiaoDich.ThatBai;
    }
}
