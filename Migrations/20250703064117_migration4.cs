using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoutiqueManagement.Migrations
{
    /// <inheritdoc />
    public partial class migration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "BoutiqueItems");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "BoutiqueItems");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "BoutiqueItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoutiqueItems_CategoryId",
                table: "BoutiqueItems",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoutiqueItems_Categories_CategoryId",
                table: "BoutiqueItems",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoutiqueItems_Categories_CategoryId",
                table: "BoutiqueItems");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_BoutiqueItems_CategoryId",
                table: "BoutiqueItems");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "BoutiqueItems");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "BoutiqueItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "BoutiqueItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
