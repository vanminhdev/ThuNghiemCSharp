using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Performance.Migrations
{
    /// <inheritdoc />
    public partial class Update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentClassrooms_Classroom_ClassroomId",
                table: "StudentClassrooms");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentClassrooms_Student_StudentId",
                table: "StudentClassrooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentClassrooms",
                table: "StudentClassrooms");

            migrationBuilder.DropIndex(
                name: "IX_StudentClassrooms_StudentId",
                table: "StudentClassrooms");

            migrationBuilder.RenameTable(
                name: "StudentClassrooms",
                newName: "StudentClassroom");

            migrationBuilder.RenameIndex(
                name: "IX_StudentClassrooms_ClassroomId",
                table: "StudentClassroom",
                newName: "IX_StudentClassroom_ClassroomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentClassroom",
                table: "StudentClassroom",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_StudentClassroom",
                table: "StudentClassroom",
                columns: new[] { "StudentId", "ClassroomId" });

            migrationBuilder.AddForeignKey(
                name: "FK_StudentClassroom_Classroom_ClassroomId",
                table: "StudentClassroom",
                column: "ClassroomId",
                principalTable: "Classroom",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentClassroom_Student_StudentId",
                table: "StudentClassroom",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentClassroom_Classroom_ClassroomId",
                table: "StudentClassroom");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentClassroom_Student_StudentId",
                table: "StudentClassroom");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentClassroom",
                table: "StudentClassroom");

            migrationBuilder.DropIndex(
                name: "IX_StudentClassroom",
                table: "StudentClassroom");

            migrationBuilder.RenameTable(
                name: "StudentClassroom",
                newName: "StudentClassrooms");

            migrationBuilder.RenameIndex(
                name: "IX_StudentClassroom_ClassroomId",
                table: "StudentClassrooms",
                newName: "IX_StudentClassrooms_ClassroomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentClassrooms",
                table: "StudentClassrooms",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_StudentClassrooms_StudentId",
                table: "StudentClassrooms",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentClassrooms_Classroom_ClassroomId",
                table: "StudentClassrooms",
                column: "ClassroomId",
                principalTable: "Classroom",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentClassrooms_Student_StudentId",
                table: "StudentClassrooms",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
