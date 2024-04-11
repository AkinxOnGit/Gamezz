using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamezz.Data.Migrations
{
    public partial class PG : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PgId",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Pg",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgeRestriction = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pg", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_PgId",
                table: "Games",
                column: "PgId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Pg_PgId",
                table: "Games",
                column: "PgId",
                principalTable: "Pg",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Pg_PgId",
                table: "Games");

            migrationBuilder.DropTable(
                name: "Pg");

            migrationBuilder.DropIndex(
                name: "IX_Games_PgId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "PgId",
                table: "Games");
        }
    }
}
