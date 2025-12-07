using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Service.Migrations
{
    /// <inheritdoc />
    public partial class ExperienceUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ImpactId",
                table: "Experience",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ImpactModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Statement = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImpactModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectModel",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    Technologies = table.Column<string>(type: "TEXT", nullable: true),
                    Contribution = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                    ExperienceId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectModel", x => x.Name);
                    table.ForeignKey(
                        name: "FK_ProjectModel_Experience_ExperienceId",
                        column: x => x.ExperienceId,
                        principalTable: "Experience",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Experience_ImpactId",
                table: "Experience",
                column: "ImpactId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectModel_ExperienceId",
                table: "ProjectModel",
                column: "ExperienceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Experience_ImpactModel_ImpactId",
                table: "Experience",
                column: "ImpactId",
                principalTable: "ImpactModel",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Experience_ImpactModel_ImpactId",
                table: "Experience");

            migrationBuilder.DropTable(
                name: "ImpactModel");

            migrationBuilder.DropTable(
                name: "ProjectModel");

            migrationBuilder.DropIndex(
                name: "IX_Experience_ImpactId",
                table: "Experience");

            migrationBuilder.DropColumn(
                name: "ImpactId",
                table: "Experience");
        }
    }
}
