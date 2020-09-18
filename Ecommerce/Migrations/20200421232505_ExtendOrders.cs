using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce.Migrations
{
    public partial class ExtendOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CardNumber",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExpDay",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameOnCard",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecCode",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardNumber",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ExpDay",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "NameOnCard",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SecCode",
                table: "Orders");
        }
    }
}
