using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Performance.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIXStudentClassroomFilteredIX : Migration
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
                columns: new[] { "Name", "MaxStudent" });

            migrationBuilder.CreateIndex(
                name: "IX_Classroom_Status_1",
                table: "Classroom",
                column: "Status",
                filter: "[Status] = 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Classroom",
                table: "Classroom");

            migrationBuilder.DropIndex(
                name: "IX_Classroom_Status_1",
                table: "Classroom");

            migrationBuilder.CreateIndex(
                name: "IX_Classroom",
                table: "Classroom",
                columns: new[] { "Status", "Name" });
        }
    }
}
