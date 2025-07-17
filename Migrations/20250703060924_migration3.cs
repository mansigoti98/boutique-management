using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoutiqueManagement.Migrations
{
    /// <inheritdoc />
    public partial class migration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "BoutiqueItems");

            migrationBuilder.DropColumn(
                name: "RenterName",
                table: "BoutiqueItems");

            migrationBuilder.DropColumn(
                name: "ReturnDate",
                table: "BoutiqueItems");

            migrationBuilder.RenameColumn(
                name: "RentDate",
                table: "RentalTransactions",
                newName: "RentalStartDate");

            migrationBuilder.RenameColumn(
                name: "DueDate",
                table: "RentalTransactions",
                newName: "RentalEndDate");

            migrationBuilder.RenameColumn(
                name: "PurchasePrice",
                table: "BoutiqueItems",
                newName: "Price");

            migrationBuilder.AddColumn<DateTime>(
                name: "BookedOn",
                table: "RentalTransactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "BoutiqueItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ItemCode",
                table: "BoutiqueItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookedOn",
                table: "RentalTransactions");

            migrationBuilder.DropColumn(
                name: "DepositAmount",
                table: "RentalTransactions");

            migrationBuilder.DropColumn(
                name: "IsReturned",
                table: "RentalTransactions");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "BoutiqueItems");

            migrationBuilder.DropColumn(
                name: "ItemCode",
                table: "BoutiqueItems");

            migrationBuilder.RenameColumn(
                name: "RentalStartDate",
                table: "RentalTransactions",
                newName: "RentDate");

            migrationBuilder.RenameColumn(
                name: "RentalEndDate",
                table: "RentalTransactions",
                newName: "DueDate");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "BoutiqueItems",
                newName: "PurchasePrice");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "BoutiqueItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RenterName",
                table: "BoutiqueItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReturnDate",
                table: "BoutiqueItems",
                type: "datetime2",
                nullable: true);
        }
    }
}
