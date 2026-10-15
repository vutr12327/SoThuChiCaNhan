using Microsoft.EntityFrameworkCore;
namespace SoThuChiCaNhan;

public partial class AppDbContext : DbContext
{
    /// <summary>
    /// Cung cấp quyền truy cập và thao tác với các thực thể trong cơ sở dữ liệu.
    /// </summary>
    public DbSet<mod_NguoiDung> NguoiDung { get; set; }
    public DbSet<mod_GiaoDich> GiaoDich { get; set; }
    public DbSet<mod_DanhMuc> DanhMuc { get; set; }
    public DbSet<mod_TinNhanAI> TinNhanAI { get; set; }

    /// <summary>
    /// Cấu hình cấu trúc và các quy tắc cho các thực thể.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<mod_NguoiDung>().HasIndex(nd => nd.TenDangNhap).IsUnique();
    }

    /// <summary>
    /// Cấu hình kết nối đến cơ sở dữ liệu SQL Server.
    /// </summary>
    protected override void OnConfiguring(DbContextOptionsBuilder op)
    {
        if (!op.IsConfigured)
        {
            string connectionString = "Server=.\\SQLEXPRESS;" +
                                      "Database=DBSoThuChiCaNhan;" +
                                      "Trusted_Connection=True;" +
                                      "TrustServerCertificate=True;";
            op.UseSqlServer(connectionString);
        }
    }
}
