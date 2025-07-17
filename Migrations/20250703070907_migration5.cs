using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoutiqueManagement.Migrations
{
    /// <inheritdoc />
    public partial class migration5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoutiqueItems_Categories_CategoryId",
                table: "BoutiqueItems");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "BoutiqueItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_BoutiqueItems_Categories_CategoryId",
                table: "BoutiqueItems",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoutiqueItems_Categories_CategoryId",
                table: "BoutiqueItems");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "BoutiqueItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BoutiqueItems_Categories_CategoryId",
                table: "BoutiqueItems",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
