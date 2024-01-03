using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Performance.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIXStudentNotFilterdDeleted : Migration
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
                column: "Name");
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
                columns: new[] { "Deleted", "Name" });
        }
    }
}
