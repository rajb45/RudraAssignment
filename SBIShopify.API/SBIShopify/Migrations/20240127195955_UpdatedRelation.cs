using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBIShopify.Migrations
{
    public partial class UpdatedRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Products_ProductsId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_ProductsId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "ProductsId",
                table: "Carts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProductsId",
                table: "Carts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ProductsId",
                table: "Carts",
                column: "ProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Products_ProductsId",
                table: "Carts",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
