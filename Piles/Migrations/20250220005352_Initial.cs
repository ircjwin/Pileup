using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Piles.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Piles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Justification = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Piles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ruminations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    PileId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ruminations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ruminations_Piles_PileId",
                        column: x => x.PileId,
                        principalTable: "Piles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ruminations_PileId",
                table: "Ruminations",
                column: "PileId");
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
