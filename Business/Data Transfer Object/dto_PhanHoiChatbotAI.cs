using System;
using System.Collections.Generic;
using System.Text;

namespace SoThuChiCaNhan;

public record dto_PhanHoiChatbotAI(
    string PhanHoi,          // Lời đáp của AI để hiển thị lên RichTextBox
    string HanhDong,         // "CREATE", "UPDATE", "DELETE", hoặc "NONE"
    decimal SoTien,          // Số tiền bóc tách được (nếu có)
    int MaDanhMuc,           // Mã danh mục AI đoán từ danh sách
    string GhiChu,           // Nội dung ghi chú giao dịch
    int MaGiaoDichCanSuaXoa  // Mã giao dịch (dùng cho UPDATE/DELETE)
);
