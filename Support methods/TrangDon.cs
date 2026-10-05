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

        // Định dạng kích thước và thuộc tính co giãn theo Panel chính (tạm bỏ)
        //ucCanHien.Width = pnlMain.ClientSize.Width;
        //ucCanHien.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        //ucCanHien.Location = new Point(0, 0);

        // Thêm UserControl mới vào Panel
        pnlMain.Controls.Add(ucCanHien);

        // Đảm bảo UserControl bám hết Panel
        ucCanHien.Dock = DockStyle.Fill;

        // Bật cuộn
        ucCanHien.AutoScroll = true;

        // Đưa thanh cuộn về vị trí đầu trang
        pnlMain.AutoScrollPosition = new Point(0, 0);

        // Bật vẽ giao diện
        pnlMain.ResumeLayout(true);
    }
}
