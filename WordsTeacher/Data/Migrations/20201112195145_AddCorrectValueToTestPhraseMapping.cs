using Microsoft.EntityFrameworkCore.Migrations;

namespace WordsTeacher.Data.Migrations
{
    public partial class AddCorrectValueToTestPhraseMapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Correct",
                table: "PhraseTestMapping",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Correct",
                table: "PhraseTestMapping");
        }
    }
}
