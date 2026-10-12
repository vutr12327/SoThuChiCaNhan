using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace SoThuChiCaNhan;

public class Session
{
    public static mod_NguoiDung NguoiDung { get; set; }

    public static void DangXuat()
    {
        NguoiDung = null;
    }
}
