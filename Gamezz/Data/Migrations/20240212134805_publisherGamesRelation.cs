using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamezz.Data.Migrations
{
    public partial class publisherGamesRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Publisher_PublisherId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_PublisherId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "PublisherId",
                table: "Games");

            migrationBuilder.CreateTable(
                name: "GamesPublisher",
                columns: table => new
                {
                    GamesId = table.Column<int>(type: "int", nullable: false),
                    PublisherId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamesPublisher", x => new { x.GamesId, x.PublisherId });
                    table.ForeignKey(
                        name: "FK_GamesPublisher_Games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamesPublisher_Publisher_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publisher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GamesPublisher_PublisherId",
                table: "GamesPublisher",
                column: "PublisherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GamesPublisher");

            migrationBuilder.AddColumn<int>(
                name: "PublisherId",
                table: "Games",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_PublisherId",
                table: "Games",
                column: "PublisherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Publisher_PublisherId",
                table: "Games",
                column: "PublisherId",
                principalTable: "Publisher",
                principalColumn: "Id");
        }
    }
}
