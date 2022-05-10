using Microsoft.EntityFrameworkCore.Migrations;

namespace MotoStore.Migrations
{
    public partial class AddPathToAvatarField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PathToAvatar",
                table: "AspNetUsers",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PathToAvatar",
                table: "AspNetUsers");
        }
    }
}
