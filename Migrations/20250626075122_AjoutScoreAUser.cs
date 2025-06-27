using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAINTJWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AjoutScoreAUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "score",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "score",
                table: "Users");
        }
    }
}
