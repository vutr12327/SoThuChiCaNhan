using System;
using System.Collections.Generic;
using System.Text;

namespace SoThuChiCaNhan;

// Trả thông tin một 'người dùng'
public record dto_NguoiDung (
    int MaNguoiDung,
    string TenDangNhap,
    string HoTen,
    decimal SoDu,
    string? AnhDaiDien
);

// Đầu vào cho chức năng 'Đăng ký tài khoản'
public record dto_DangKy(string HoTen, string TenDangNhap, string MatKhau);

// Đầu vào cho chức năng 'Đăng nhập'
public record dto_DangNhap(string TenDangNhap, string MatKhau);

// Đầu vào cho chức năng 'Đổi mật khẩu'
public record dto_DoiMatKhau(int MaNguoiDung, string MatKhauCu, string MatKhauMoi);

//
public record dto_DoiHoTen(int MaNguoiDung, string HoTen);

//
public record dto_DoiAnhDaiDien(int MaNguoiDung, string AnhDaiDien);

