using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace SoThuChiCaNhan;

public class dal_GiaoDich
{
    /// <summary>
    /// Thêm giao dịch và cập nhật số dư của người dùng trong cùng một giao dịch cơ sở dữ liệu.
    /// Số dư âm được phép sử dụng để biểu thị khoản nợ.
    /// </summary>
    /// <returns> Trả về
    /// <c>true</c> nếu thêm giao dịch và cập nhật số dư thành công;
    /// <c>false</c> nếu thao tác thất bại.
    /// </returns>
    public async Task<bool> ThemGiaoDichAsync(mod_GiaoDich gd)
    {
        using var db = new AppDbContext();
        await using var trans = await db.Database.BeginTransactionAsync();

        try
        {

            var ngd = await db.NguoiDung.FirstOrDefaultAsync(u => u.MaNguoiDung == gd.MaNguoiDung);
            if (ngd == null) throw new Exception("Không hợp lệ");

            var dm = await db.DanhMuc.FirstOrDefaultAsync(dm => dm.MaDanhMuc == gd.MaDanhMuc && (dm.MaNguoiDung == null || dm.MaNguoiDung == ngd.MaNguoiDung));
            if (dm == null) return false;

            switch(dm.Loai)
            {
                case "Thu": ngd.SoDu += gd.SoTien; break;
                case "Chi": ngd.SoDu -= gd.SoTien; break;
                default: throw new Exception("Không hợp lệ");
            }

            db.GiaoDich.Add(gd);

            await db.SaveChangesAsync();
            await trans.CommitAsync();
            return true;
        }
        catch
        {
            await trans.RollbackAsync();
            return false;
        }
    }

    /// <summary>
    /// Lấy danh sách giao dịch của người dùng theo thứ tự mới nhất.
    /// </summary>
    /// <returns>Trả về danh sách <see cref="List{mod_GiaoDich}"/> của người dùng.</returns>
    public async Task<List<mod_GiaoDich>> LayDanhSachGiaoDichMoiNhatAsync(int maNguoiDung)
    {
        using var db = new AppDbContext();

        return await db.GiaoDich
            .Include(gd => gd.DanhMuc)
            .Where(gd => gd.MaNguoiDung == maNguoiDung)
            .OrderByDescending(gd => gd.NgayGiaoDich)
            .ToListAsync();
    }

    /// <summary>
    /// Cập nhật giao dịch và điều chỉnh số dư của người dùng trong cùng một giao dịch cơ sở dữ liệu.
    /// Số dư âm được phép sử dụng để biểu thị khoản nợ.
    /// </summary>
    /// <returns>Trả về <c>true</c> nếu cập nhật thành công; ngược lại trả về <c>false</c>.</returns>
    public async Task<bool> SuaGiaoDichAsync(mod_GiaoDich gdMoi)
    {
        using var db = new AppDbContext();
        await using var trans = await db.Database.BeginTransactionAsync();

        try
        {
            var ngd = await db.NguoiDung.FirstOrDefaultAsync(u => u.MaNguoiDung == gdMoi.MaNguoiDung);
            if(ngd == null) throw new Exception("Không hợp lệ");

            var gdCanSua = await db.GiaoDich.FirstOrDefaultAsync(gd => gd.MaGiaoDich == gdMoi.MaGiaoDich);
            if (gdCanSua == null) throw new Exception("Không hợp lệ");
            if(gdCanSua.MaNguoiDung != ngd.MaNguoiDung) throw new Exception("Không hợp lệ");


            var dmMoi = await db.DanhMuc.FirstOrDefaultAsync(dm => dm.MaDanhMuc == gdMoi.MaDanhMuc && (dm.MaNguoiDung == null || dm.MaNguoiDung == gdCanSua.MaNguoiDung));
            var dmCu = await db.DanhMuc.FirstOrDefaultAsync(dm => dm.MaDanhMuc == gdCanSua.MaDanhMuc);

            if (dmCu == null || dmMoi == null) throw new Exception("Không hợp lệ");

            switch (dmCu.Loai)
            {
                case "Thu": ngd.SoDu -= gdCanSua.SoTien; break;
                case "Chi": ngd.SoDu += gdCanSua.SoTien; break;
                default: throw new Exception("Không hợp lệ");
            }

            switch (dmMoi.Loai)
            {
                case "Thu": ngd.SoDu += gdMoi.SoTien; break;
                case "Chi": ngd.SoDu -= gdMoi.SoTien; break;
                default: throw new Exception("Không hợp lệ");
            }

            gdCanSua.SoTien = gdMoi.SoTien;
            gdCanSua.MaDanhMuc = gdMoi.MaDanhMuc;
            gdCanSua.NgayGiaoDich = gdMoi.NgayGiaoDich;
            gdCanSua.GhiChu = gdMoi.GhiChu;

            await db.SaveChangesAsync();
            await trans.CommitAsync();
            return true;
        } 
        catch
        {
            await trans.RollbackAsync();
            return false;
        }
    }

    /// <summary>
    /// Xóa giao dịch và hoàn trả số tiền vào số dư của người dùng trong cùng một giao dịch cơ sở dữ liệu.
    /// Số dư âm được phép sử dụng để biểu thị khoản nợ.
    /// </summary>
    /// <returns>Trả về <c>true</c> nếu xóa thành công; ngược lại trả về <c>false</c>.</returns>
    public async Task<bool> XoaGiaoDichAsync(int maGiaoDich, int maNguoiDung)
    {
        using var db = new AppDbContext();
        await using var trans = await db.Database.BeginTransactionAsync();

        try
        {
            var gd = await db.GiaoDich.FirstOrDefaultAsync(g => g.MaGiaoDich == maGiaoDich && g.MaNguoiDung == maNguoiDung);
            if (gd == null) throw new Exception("Không hợp lệ");

            var ngd = await db.NguoiDung.FirstOrDefaultAsync(u => u.MaNguoiDung == maNguoiDung);
            var dm = ngd == null
                ? null
                : await db.DanhMuc.FirstOrDefaultAsync(d => d.MaDanhMuc == gd.MaDanhMuc && (d.MaNguoiDung == null || d.MaNguoiDung == ngd.MaNguoiDung));

            if (ngd == null || dm == null) throw new Exception("Không hợp lệ");

            switch (dm.Loai)
            {
                case "Thu": ngd.SoDu -= gd.SoTien; break;
                case "Chi": ngd.SoDu += gd.SoTien; break;
                default: throw new Exception("Không hợp lệ");
            }

            db.GiaoDich.Remove(gd);

            await db.SaveChangesAsync();
            await trans.CommitAsync();
            return true;
        }
        catch
        {
            await trans.RollbackAsync();
            return false;
        }
    }
}