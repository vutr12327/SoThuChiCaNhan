using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoThuChiCaNhan.Migrations
{
    /// <inheritdoc />
    public partial class ThemThuocTinhChoMod_NguoiDung : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GioiTinh",
                table: "NguoiDung",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "QuocGia",
                table: "NguoiDung",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Tuoi",
                table: "NguoiDung",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GioiTinh",
                table: "NguoiDung");

            migrationBuilder.DropColumn(
                name: "QuocGia",
                table: "NguoiDung");

            migrationBuilder.DropColumn(
                name: "Tuoi",
                table: "NguoiDung");
        }
    }
}
