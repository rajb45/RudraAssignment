using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBIShopify.Migrations
{
    public partial class addedIsAdminProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "Customers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "Customers");
        }
    }
}
