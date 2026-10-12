using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SoThuChiCaNhan;

public class dal_NguoiDung
{
    /// <summary>
    /// Tìm người dùng theo mã người dùng.
    /// </summary>
    /// <returns>Trả về đối tượng <see cref="mod_NguoiDung"/> nếu tìm thấy; ngược lại trả về <c>null</c>.</returns>
    public async Task<mod_NguoiDung?> TimTheoMaNguoiDung(int maNguoiDung)
    {
        using var db = new AppDbContext();
        return await db.NguoiDung.FirstOrDefaultAsync(ngd => ngd.MaNguoiDung == maNguoiDung);
    }

    /// <summary>
    /// Tìm người dùng theo tên đăng nhập.
    /// </summary>
    /// <returns>Trả về đối tượng <see cref="mod_NguoiDung"/> nếu tìm thấy; ngược lại trả về <c>null</c>.</returns>
    public async Task<mod_NguoiDung?> TimTheoTenDangNhap(string tenDangNhap)
    {
        using var db = new AppDbContext();
        return await db.NguoiDung.FirstOrDefaultAsync(ngd => ngd.TenDangNhap == tenDangNhap);
    }

    /// <summary>
    /// Thêm người dùng vào cơ sở dữ liệu.
    /// </summary>
    /// <returns>Trả về <c>true</c> nếu thêm thành công; ngược lại trả về <c>false</c>.</returns>
    public async Task<bool> ThemNguoiDung(mod_NguoiDung ngDung)
    {
        using var db = new AppDbContext();
        db.NguoiDung.Add(ngDung);
        return await db.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// Thay đổi mật khẩu của người dùng.
    /// </summary>
    /// <returns>Trả về <c>true</c> nếu cập nhật thành công; ngược lại trả về <c>false</c>.</returns>
    public async Task<bool> DoiMatKhau(int maNguoiDung, string matKhau)
    {
        using var db = new AppDbContext();
        var ngd = await db.NguoiDung.FirstOrDefaultAsync(u => u.MaNguoiDung == maNguoiDung);
        if (ngd == null) return false;

        ngd.MatKhau = matKhau;

        return await db.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// Thay đổi họ tên của người dùng.
    /// </summary>
    /// <returns>Trả về <c>true</c> nếu cập nhật thành công; ngược lại trả về <c>false</c>.</returns>
    public async Task<bool> DoiHoTen(int maNguoiDung, string hoTen)
    {
        using var db = new AppDbContext();
        var ngd = await db.NguoiDung.FirstOrDefaultAsync(u => u.MaNguoiDung == maNguoiDung);
        if (ngd == null) return false;

        ngd.HoTen = hoTen;

        return await db.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// Thay đổi ảnh đại diện của người dùng.
    /// </summary>
    /// <returns>Trả về <c>true</c> nếu cập nhật thành công; ngược lại trả về <c>false</c>.</returns>
    public async Task<bool> DoiAnhDaiDien(int maNguoiDung, string anhDaiDien)
    {
        using var db = new AppDbContext();
        var ngd = await db.NguoiDung.FirstOrDefaultAsync(u => u.MaNguoiDung == maNguoiDung);
        if (ngd == null) return false;

        ngd.AnhDaiDien = anhDaiDien;

        return await db.SaveChangesAsync() > 0;
    }
}
