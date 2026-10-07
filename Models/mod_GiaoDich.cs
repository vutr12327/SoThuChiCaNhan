using System;
using System.Collections.Generic;
using System.Text;

namespace SoThuChiCaNhan;

public class mod_GiaoDich
{
    public int MaGiaoDich { get; set; }
    public int MaNguoiDung { get; set; }
    public int MaDanhMuc { get; set; }
    public decimal SoTien { get; set; }
    public DateTime NgayGiaoDich { get; set; }
    public string GhiChu { get; set; }

    // Các thuộc tính phụ để hứng dữ liệu hiển thị lên UI (nếu cần JOIN bảng)
    public string TenDanhMuc { get; set; }
    public string LoaiDanhMuc { get; set; }
}
