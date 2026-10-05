using System;
using System.Windows.Forms;
using Guna.UI2;
using Guna.UI2.WinForms;
namespace SoThuChiCaNhan.frm.HoTro;

public static class TrangDon
{
    public static void HienTrang(Guna2Panel pnlMain, UserControl ucCanHien)
    {
        if (pnlMain == null || ucCanHien == null) return;

        // Tạm thời tắt vẽ giao diện
        pnlMain.SuspendLayout();

        // Đảm bảo trang hiển thị chiếm trọn panel
        ucCanHien.Dock = DockStyle.Fill;

        // Dọn dẹp trang cũ
        pnlMain.Controls.Clear();

        // Thêm trang cần hiển thị
        pnlMain.Controls.Add(ucCanHien);

        // Mở lại vẽ giao diện
        pnlMain.ResumeLayout();
    }
}
