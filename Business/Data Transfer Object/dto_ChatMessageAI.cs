namespace SoThuChiCaNhan;

// Một message được gửi đến mô hình AI theo vai trò user, assistant hoặc system.
public record dto_ChatMessageAI(
    string Role,
    string Content
);
