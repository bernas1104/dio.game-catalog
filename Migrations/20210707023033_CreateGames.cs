using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace dio.game_catalog.Migrations
{
    public partial class CreateGames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "games",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    produtora = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    preco = table.Column<double>(type: "double precision", precision: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_games", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "games");
        }
    }
}
