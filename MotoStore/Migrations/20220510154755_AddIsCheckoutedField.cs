using Microsoft.EntityFrameworkCore.Migrations;

namespace MotoStore.Migrations
{
    public partial class AddIsCheckoutedField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCheckouted",
                table: "Orders",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCheckouted",
                table: "Orders");
        }
    }
}
