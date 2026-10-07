using System;
using System.Collections.Generic;
using System.Text;

namespace SoThuChiCaNhan;

public class Session
{
    public static mod_NguoiDung ngd { get; set; }
    public static void DangXuat()
    {
        ngd = null;
    }
}
