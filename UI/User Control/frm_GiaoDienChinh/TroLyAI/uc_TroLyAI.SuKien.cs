using SoThuChiCaNhan.Business.Services.AI;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoThuChiCaNhan;

public partial class uc_TroLyAI
{
    private async void btn_GuiTinNhan_Click(object sender, EventArgs e)
    {
        string noiDung = tb_NhapTinNhan.Text.Trim();

        if (string.IsNullOrWhiteSpace(noiDung))
            return;

        ThemTinNhan(noiDung, true);
        tb_NhapTinNhan.Clear();
        btn_GuiTinNhan.Enabled = false;

        try
        {
            string tinNhanAI = await DichVuAI.GuiTinNhan(
                noiDung,
                CheDoAI.AcMong,
                Session.NguoiDung.MaNguoiDung
            );

            ThemTinNhan(tinNhanAI, false);
        }
        catch (Exception ex)
        {
            ThemTinNhan($"Lỗi: {ex.Message}", false);
        }
        finally
        {
            btn_GuiTinNhan.Enabled = true;
            tb_NhapTinNhan.Focus();
        }
    }
}
