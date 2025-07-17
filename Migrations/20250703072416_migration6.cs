using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoutiqueManagement.Migrations
{
    /// <inheritdoc />
    public partial class migration6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalTransactions_BoutiqueItems_BoutiqueItemId",
                table: "RentalTransactions");

            migrationBuilder.DropIndex(
                name: "IX_RentalTransactions_BoutiqueItemId",
                table: "RentalTransactions");

            migrationBuilder.DropColumn(
                name: "BoutiqueItemId",
                table: "RentalTransactions");

            migrationBuilder.DropColumn(
                name: "DepositAmount",
                table: "RentalTransactions");

            migrationBuilder.DropColumn(
                name: "IsReturned",
                table: "RentalTransactions");

            migrationBuilder.AddColumn<string>(
                name: "ItemCode",
                table: "RentalTransactions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ItemCode",
                table: "BoutiqueItems",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_BoutiqueItems_ItemCode",
                table: "BoutiqueItems",
                column: "ItemCode");

            migrationBuilder.CreateIndex(
                name: "IX_RentalTransactions_ItemCode",
                table: "RentalTransactions",
                column: "ItemCode");

            migrationBuilder.CreateIndex(
                name: "IX_BoutiqueItems_ItemCode",
                table: "BoutiqueItems",
                column: "ItemCode",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalTransactions_BoutiqueItems_ItemCode",
                table: "RentalTransactions",
                column: "ItemCode",
                principalTable: "BoutiqueItems",
                principalColumn: "ItemCode",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalTransactions_BoutiqueItems_ItemCode",
                table: "RentalTransactions");

            migrationBuilder.DropIndex(
                name: "IX_RentalTransactions_ItemCode",
                table: "RentalTransactions");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_BoutiqueItems_ItemCode",
                table: "BoutiqueItems");

            migrationBuilder.DropIndex(
                name: "IX_BoutiqueItems_ItemCode",
                table: "BoutiqueItems");

            migrationBuilder.DropColumn(
                name: "ItemCode",
                table: "RentalTransactions");

            migrationBuilder.AddColumn<int>(
                name: "BoutiqueItemId",
                table: "RentalTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "DepositAmount",
                table: "RentalTransactions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsReturned",
                table: "RentalTransactions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "ItemCode",
                table: "BoutiqueItems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_RentalTransactions_BoutiqueItemId",
                table: "RentalTransactions",
                column: "BoutiqueItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_RentalTransactions_BoutiqueItems_BoutiqueItemId",
                table: "RentalTransactions",
                column: "BoutiqueItemId",
                principalTable: "BoutiqueItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
