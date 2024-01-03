using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Performance.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIXClassroomOnlyMaxStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Classroom",
                table: "Classroom");

            migrationBuilder.CreateIndex(
                name: "IX_Classroom",
                table: "Classroom",
                column: "MaxStudent");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Classroom",
                table: "Classroom");

            migrationBuilder.CreateIndex(
                name: "IX_Classroom",
                table: "Classroom",
                columns: new[] { "MaxStudent", "Name" });
        }
    }
}
