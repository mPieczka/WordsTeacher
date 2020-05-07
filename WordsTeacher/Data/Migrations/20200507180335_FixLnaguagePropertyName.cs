using Microsoft.EntityFrameworkCore.Migrations;

namespace WordsTeacher.Data.Migrations
{
    public partial class FixLnaguagePropertyName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Phrases_Languages_PhareLanguageId",
                table: "Phrases");

            migrationBuilder.DropIndex(
                name: "IX_Phrases_PhareLanguageId",
                table: "Phrases");

            migrationBuilder.DropColumn(
                name: "PhareLanguageId",
                table: "Phrases");

            migrationBuilder.AddColumn<int>(
                name: "PhraseLanguageId",
                table: "Phrases",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Phrases_PhraseLanguageId",
                table: "Phrases",
                column: "PhraseLanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Phrases_Languages_PhraseLanguageId",
                table: "Phrases",
                column: "PhraseLanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Phrases_Languages_PhraseLanguageId",
                table: "Phrases");

            migrationBuilder.DropIndex(
                name: "IX_Phrases_PhraseLanguageId",
                table: "Phrases");

            migrationBuilder.DropColumn(
                name: "PhraseLanguageId",
                table: "Phrases");

            migrationBuilder.AddColumn<int>(
                name: "PhareLanguageId",
                table: "Phrases",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Phrases_PhareLanguageId",
                table: "Phrases",
                column: "PhareLanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Phrases_Languages_PhareLanguageId",
                table: "Phrases",
                column: "PhareLanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
