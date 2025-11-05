using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Service.Migrations
{
    /// <inheritdoc />
    public partial class AllowMultipleExperiencesPerUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserDetail_UserId",
                table: "UserDetail");

            migrationBuilder.DropIndex(
                name: "IX_Experience_UserId",
                table: "Experience");

            migrationBuilder.CreateIndex(
                name: "IX_UserDetail_UserId",
                table: "UserDetail",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Experience_UserId",
                table: "Experience",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserDetail_UserId",
                table: "UserDetail");

            migrationBuilder.DropIndex(
                name: "IX_Experience_UserId",
                table: "Experience");

            migrationBuilder.CreateIndex(
                name: "IX_UserDetail_UserId",
                table: "UserDetail",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Experience_UserId",
                table: "Experience",
                column: "UserId",
                unique: true);
        }
    }
}
