using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Performance.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIXClassroomInclude : Migration
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
                column: "MaxStudent")
                .Annotation("SqlServer:Include", new[] { "Name", "Status" });
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
                column: "MaxStudent");
        }
    }
}
