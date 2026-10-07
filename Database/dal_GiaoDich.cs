using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoThuChiCaNhan;

public class dal_GiaoDich
{
    public bool ThemGiaoDich(mod_GiaoDich gd, string loaiDanhMuc)
    {
        // Thêm giao dịch vào SQL
        string truyVan = "INSERT INTO GiaoDich (MaNguoiDung, MaDanhMuc, SoTien, NgayGiaoDich, GhiChu) VALUES (@1, @2, @3, @4, @5)";

        SqlParameter[] pInsert = {
                new SqlParameter("@1", gd.MaNguoiDung),
                new SqlParameter("@2", gd.MaDanhMuc),
                new SqlParameter("@3", gd.SoTien),
                new SqlParameter("@4", gd.NgayGiaoDich),
                new SqlParameter("@5", (object)gd.GhiChu ?? DBNull.Value)
        };

        int rows = dal_HoTro.ExecuteNonQuery(truyVan, pInsert);

        // Nếu thêm giao dịch thành công -> Cập nhật số dư trong bảng NguoiDung
        if (rows > 0)
        {
            string queryUpdateSoDu = loaiDanhMuc == "Thu"
                ? "UPDATE NguoiDung SET SoDu = SoDu + @1 WHERE MaNguoiDung = @2"
                : "UPDATE NguoiDung SET SoDu = SoDu - @1 WHERE MaNguoiDung = @2";

            SqlParameter[] p = {
                    new SqlParameter("@1", gd.SoTien),
                    new SqlParameter("@2", gd.MaNguoiDung)
            };

            dal_HoTro.ExecuteNonQuery(queryUpdateSoDu, p);
            return true;
        }
        return false;
    }
}
