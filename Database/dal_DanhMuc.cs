using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using Microsoft.Data.SqlClient;

namespace SoThuChiCaNhan;

public class dal_DanhMuc
{
    private readonly string diaChi = ConfigurationManager.ConnectionStrings["SoThuChiDB"].ConnectionString;

    public bool XoaDanhMuc(int maDanhMuc)
    {
        string truyVan = "DELETE FROM DanhMuc WHERE MaDanhMuc = @1";

        using SqlConnection conn = new SqlConnection(diaChi);
        conn.Open();

        SqlParameter[] q = {
            new SqlParameter("@1", maDanhMuc)
        };

        using SqlCommand cmd = new SqlCommand(truyVan, conn);
        cmd.Parameters.AddRange(q);

        return cmd.ExecuteNonQuery() > 0;
    }

    public bool SuaDanhMuc(int maDanhMuc, string tenDanhMuc, string loai)
    {
        string truyVan = "UPDATE DanhMuc SET TenDanhMuc = @2, Loai = @3 WHERE MaDanhMuc = @1";

        using SqlConnection conn = new SqlConnection(diaChi);
        conn.Open();

        SqlParameter[] q = {
            new SqlParameter("@1", maDanhMuc),
            new SqlParameter("@2", tenDanhMuc),
            new SqlParameter("@3", loai)
        };

        using SqlCommand cmd = new SqlCommand(truyVan, conn);
        cmd.Parameters.AddRange(q);

        return cmd.ExecuteNonQuery() > 0;
    }

    public bool ThemDanhMuc(string tenDanhMuc, string loai)
    {
        string truyVan = "INSERT INTO DanhMuc (TenDanhMuc, Loai) VALUES (@1, @2)";

        using SqlConnection conn = new SqlConnection(diaChi);
        conn.Open();

        SqlParameter[] q = {
            new SqlParameter("@1", tenDanhMuc),
            new SqlParameter("@2", loai)
        };

        using SqlCommand cmd = new SqlCommand(truyVan, conn);
        cmd.Parameters.AddRange(q);
        return cmd.ExecuteNonQuery() > 0;
    }


    public List<mod_DanhMuc> LayTatCaDanhMuc()
    {

        string truyVan = "SELECT MaDanhMuc, TenDanhMuc, Loai FROM DanhMuc";
        List<mod_DanhMuc> ds = new List<mod_DanhMuc>();

        using SqlConnection conn = new SqlConnection(diaChi);
        conn.Open();

        using SqlCommand cmd = new SqlCommand(truyVan, conn);
        using SqlDataReader reader = cmd.ExecuteReader();

        while(reader.Read())
        {
            ds.Add(
                new mod_DanhMuc
                {
                    MaDanhMuc = reader.GetInt32(reader.GetOrdinal("MaDanhMuc")),
                    TenDanhMuc = reader.GetString(reader.GetOrdinal("TenDanhMuc")),
                    Loai = reader.GetString(reader.GetOrdinal("Loai"))
                }
            );
        }

        return ds;
    }
}
