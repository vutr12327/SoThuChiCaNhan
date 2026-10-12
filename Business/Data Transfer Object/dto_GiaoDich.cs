using System;
using System.Collections.Generic;
using System.Text;

namespace SoThuChiCaNhan;

// Trả thông tin một 'giao dịch'
public record dto_GiaoDich(
    int MaGiaoDich,
    decimal SoTien,
    DateTime NgayGiaoDich,
    string? GhiChu,
    string TenDanhMuc,
    string Loai
);

// Đầu vào cho chức năng Thêm giao dịch
public record dto_ThemGiaoDich(decimal SoTien, int MaDanhMuc, string? GhiChu, int MaNguoiDung);

// Đầu vào cho chức năng Sửa giao dịch
public record dto_SuaGiaoDich(int MaGiaoDich, decimal SoTien, int MaDanhMuc, string? GhiChu, int MaNguoiDung);
