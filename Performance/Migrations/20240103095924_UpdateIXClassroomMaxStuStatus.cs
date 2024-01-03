using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Performance.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIXClassroomMaxStuStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Classroom",
                table: "Classroom");

            migrationBuilder.DropIndex(
                name: "IX_Classroom_Status",
                table: "Classroom");

            migrationBuilder.CreateIndex(
                name: "IX_Classroom",
                table: "Classroom",
                columns: new[] { "MaxStudent", "Status" })
                .Annotation("SqlServer:Include", new[] { "Name" });
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
                column: "MaxStudent")
                .Annotation("SqlServer:Include", new[] { "Name", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_Classroom_Status",
                table: "Classroom",
                column: "Status");
        }
    }
}
