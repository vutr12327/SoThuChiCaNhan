using System.Linq;

namespace SoThuChiCaNhan.Business.Services.AI;

public enum srv_KetQuaTinNhanAI
{
    ThanhCong,
    DuLieuKhongHopLe,
    ThatBai
}

public class srv_TinNhanAI
{
    private readonly dal_TinNhanAI dalTinNhanAI = new dal_TinNhanAI();

    /// <summary>
    /// Kiểm tra dữ liệu của một lượt trao đổi trước khi lưu.
    /// </summary>
    private bool KiemTraDuLieuHopLe(dto_ThemTinNhanAI? dto)
    {
        return dto != null
            && dto.MaNguoiDung > 0
            && !string.IsNullOrWhiteSpace(dto.TinNhanCuaBan)
            && !string.IsNullOrWhiteSpace(dto.TinNhanCuaAI);
    }

    /// <summary>
    /// Lưu một lượt trao đổi giữa người dùng và AI.
    /// </summary>
    public async Task<srv_KetQuaTinNhanAI> ThemTinNhanAsync(dto_ThemTinNhanAI dto)
    {
        if (!KiemTraDuLieuHopLe(dto))
            return srv_KetQuaTinNhanAI.DuLieuKhongHopLe;

        var tinNhan = new mod_TinNhanAI
        {
            TinNhanCuaBan = dto.TinNhanCuaBan.Trim(),
            TinNhanCuaAI = dto.TinNhanCuaAI.Trim(),
            MaNguoiDung = dto.MaNguoiDung,
            ThoiGianNhan = DateTime.Now
        };

        return await dalTinNhanAI.ThemTinNhanAsync(tinNhan)
            ? srv_KetQuaTinNhanAI.ThanhCong
            : srv_KetQuaTinNhanAI.ThatBai;
    }

    /// <summary>
    /// Lấy toàn bộ lịch sử trao đổi của người dùng.
    /// </summary>
    public async Task<List<dto_TinNhanAI>> LayLichSuAsync(int maNguoiDung)
    {
        if (maNguoiDung <= 0)
            return new List<dto_TinNhanAI>();

        var tinNhans = await dalTinNhanAI.LayLichSuAsync(maNguoiDung);
        return tinNhans.Select(ChuyenSangDto).ToList();
    }

    /// <summary>
    /// Lấy các lượt trao đổi gần đây nhất của người dùng.
    /// </summary>
    public async Task<List<dto_TinNhanAI>> LayLichSuGanNhatAsync(
        int maNguoiDung,
        int soLuong = 5)
    {
        if (maNguoiDung <= 0 || soLuong <= 0)
            return new List<dto_TinNhanAI>();

        var tinNhans = await dalTinNhanAI.LayLichSuGanNhatAsync(maNguoiDung, soLuong);
        return tinNhans.Select(ChuyenSangDto).ToList();
    }

    /// <summary>
    /// Xóa một lượt trao đổi của người dùng.
    /// </summary>
    public async Task<srv_KetQuaTinNhanAI> XoaTinNhanAsync(dto_XoaTinNhanAI dto)
    {
        if (dto == null || dto.MaTinNhanAI <= 0 || dto.MaNguoiDung <= 0)
            return srv_KetQuaTinNhanAI.DuLieuKhongHopLe;

        return await dalTinNhanAI.XoaTinNhanAsync(dto.MaTinNhanAI, dto.MaNguoiDung)
            ? srv_KetQuaTinNhanAI.ThanhCong
            : srv_KetQuaTinNhanAI.ThatBai;
    }

    /// <summary>
    /// Xóa toàn bộ lịch sử trao đổi của người dùng.
    /// </summary>
    public async Task<srv_KetQuaTinNhanAI> XoaLichSuAsync(int maNguoiDung)
    {
        if (maNguoiDung <= 0)
            return srv_KetQuaTinNhanAI.DuLieuKhongHopLe;

        return await dalTinNhanAI.XoaLichSuAsync(maNguoiDung)
            ? srv_KetQuaTinNhanAI.ThanhCong
            : srv_KetQuaTinNhanAI.ThatBai;
    }

    private static dto_TinNhanAI ChuyenSangDto(mod_TinNhanAI tinNhan)
    {
        return new dto_TinNhanAI(
            tinNhan.MaTinNhanAI,
            tinNhan.TinNhanCuaBan,
            tinNhan.TinNhanCuaAI,
            tinNhan.ThoiGianNhan
        );
    }
}
