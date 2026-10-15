using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoThuChiCaNhan.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class ThemBangTinNhanAI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TinNhanAI",
                columns: table => new
                {
                    MaTinNhanAI = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TinNhanCuaBan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TinNhanCuaAI = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThoiGianNhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaNguoiDung = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TinNhanAI", x => x.MaTinNhanAI);
                    table.ForeignKey(
                        name: "FK_TinNhanAI_NguoiDung_MaNguoiDung",
                        column: x => x.MaNguoiDung,
                        principalTable: "NguoiDung",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TinNhanAI_MaNguoiDung",
                table: "TinNhanAI",
                column: "MaNguoiDung");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TinNhanAI");
        }
    }
}
