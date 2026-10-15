using SoThuChiCaNhan.Business.Services;
using SoThuChiCaNhan.Business.Services.AI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace SoThuChiCaNhan;

public class srv_ChatBoxAI
{
    // Các dịch vụ đăng ký
    private readonly srv_Groq DichVuGroq = new();
    private readonly srv_TinNhanAI DichVuTinNhanAI = new();
    private readonly srv_GiaoDich DichVuGiaoDich = new();
    private readonly srv_DanhMuc DichVuDanhMuc = new();

    public async Task<string> GuiTinNhan(string tinNhanNguoiDung, CheDoAI mode, int maNguoiDung)
    {
        try
        {
            var dsDanhMuc = await DichVuDanhMuc.LayDanhSachDanhMucAsync(maNguoiDung);
            string nguCanhAI = await DichVuGroq.NguCanhAI(dsDanhMuc, mode);

            var lichSu = await DichVuTinNhanAI.LayLichSuGanNhatAsync(maNguoiDung);
            var messages = new List<dto_ChatMessageAI>();

            foreach (var tinNhan in lichSu)
            {
                messages.Add(new dto_ChatMessageAI(
                    Role: "user",
                    Content: tinNhan.TinNhanCuaBan)
                );

                messages.Add(new dto_ChatMessageAI(
                    Role: "assistant",
                    Content: tinNhan.TinNhanCuaAI)
                );
            }

            string? KetQuaPhanHoi = await DichVuGroq.TruyVanAIAsync(
                tinNhanNguoiDung,
                nguCanhAI,
                messages);

            if (string.IsNullOrWhiteSpace(KetQuaPhanHoi))
                throw new Exception("Lỗi: Không nhận kết quả");

            var dtoPhanHoi = JsonSerializer.Deserialize<dto_PhanHoiChatbotAI>(
                KetQuaPhanHoi,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            if (dtoPhanHoi == null)
                throw new Exception("Lỗi: Phản hồi AI không đúng định dạng");

            switch (dtoPhanHoi.HanhDong.ToUpper())
            {
                case "CREATE":
                    var dtoThem = new dto_ThemGiaoDich(
                        MaNguoiDung: maNguoiDung,
                        MaDanhMuc: dtoPhanHoi.MaDanhMuc,
                        GhiChu: dtoPhanHoi.GhiChu,
                        SoTien: dtoPhanHoi.SoTien
                    );
                    await DichVuGiaoDich.ThemGiaoDichAsync(dtoThem);
                    break;

                case "UPDATE":
                    var dtoSua = new dto_SuaGiaoDich(
                        MaGiaoDich: dtoPhanHoi.MaGiaoDichCanSuaXoa,
                        SoTien: dtoPhanHoi.SoTien,
                        MaDanhMuc: dtoPhanHoi.MaDanhMuc,
                        GhiChu: dtoPhanHoi.GhiChu,
                        MaNguoiDung: maNguoiDung
                    );
                    await DichVuGiaoDich.SuaGiaoDichAsync(dtoSua);
                    break;

                case "DELETE":
                    var dtoXoa = new dto_XoaGiaoDich(
                        MaGiaoDich: dtoPhanHoi.MaGiaoDichCanSuaXoa,
                        MaNguoiDung: maNguoiDung
                    );
                    await DichVuGiaoDich.XoaGiaoDichAsync(dtoXoa);
                    break;

                case "NONE":
                default: break;
            }

            var dtoTinNhan = new dto_ThemTinNhanAI(
                MaNguoiDung: maNguoiDung,
                TinNhanCuaBan: tinNhanNguoiDung,
                TinNhanCuaAI: dtoPhanHoi.PhanHoi
            );

            await DichVuTinNhanAI.ThemTinNhanAsync(dtoTinNhan);

            return dtoPhanHoi.PhanHoi;
        }
        catch (Exception e)
        {
            return $"{e.Message}";
        }
    }
}
