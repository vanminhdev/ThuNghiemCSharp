using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationTest.Migrations
{
    /// <inheritdoc />
    public partial class DemoDeleteBehavior3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntityDependent2_EntityPrinciple_EntityPrincipleId",
                table: "EntityDependent2");

            migrationBuilder.DropForeignKey(
                name: "FK_EntityDependentLevel2_EntityDependent_EntityDependentId",
                table: "EntityDependentLevel2");

            migrationBuilder.AlterColumn<int>(
                name: "EntityPrincipleId",
                table: "EntityDependent",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_EntityDependent2_EntityPrinciple_EntityPrincipleId",
                table: "EntityDependent2",
                column: "EntityPrincipleId",
                principalTable: "EntityPrinciple",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EntityDependentLevel2_EntityDependent_EntityDependentId",
                table: "EntityDependentLevel2",
                column: "EntityDependentId",
                principalTable: "EntityDependent",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntityDependent2_EntityPrinciple_EntityPrincipleId",
                table: "EntityDependent2");

            migrationBuilder.DropForeignKey(
                name: "FK_EntityDependentLevel2_EntityDependent_EntityDependentId",
                table: "EntityDependentLevel2");

            migrationBuilder.AlterColumn<int>(
                name: "EntityPrincipleId",
                table: "EntityDependent",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EntityDependent2_EntityPrinciple_EntityPrincipleId",
                table: "EntityDependent2",
                column: "EntityPrincipleId",
                principalTable: "EntityPrinciple",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EntityDependentLevel2_EntityDependent_EntityDependentId",
                table: "EntityDependentLevel2",
                column: "EntityDependentId",
                principalTable: "EntityDependent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
