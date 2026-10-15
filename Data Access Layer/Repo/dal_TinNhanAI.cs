using Microsoft.EntityFrameworkCore;

namespace SoThuChiCaNhan;

public class dal_TinNhanAI
{
    /// <summary>
    /// Lưu một lượt trao đổi giữa người dùng và AI.
    /// </summary>
    /// <returns>Trả về <c>true</c> nếu lưu thành công; ngược lại trả về <c>false</c>.</returns>
    public async Task<bool> ThemTinNhanAsync(mod_TinNhanAI tinNhan)
    {
        if (tinNhan == null || tinNhan.MaNguoiDung <= 0)
            return false;

        using var db = new AppDbContext();

        var nguoiDungTonTai = await db.NguoiDung
            .AnyAsync(nguoiDung => nguoiDung.MaNguoiDung == tinNhan.MaNguoiDung);

        if (!nguoiDungTonTai)
            return false;

        tinNhan.NguoiDung = null;
        db.TinNhanAI.Add(tinNhan);

        return await db.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// Lấy lịch sử trao đổi của một người dùng, từ cũ đến mới.
    /// </summary>
    public async Task<List<mod_TinNhanAI>> LayLichSuAsync(int maNguoiDung)
    {
        using var db = new AppDbContext();

        return await db.TinNhanAI
            .AsNoTracking()
            .Where(tinNhan => tinNhan.MaNguoiDung == maNguoiDung)
            .OrderBy(tinNhan => tinNhan.ThoiGianNhan)
            .ToListAsync();
    }

    /// <summary>
    /// Lấy các lượt trao đổi gần đây nhất của một người dùng.
    /// </summary>
    public async Task<List<mod_TinNhanAI>> LayLichSuGanNhatAsync(
        int maNguoiDung,
        int soLuong = 20)
    {
        if (soLuong <= 0)
            return new List<mod_TinNhanAI>();

        using var db = new AppDbContext();

        var tinNhans = await db.TinNhanAI
            .AsNoTracking()
            .Where(tinNhan => tinNhan.MaNguoiDung == maNguoiDung)
            .OrderByDescending(tinNhan => tinNhan.ThoiGianNhan)
            .Take(soLuong)
            .ToListAsync();

        return tinNhans
            .OrderBy(tinNhan => tinNhan.ThoiGianNhan)
            .ToList();
    }

    /// <summary>
    /// Xóa một lượt trao đổi, chỉ khi lượt trao đổi thuộc về người dùng đó.
    /// </summary>
    public async Task<bool> XoaTinNhanAsync(int maTinNhanAI, int maNguoiDung)
    {
        using var db = new AppDbContext();

        var tinNhan = await db.TinNhanAI
            .FirstOrDefaultAsync(x =>
                x.MaTinNhanAI == maTinNhanAI &&
                x.MaNguoiDung == maNguoiDung);

        if (tinNhan == null)
            return false;

        db.TinNhanAI.Remove(tinNhan);
        return await db.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// Xóa toàn bộ lịch sử trao đổi của một người dùng.
    /// </summary>
    public async Task<bool> XoaLichSuAsync(int maNguoiDung)
    {
        using var db = new AppDbContext();

        var tinNhans = await db.TinNhanAI
            .Where(tinNhan => tinNhan.MaNguoiDung == maNguoiDung)
            .ToListAsync();

        if (tinNhans.Count == 0)
            return false;

        db.TinNhanAI.RemoveRange(tinNhans);
        return await db.SaveChangesAsync() > 0;
    }
}
