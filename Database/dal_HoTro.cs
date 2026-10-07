using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using Microsoft.Data.SqlClient;

namespace SoThuChiCaNhan;

public class dal_HoTro
{
    private static string diaChi = ConfigurationManager.ConnectionStrings["SoThuChiDB"].ConnectionString;

    //
    // Dùng cho truy vấn SELECT
    //
    public static DataTable ExecuteQuery(string query, SqlParameter[] p = null)
    {
        // Nơi hứng dữ liệu từ db
        DataTable data = new DataTable();

        // Tạo đối tượng để kết nối db
        using var conn = new SqlConnection(diaChi);

        // Mở db
        conn.Open();

        // Thực thi truy vấn
        using var cmd = new SqlCommand(query, conn);

        if (p != null)
            // Nạp toàn bộ danh sách tham số (như @User, @Pass...)
            cmd.Parameters.AddRange(p);

        // Hứng tạm dữ liệu từ sql
        using var t_data = new SqlDataAdapter(cmd);

        // Đổ dữ liệu về data
        t_data.Fill(data);

        return data;
    }

    //
    // Dùng cho truy vấn INSERT, UPDATE, DELETE (trả về số dòng ở sql)
    //
    public static int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
    {
        int index = 0;

        using SqlConnection conn = new SqlConnection(diaChi) ;
        conn.Open();

        using SqlCommand cmd = new SqlCommand(query, conn);

        if (parameters != null)
           cmd.Parameters.AddRange(parameters);

        index = cmd.ExecuteNonQuery(); // ExecuteNonQuery trả về số dòng đã xoá, thêm, cập nhật

        return index;
    }

    //
    // Dùng cho COUNT, SUM, MAX...
    //
    public static object ExecuteScalar(string query, SqlParameter[] parameters = null)
    {
        object ans = null;
        using SqlConnection conn = new SqlConnection(diaChi);
        conn.Open();
        using SqlCommand cmd = new SqlCommand(query, conn);

        if (parameters != null)
            cmd.Parameters.AddRange(parameters);

        ans = cmd.ExecuteScalar(); // trả về dòng giá trị

        return ans;
    }

    //
    // Tìm kiếm tài khoản (true/ false)
    //

    public static bool TimKiemDon(string ThamSoCanTim, string ThamSoBang, string tenBang)
    {
        string truyVan = $"SELECT COUNT(*) FROM {tenBang} WHERE {ThamSoBang} = @1";
        SqlParameter[] p = {
            new SqlParameter("@1", ThamSoCanTim)
        };

        object ketqua = ExecuteScalar(truyVan, p);
        int ans = (ketqua == null) ? 0 : Convert.ToInt32(ketqua);

        return (ans > 0);
    }
}
