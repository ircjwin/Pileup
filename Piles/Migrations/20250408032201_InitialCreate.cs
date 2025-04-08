using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Piles.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Piles",
                columns: table => new
                {
                    Origin = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Piles", x => new { x.Origin, x.CreatedOn });
                });

            migrationBuilder.CreateTable(
                name: "Ruminations",
                columns: table => new
                {
                    Origin = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    PileOrigin = table.Column<int>(type: "INTEGER", nullable: false),
                    PileCreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ruminations", x => new { x.Origin, x.CreatedOn });
                    table.ForeignKey(
                        name: "FK_Ruminations_Piles_PileOrigin_PileCreatedOn",
                        columns: x => new { x.PileOrigin, x.PileCreatedOn },
                        principalTable: "Piles",
                        principalColumns: new[] { "Origin", "CreatedOn" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ruminations_PileOrigin_PileCreatedOn",
                table: "Ruminations",
                columns: new[] { "PileOrigin", "PileCreatedOn" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ruminations");

            migrationBuilder.DropTable(
                name: "Piles");
        }
    }
}
