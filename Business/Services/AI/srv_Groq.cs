using SoThuChiCaNhan.Business.Services;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SoThuChiCaNhan.Business.Services.AI;

public enum CheDoAI
{
    ThienThan,
    AcMong
}

public class srv_Groq
{
    private static readonly string apiKey = ""; // Thay API Key của Groq vào đây
    private static readonly string apiUrl = "https://api.groq.com/openai/v1/chat/completions";
    private static readonly HttpClient client = new(); // Khai báo 1 httpClient đóng vai trò giao tiếp giữa dịch vụ trung gian (srv_Groq) và dịch vụ bên thứ 3 (Groq AI)

    public async Task<string> NguCanhAI(List<dto_DanhMuc> dsDanhMuc, CheDoAI mode = CheDoAI.ThienThan)
    {
        // Chuyển danh sách danh mục thành chuỗi
        string chuoiDsDanhMuc = "";
        foreach (var dm in dsDanhMuc)
            chuoiDsDanhMuc += $"ID:{dm.MaDanhMuc}-{dm.TenDanhMuc}-Loại:{dm.Loai},";

        // Phong cách AI
        string phongCach = mode == CheDoAI.ThienThan
            ? @"
                Bạn là trợ lý tài chính THIÊN THẦN: thân thiện, ấm áp,
                biết động viên và khéo léo nhắc nhở người dùng quản lý tiền.
                Phản hồi người dùng ít nhất 2 - 3 dòng.
              "
            : @"
                Bạn là 'Thần Giữ Của' - độc mồm, chua ngoa, coi tiền như mạng sống. Luôn dùng tiếng lóng, so sánh châm biếm sâu cay để mắng người tiêu tiền.
                Nếu họ tiêu 0đ hoặc được bao, hãy mỉa mai tính ăn chực/đi ké của họ.
                CẤM lặp lại văn mẫu cũ.
                phải dài ít nhất 3 - 5 câu để châm biến, mỉa mai người dùng bất kể
              ";

                // Trả về ngữ cảnh làm việc cho AI
                return $@"
        {phongCach}

        Bạn là trợ lý AI trong ứng dụng quản lý thu chi cá nhân.

        DANH SÁCH DANH MỤC HIỆN CÓ:
        {chuoiDsDanhMuc}

        QUY TẮC NGHIỆP VỤ:

        1. HanhDong là 1 trong 4 bên dưới:
        ""CREATE"": tạo giao dịch mới.
        ""UPDATE"": sửa giao dịch đã tồn tại.
        ""DELETE"": xóa giao dịch theo yêu cầu rõ ràng.
        ""NONE"" trò chuyện hoặc chưa đủ thông tin.

        2. LoaiGiaoDich:
        - ""THU"": tiền đi vào ví, bao gồm nhận tiền và được hoàn tiền.
        - ""CHI"": tiền đi ra khỏi ví, bao gồm mua sắm, trả nợ, thanh toán và hoàn tiền cho người khác.
        - ""NONE"": không có giao dịch tài chính.

        3. Phải hiểu toàn bộ câu, không bắt từ khóa máy móc:
        - ""Tôi được hoàn tiền 100k"" => CREATE, THU, 100000.
        - ""Tôi hoàn trả bạn 100k"" => CREATE, CHI, 100000.
        - ""Tôi trả lại bạn 50k"" => CREATE, CHI, 50000.
        - ""Xóa giao dịch ăn sáng đã ghi nhầm"" => DELETE nếu xác định được giao dịch.
        - ""Tôi vừa trả tiền, xóa đi"" => hỏi lại nếu chưa rõ ý định. Không được tự ý xóa chỉ vì câu có chữ ""xóa đi"".

        4. Quy đổi số tiền theo tiếng lóng:
        - k, ngàn, nghìn = 1.000.
        - cành, xị = X1.000 khi phù hợp ngữ cảnh.
        - tr, triệu, củ, khoai, chai = X1.000.000.
        - tỷ, tỉ, tỏi = X1.000.000.000.
        - 10k5 = 10.500; 1,5tr = 1.500.000.
        - ""Cơm 35"" trong ngữ cảnh mua bán có thể hiểu là 35.000.
        Không tự bịa số tiền. Nếu thiếu số tiền cần thiết, hãy hỏi lại.

        5. Chỉ chọn MaDanhMuc có trong danh sách, không bịa ID.
        Nếu không xác định được, đặt MaDanhMuc = 0.
        Chỉ UPDATE hoặc DELETE khi xác định được giao dịch mục tiêu.
        Không bịa MaGiaoDichCanSuaXoa; nếu chưa rõ, đặt bằng 0.

        6. Nếu không có giao dịch:
        HanhDong = NONE, LoaiGiaoDich = NONE, SoTien = 0,
        MaDanhMuc = 0, MaGiaoDichCanSuaXoa = 0.

        7. PhanHoi:
        - Thiên Thần: nhẹ nhàng, tích cực, động viên.
        - Ác Quỷ: cà khịa sắc bén, độc mồm, hài hước và đúng tình huống.
        - Không tuyên bố giao dịch thành công khi hệ thống chưa xử lý.
        - Nếu thiếu thông tin, hãy hỏi lại theo đúng phong cách.
        - Dù chỉ trò chuyện, vẫn phải trả JSON đúng cấu trúc.

        QUAN TRONG TRẢ VỀ JSON BẮT BUỘC:
        {{
            ""PhanHoi"": ""Câu trả lời cho người dùng"",
            ""HanhDong"": ""NONE"",
            ""LoaiGiaoDich"": ""NONE"",
            ""SoTien"": 0,
            ""MaDanhMuc"": 0,
            ""GhiChu"": """",
            ""MaGiaoDichCanSuaXoa"": 0
        }}

        HanhDong chỉ gồm CREATE, UPDATE, DELETE, NONE.
        LoaiGiaoDich chỉ gồm THU, CHI, NONE.
        Các trường phải đúng kiểu dữ liệu. JSON trên chỉ minh họa
        cấu trúc, không phải kết quả mặc định.
        ";
    }
    public async Task<string?> TruyVanAIAsync(
        string tinNhanNguoiDung,
        string NguCanhAI = "",
        IReadOnlyList<dto_ChatMessageAI>? lichSu = null)
    {
        // Tạo 1 đối tượng xác thực 'yêu cầu' khi ta gửi 1 yêu cầu đến dịch vụ bên thứ 3 (Groq AI)
        // "Bearer" là loại thông tin xác thực bên thứ 3 chấp nhận
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        var messages = new List<object>
        {
            new
            {
                role = "system",
                content = string.IsNullOrEmpty(NguCanhAI)
                    ? "Bạn là trợ lý ảo thân thiện."
                    : NguCanhAI
            }
        };

        if (lichSu != null)
        {
            foreach (var message in lichSu)
            {
                if (string.IsNullOrWhiteSpace(message.Role)
                    || string.IsNullOrWhiteSpace(message.Content))
                {
                    continue;
                }

                messages.Add(new
                {
                    role = message.Role,
                    content = message.Content
                });
            }
        }

        messages.Add(new
        {
            role = "user",
            content = tinNhanNguoiDung
        });

        // Đóng gói dữ liệu thành JSON để gửi câu hỏi đến API AI của Groq
        var NoiDungYeuCau = new
        {
            model = "openai/gpt-oss-120b", // Tên mô hình AI
            messages,
            temperature = 0.4, // Tham số điều chỉnh mức độ ngẫu nhiên trong câu trả lời. Càng thấp AI phân tích càng chính xác
            response_format = new { type = "json_object" } // Ép về cấu trúc phản hồi dạng json
        };

        // Chuyển nội dung thành json
        string NoiDungJson = JsonSerializer.Serialize(NoiDungYeuCau);

        // Đóng gói thành dữ liệu có thể gửi
        var NoiDungDeGui = new StringContent(NoiDungJson, Encoding.UTF8, "application/json");

        try
        {
            // Gửi yếu cầu tới API bằng phương thức Post
            HttpResponseMessage PhanHoi = await client.PostAsync(apiUrl, NoiDungDeGui);

            // Nội dung phản hồi về
            string KetQuaPhanHoi = await PhanHoi.Content.ReadAsStringAsync();

            // Đúng khi trạng thái phản hồi trả 
            if (PhanHoi.IsSuccessStatusCode)
            {
                // Ép kiểu kết quả phản hồi về dạng json và giải phóng tài nguyên đúng cách
                using JsonDocument ketQuaJson = JsonDocument.Parse(KetQuaPhanHoi);
                return ketQuaJson.RootElement
                    .GetProperty("choices")[0]
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString();
            }
            else
            {
                throw new HttpRequestException(
                    $"Lỗi phản hồi Groq: {PhanHoi.StatusCode}. Nội dung: {KetQuaPhanHoi}");
            }
        }
        catch
        {
            throw;
        }
    }
}
