using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationTest.Migrations
{
    /// <inheritdoc />
    public partial class DemoDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hobby_Students_StudentId",
                table: "Hobby");

            migrationBuilder.CreateTable(
                name: "EntityPrinciple",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityPrinciple", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntityDependent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EntityPrincipleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityDependent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntityDependent_EntityPrinciple_EntityPrincipleId",
                        column: x => x.EntityPrincipleId,
                        principalTable: "EntityPrinciple",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntityDependent2",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EntityPrincipleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityDependent2", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntityDependent2_EntityPrinciple_EntityPrincipleId",
                        column: x => x.EntityPrincipleId,
                        principalTable: "EntityPrinciple",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntityDependentLevel2",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EntityDependentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityDependentLevel2", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntityDependentLevel2_EntityDependent_EntityDependentId",
                        column: x => x.EntityDependentId,
                        principalTable: "EntityDependent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntityDependent_EntityPrincipleId",
                table: "EntityDependent",
                column: "EntityPrincipleId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityDependent2_EntityPrincipleId",
                table: "EntityDependent2",
                column: "EntityPrincipleId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityDependentLevel2_EntityDependentId",
                table: "EntityDependentLevel2",
                column: "EntityDependentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hobby_Students_StudentId",
                table: "Hobby",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hobby_Students_StudentId",
                table: "Hobby");

            migrationBuilder.DropTable(
                name: "EntityDependent2");

            migrationBuilder.DropTable(
                name: "EntityDependentLevel2");

            migrationBuilder.DropTable(
                name: "EntityDependent");

            migrationBuilder.DropTable(
                name: "EntityPrinciple");

            migrationBuilder.AddForeignKey(
                name: "FK_Hobby_Students_StudentId",
                table: "Hobby",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
