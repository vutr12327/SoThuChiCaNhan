using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace SoThuChiCaNhan;

public class dal_DanhMuc
{
    /// <summary>
    /// Xóa danh mục cá nhân của người dùng khỏi cơ sở dữ liệu.
    /// </summary>
    /// <returns>Trả về <c>true</c> nếu xóa thành công; ngược lại trả về <c>false</c>.</returns>
    public async Task<bool> XoaDanhMucAsync(int maDanhMuc, int maNguoiDung)
    {
        using var db = new AppDbContext();
        var dm = await db.DanhMuc.FirstOrDefaultAsync(d => d.MaDanhMuc == maDanhMuc && d.MaNguoiDung == maNguoiDung);
        if (dm == null) return false;

        db.DanhMuc.Remove(dm);
        return await db.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// Cập nhật tên và loại của danh mục cá nhân.
    /// </summary>
    /// <returns>Trả về <c>true</c> nếu cập nhật thành công; ngược lại trả về <c>false</c>.</returns>
    public async Task<bool> SuaDanhMucAsync(mod_DanhMuc dmMoi)
    {
        using var db = new AppDbContext();
        var dm = await db.DanhMuc.FirstOrDefaultAsync(d => d.MaDanhMuc == dmMoi.MaDanhMuc && d.MaNguoiDung == dmMoi.MaNguoiDung);
        if (dm == null) return false;

        dm.TenDanhMuc = dmMoi.TenDanhMuc;
        dm.Loai = dmMoi.Loai;

        return await db.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// Thêm một danh mục mới vào cơ sở dữ liệu.
    /// </summary>
    /// <returns>Trả về <c>true</c> nếu thêm thành công; ngược lại trả về <c>false</c>.</returns>
    public async Task<bool> ThemDanhMucAsync(mod_DanhMuc dmMoi)
    {
        using var db = new AppDbContext();
        db.DanhMuc.Add(dmMoi);
        return await db.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// Lấy danh sách danh mục của người dùng, bao gồm cả danh mục hệ thống.
    /// </summary>
    /// <returns>Trả về một <see cref="List{mod_DanhMuc}"/> chứa danh sách các danh mục tìm thấy.</returns>
    public async Task<List<mod_DanhMuc>> LayTatCaDanhMucAsync(int maNguoiDung)
    {
        using var db = new AppDbContext();
        return await db.DanhMuc.Where(d => d.MaNguoiDung == null || d.MaNguoiDung == maNguoiDung)
                    .OrderBy(d => d.TenDanhMuc)
                    .ToListAsync();
    }

    /// <summary>
    /// Lấy thông tin danh mục theo mã danh mục và quyền sử dụng của người dùng.
    /// </summary>
    /// <returns>Trả về đối tượng <see cref="mod_DanhMuc"/> nếu tìm thấy; ngược lại trả về <c>null</c>.</returns>
    public async Task<mod_DanhMuc?> LayDanhMucTheoMaAsync(int maDanhMuc, int maNguoiDung)
    {
        using var db = new AppDbContext();
        return await db.DanhMuc.FirstOrDefaultAsync(d => d.MaDanhMuc == maDanhMuc && (d.MaNguoiDung == null || d.MaNguoiDung == maNguoiDung));
    }
}