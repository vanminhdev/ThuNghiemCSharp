using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationTest.Migrations
{
    /// <inheritdoc />
    public partial class Init4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hobby_Students_StudentId",
                table: "Hobby");

            migrationBuilder.AddForeignKey(
                name: "FK_Hobby_Students_StudentId",
                table: "Hobby",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hobby_Students_StudentId",
                table: "Hobby");

            migrationBuilder.AddForeignKey(
                name: "FK_Hobby_Students_StudentId",
                table: "Hobby",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }
    }
}
