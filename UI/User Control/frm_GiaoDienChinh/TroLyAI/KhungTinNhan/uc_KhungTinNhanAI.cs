using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SoThuChiCaNhan.UI.User_Control.frm_GiaoDienChinh.TroLyAI.KhungTinNhan
{
    public partial class uc_KhungTinNhanAI : UserControl
    {
        public uc_KhungTinNhanAI(string VanBan)
        {
            InitializeComponent();
            guna2HtmlLabel1.Text = System.Net.WebUtility.HtmlEncode(VanBan);
            // Bo tròn khung tin nhắn
            guna2Panel1.BorderRadius = 10;

            // Đặt màu nền
            guna2Panel1.FillColor = Color.FromArgb(232, 240, 254);

            // Để nền Label trong suốt
            guna2HtmlLabel1.BackColor = Color.Transparent;

        }
    }
}
