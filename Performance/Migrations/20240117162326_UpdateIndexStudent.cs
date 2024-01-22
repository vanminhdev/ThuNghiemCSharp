using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Performance.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIndexStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Student",
                table: "Student");

            migrationBuilder.CreateIndex(
                name: "IX_Student",
                table: "Student",
                columns: new[] { "StudentCode", "Phone", "Email", "DateOfBirth", "Name", "IndustryCode", "MajorCode" },
                descending: new[] { false, false, false, true, false, false, false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Student",
                table: "Student");

            migrationBuilder.CreateIndex(
                name: "IX_Student",
                table: "Student",
                column: "Name");
        }
    }
}
