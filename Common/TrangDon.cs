using System;
using System.Windows.Forms;
using Guna.UI2;
using Guna.UI2.WinForms;

namespace SoThuChiCaNhan;

public static class TrangDon
{
    /// <summary>
    /// Chuyển đổi và hiển thị một UserControl lên Panel chính
    /// </summary>
    /// <param name="pnlMain">Panel chứa giao diện chính</param>
    /// <param name="ucCanHien">UserControl giao diện trang cần hiển thị</param>
    public static void HienTrang(Guna2Panel pnlMain, UserControl ucCanHien)
    {
        // Kiểm tra an toàn
        if (pnlMain == null || ucCanHien == null) return;

        // Tạm dừng vẽ giao diện
        pnlMain.SuspendLayout();

        // Dọn dẹp UserControl cũ
        pnlMain.Controls.Clear();

        // Thêm UserControl mới vào Panel
        pnlMain.Controls.Add(ucCanHien);

        // Cho UserControl bám hết Panel
        ucCanHien.Dock = DockStyle.Fill;

        // Đưa thanh cuộn về vị trí đầu trang
        pnlMain.AutoScrollPosition = new Point(0, 0);

        // Bật vẽ giao diện
        pnlMain.ResumeLayout(true);
    }
}
