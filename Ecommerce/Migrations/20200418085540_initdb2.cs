using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce.Migrations
{
    public partial class initdb2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Customer",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "CustomerInfo",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerInfo",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "Customer",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
