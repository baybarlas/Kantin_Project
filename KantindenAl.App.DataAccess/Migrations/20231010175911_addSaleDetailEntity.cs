using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KantindenAl.App.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addSaleDetailEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "Carts");

            migrationBuilder.AddColumn<string>(
                name: "ReceiptNo",
                table: "Sales",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiptNo",
                table: "Sales");

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderDate",
                table: "Carts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "Carts",
                type: "int",
                nullable: true);
        }
    }
}
