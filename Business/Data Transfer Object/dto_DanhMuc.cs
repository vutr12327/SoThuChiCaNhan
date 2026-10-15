namespace SoThuChiCaNhan;

// Trả thông tin một 'danh mục'
public record dto_DanhMuc(
    int MaDanhMuc, 
    string TenDanhMuc, 
    string Loai, 
    int? MaNguoiDung
);

// Đầu vào cho chức năng Thêm danh mục
public record dto_ThemDanhMuc(string TenDanhMuc, string Loai, int MaNguoiDung);

// Đầu vào cho chức năng Sửa danh mục
public record dto_SuaDanhMuc(int MaDanhMuc, string TenDanhMuc, string Loai, int MaNguoiDung);

// Dữ liệu đầu vào cho chức năng Xoá danh mục
public record dto_XoaDanhMuc(int MaDanhMuc, int MaNguoiDung);
