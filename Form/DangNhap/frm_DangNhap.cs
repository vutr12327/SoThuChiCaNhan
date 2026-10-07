using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SoThuChiCaNhan;

public partial class frm_DangNhap : Form
{
    private readonly srv_NguoiDung srvNguoiDung = new();

    public frm_DangNhap()
    {
        InitializeComponent();
        run_SuKien();
        run();
    }

    private void run()
    {
        HienThiDangNhap(null, null);
    }
}
