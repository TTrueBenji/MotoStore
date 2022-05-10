using Microsoft.EntityFrameworkCore.Migrations;

namespace MotoStore.Migrations
{
    public partial class AddConfirmedField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LiveOrders_AspNetUsers_UserId",
                table: "LiveOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_LiveOrders_Orders_OrderId",
                table: "LiveOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_LiveOrders_Positions_PositionId",
                table: "LiveOrders");

            migrationBuilder.AddColumn<bool>(
                name: "Confirmed",
                table: "Orders",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "LiveOrders",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PositionId",
                table: "LiveOrders",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OrderId",
                table: "LiveOrders",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Confirmed",
                table: "LiveOrders",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_LiveOrders_AspNetUsers_UserId",
                table: "LiveOrders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LiveOrders_Orders_OrderId",
                table: "LiveOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LiveOrders_Positions_PositionId",
                table: "LiveOrders",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LiveOrders_AspNetUsers_UserId",
                table: "LiveOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_LiveOrders_Orders_OrderId",
                table: "LiveOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_LiveOrders_Positions_PositionId",
                table: "LiveOrders");

            migrationBuilder.DropColumn(
                name: "Confirmed",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Confirmed",
                table: "LiveOrders");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "LiveOrders",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "PositionId",
                table: "LiveOrders",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "OrderId",
                table: "LiveOrders",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_LiveOrders_AspNetUsers_UserId",
                table: "LiveOrders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LiveOrders_Orders_OrderId",
                table: "LiveOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LiveOrders_Positions_PositionId",
                table: "LiveOrders",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
