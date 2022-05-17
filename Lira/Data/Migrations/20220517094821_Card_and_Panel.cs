using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lira.Data.Migrations
{
    public partial class Card_and_Panel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Panel",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    BoardId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Panel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Panel_Board_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Board",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Card",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    PanelId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Card_Panel_PanelId",
                        column: x => x.PanelId,
                        principalTable: "Panel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Card_PanelId",
                table: "Card",
                column: "PanelId");

            migrationBuilder.CreateIndex(
                name: "IX_Panel_BoardId",
                table: "Panel",
                column: "BoardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Card");

            migrationBuilder.DropTable(
                name: "Panel");
        }
    }
}
