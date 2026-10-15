using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoThuChiCaNhan;

[Table("TinNhanAI")]
public class mod_TinNhanAI
{
    [Key]
    public int MaTinNhanAI { get; set; }

    [Column(TypeName = "nvarchar(max)")]
    public string TinNhanCuaBan { get; set; } = string.Empty;

    [Column(TypeName = "nvarchar(max)")]
    public string TinNhanCuaAI { get; set; } = string.Empty;

    public DateTime ThoiGianNhan { get; set; } = DateTime.Now;

    // Khai báo khóa ngoại
    public int MaNguoiDung { get; set; }

    [ForeignKey(nameof(MaNguoiDung))]
    public mod_NguoiDung? NguoiDung { get; set; }
}
