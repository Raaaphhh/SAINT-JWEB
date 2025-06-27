using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SAINTJWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AjoutModelDefisUserdefi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Defis",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    titre = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    difficulte = table.Column<string>(type: "text", nullable: false),
                    points = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Defis", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "UserDefis",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userId = table.Column<int>(type: "integer", nullable: false),
                    defiId = table.Column<int>(type: "integer", nullable: false),
                    estAccompli = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDefis", x => x.id);
                    table.ForeignKey(
                        name: "FK_UserDefis_Defis_defiId",
                        column: x => x.defiId,
                        principalTable: "Defis",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserDefis_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "idUSer",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserDefis_defiId",
                table: "UserDefis",
                column: "defiId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDefis_userId",
                table: "UserDefis",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDefis");

            migrationBuilder.DropTable(
                name: "Defis");
        }
    }
}
