using Microsoft.EntityFrameworkCore.Migrations;

namespace MotoStore.Migrations
{
    public partial class RemoveIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_PositionId_UserId",
                table: "Orders");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PositionId",
                table: "Orders",
                column: "PositionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_PositionId",
                table: "Orders");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PositionId_UserId",
                table: "Orders",
                columns: new[] { "PositionId", "UserId" },
                unique: true);
        }
    }
}
