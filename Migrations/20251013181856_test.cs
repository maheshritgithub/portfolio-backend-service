using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Service.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppConfig",
                columns: table => new
                {
                    Key = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppConfig", x => x.Key);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppConfig");
        }
    }
}
