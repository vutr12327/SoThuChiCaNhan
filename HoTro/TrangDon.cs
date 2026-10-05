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

        // Dọn dẹp trang cũ
        pnlMain.Controls.Clear();

        // Đặt vị trí đầu trang và thêm vào Panel
        ucCanHien.Width = pnlMain.ClientSize.Width;
        ucCanHien.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        ucCanHien.Location = new Point(0, 0);

        // Thêm trang cần hiển thị
        pnlMain.Controls.Add(ucCanHien);

        // Đặt l
        pnlMain.AutoScrollPosition = new Point(0, 0);

        // Mở lại vẽ giao diện
        pnlMain.ResumeLayout(true);
    }
}
