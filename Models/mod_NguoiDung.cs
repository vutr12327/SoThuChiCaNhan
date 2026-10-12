using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SoThuChiCaNhan;

[Table("NguoiDung")] // Tên bảng bên csdl
public class mod_NguoiDung
{
    [Key] // Khai báo là khoá chính
    public int MaNguoiDung { get; set; } // Khoá chính và tự tăng chỉ số "IDENTITY(1,1)"

    [Required] // Để thuộc tính không được null "NOT NULL"
    [StringLength(50)] // Độ dài tối đa
    public string TenDangNhap { get; set; } = string.Empty;

    [Required]
    [StringLength(255)]
    public string MatKhau { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string HoTen { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18, 2)")] // Chỉ định rõ kiểu dữ liệu là decimal
    public decimal SoDu { get; set; } = 0;

    [StringLength(255)]
    public string? AnhDaiDien { get; set; } = "default_avatar.png";

    [StringLength(64)]
    public string QuocGia { get; set; } = "Việt Nam";

    [Required]
    [StringLength(4)]
    public string GioiTinh { get; set; } = "Nam";

    public int Tuoi { get; set; } = 18;

    public DateTime NgayTao { get; set; } = DateTime.Now;

    // 1 Người dùng có thể tạo nhiều "Danh mục" cá nhân
    public ICollection<mod_DanhMuc> DanhMucs { get; set; } = new List<mod_DanhMuc>();

    // 1 Người dùng có nhiều "Giao dịch"
    public ICollection<mod_GiaoDich> GiaoDichs { get; set; } = new List<mod_GiaoDich>();
}
