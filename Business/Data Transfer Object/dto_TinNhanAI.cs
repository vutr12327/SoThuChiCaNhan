namespace SoThuChiCaNhan;

// Dữ liệu trả về cho một lượt trao đổi giữa người dùng và AI.
public record dto_TinNhanAI(
    int MaTinNhanAI,
    string TinNhanCuaBan,
    string TinNhanCuaAI,
    DateTime ThoiGianNhan
);

// Dữ liệu đầu vào khi thêm một lượt trao đổi.
public record dto_ThemTinNhanAI(
    string TinNhanCuaBan,
    string TinNhanCuaAI,
    int MaNguoiDung
);

// Dữ liệu đầu vào khi xóa một lượt trao đổi.
public record dto_XoaTinNhanAI(
    int MaTinNhanAI,
    int MaNguoiDung
);
