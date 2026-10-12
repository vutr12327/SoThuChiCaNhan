using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SoThuChiCaNhan;

[Table("GiaoDich")]
public class mod_GiaoDich
{
    [Key] 
    public int MaGiaoDich { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal SoTien { get; set; }

    public DateTime NgayGiaoDich { get; set; } = DateTime.Now;

    [StringLength(500)]
    public string? GhiChu { get; set; }

    // Khóa ngoại 1
    public int MaNguoiDung { get; set; }

    [ForeignKey(nameof(MaNguoiDung))]
    public mod_NguoiDung? NguoiDung { get; set; }

    // Khóa ngoại 2
    public int MaDanhMuc { get; set; }

    [ForeignKey(nameof(MaDanhMuc))]
    public mod_DanhMuc? DanhMuc { get; set; }
}
