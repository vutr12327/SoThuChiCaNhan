using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace SoThuChiCaNhan;

public class dal_NguoiDung
{
    public mod_NguoiDung DangNhap(string TaiKhoan, string MatKhau)
    {
        string truyVan = "SELECT * FROM NguoiDung WHERE TenDangNhap = @User AND MatKhau = @Pass";
        SqlParameter[] p = {
                new SqlParameter("@User", TaiKhoan),
                new SqlParameter("@Pass", MatKhau)
        };

        DataTable ans = dal_HoTro.ExecuteQuery(truyVan, p);

        if(ans.Rows.Count > 0)
        {
            DataRow r = ans.Rows[0];
            return new mod_NguoiDung
            {
                MaNguoiDung = Convert.ToInt32(r["MaNguoiDung"]),
                TenDangNhap = r["TenDangNhap"].ToString(),
                MatKhau = r["MatKhau"].ToString(),
                HoTen = r["HoTen"].ToString(),
                SoDu = Convert.ToDecimal(r["SoDu"]),
                AnhDaiDien = r["AnhDaiDien"] != DBNull.Value ? r["AnhDaiDien"].ToString() : "default_avatar.png",
                NgayTao = Convert.ToDateTime(r["NgayTao"])
            };
        }

        return null;
    }

    public bool DangKy(mod_NguoiDung ngDung)
    {
        string truyVan = "INSERT INTO NguoiDung (TenDangNhap, MatKhau, HoTen, SoDu) VALUES (@1, @2, @3, @4)";
        SqlParameter[] p = {
                new SqlParameter("@1", ngDung.TenDangNhap),
                new SqlParameter("@2", ngDung.MatKhau),
                new SqlParameter("@3", ngDung.HoTen),
                new SqlParameter("@4", ngDung.SoDu)
        };

        return dal_HoTro.ExecuteNonQuery(truyVan, p) > 0;
    }
}
