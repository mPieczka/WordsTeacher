using Microsoft.EntityFrameworkCore.Migrations;

namespace WordsTeacher.Data.Migrations
{
    public partial class FixPhraseTypeo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BasePharse",
                table: "Phrases");

            migrationBuilder.DropColumn(
                name: "TranslatedPharse",
                table: "Phrases");

            migrationBuilder.AddColumn<string>(
                name: "BasePhrase",
                table: "Phrases",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TranslatedPhrase",
                table: "Phrases",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BasePhrase",
                table: "Phrases");

            migrationBuilder.DropColumn(
                name: "TranslatedPhrase",
                table: "Phrases");

            migrationBuilder.AddColumn<string>(
                name: "BasePharse",
                table: "Phrases",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TranslatedPharse",
                table: "Phrases",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
