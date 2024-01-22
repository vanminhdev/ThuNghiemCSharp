using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Performance.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStudentColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Student",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Student",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Student",
                type: "varchar(128)",
                unicode: false,
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IndustryCode",
                table: "Student",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MajorCode",
                table: "Student",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Student",
                type: "varchar(128)",
                unicode: false,
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentCode",
                table: "Student",
                type: "varchar(128)",
                unicode: false,
                maxLength: 128,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "IndustryCode",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "MajorCode",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "StudentCode",
                table: "Student");
        }
    }
}
