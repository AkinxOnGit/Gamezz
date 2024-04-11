using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamezz.Data.Migrations
{
    public partial class ids : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GamesOrders_Orders_OrdersId",
                table: "GamesOrders");

            migrationBuilder.RenameColumn(
                name: "OrdersId",
                table: "GamesOrders",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_GamesOrders_OrdersId",
                table: "GamesOrders",
                newName: "IX_GamesOrders_OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_GamesOrders_Orders_OrderId",
                table: "GamesOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GamesOrders_Orders_OrderId",
                table: "GamesOrders");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "GamesOrders",
                newName: "OrdersId");

            migrationBuilder.RenameIndex(
                name: "IX_GamesOrders_OrderId",
                table: "GamesOrders",
                newName: "IX_GamesOrders_OrdersId");

            migrationBuilder.AddForeignKey(
                name: "FK_GamesOrders_Orders_OrdersId",
                table: "GamesOrders",
                column: "OrdersId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
