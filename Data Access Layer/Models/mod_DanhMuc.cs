using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SoThuChiCaNhan;

[Table("DanhMuc")]
public class mod_DanhMuc
{
    [Key] // Khai báo là khoá chính
    public int MaDanhMuc { get; set; } // Khoá chính và tự tăng chỉ số "IDENTITY(1,1)"

    [Required]
    [StringLength(100)]
    public string TenDanhMuc { get; set; } = string.Empty;

    [Required]
    [StringLength(10)]
    public string Loai { get; set; } = "Chi"; // "Thu" hoặc "Chi"

    // Khai báo khóa ngoại
    // NULL sẽ là "Danh mục" của hệ thống ngược lại là "Danh mục cá nhân"
    public int? MaNguoiDung { get; set; }

    // Tham chiếu trực tiếp thông tin người dùng nếu khoá ngoại không null
    [ForeignKey(nameof(MaNguoiDung))]
    public mod_NguoiDung? NguoiDung { get; set; }

    // 1 Danh mục có thể xuất hiện trong nhiều "Giao dịch"
    public ICollection<mod_GiaoDich> GiaoDichs { get; set; } = new List<mod_GiaoDich>();
}
