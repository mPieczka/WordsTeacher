using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WordsTeacher.Data.Migrations
{
    public partial class AddDatabaseSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDateUtc = table.Column<DateTime>(nullable: false),
                    UpdateTimeUtc = table.Column<DateTime>(nullable: false),
                    FullName = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.Sql("insert into Languages (GetDate(),GetDate(),'Polish','PL-pl'),(GetDate(),GetDate(),'English','En-us')");

            migrationBuilder.CreateTable(
                name: "Phrases",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDateUtc = table.Column<DateTime>(nullable: false),
                    UpdateTimeUtc = table.Column<DateTime>(nullable: false),
                    PhareLanguageId = table.Column<int>(nullable: true),
                    TranslationLanguageId = table.Column<int>(nullable: true),
                    BasePharse = table.Column<string>(nullable: true),
                    TranslatedPharse = table.Column<string>(nullable: true),
                    TranslationPronunciation = table.Column<string>(nullable: true),
                    AudioPath = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    RemaiderTime = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phrases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Phrases_Languages_PhareLanguageId",
                        column: x => x.PhareLanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Phrases_Languages_TranslationLanguageId",
                        column: x => x.TranslationLanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Phrases_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDateUtc = table.Column<DateTime>(nullable: false),
                    UpdateTimeUtc = table.Column<DateTime>(nullable: false),
                    BaseLanguageId = table.Column<int>(nullable: true),
                    TranslationLanguageId = table.Column<int>(nullable: true),
                    CorrectAnswers = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tests_Languages_BaseLanguageId",
                        column: x => x.BaseLanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tests_Languages_TranslationLanguageId",
                        column: x => x.TranslationLanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhraseTestMapping",
                columns: table => new
                {
                    TestId = table.Column<int>(nullable: false),
                    PhraseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhraseTestMapping", x => new { x.TestId, x.PhraseId });
                    table.ForeignKey(
                        name: "FK_PhraseTestMapping_Phrases_PhraseId",
                        column: x => x.PhraseId,
                        principalTable: "Phrases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhraseTestMapping_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Phrases_PhareLanguageId",
                table: "Phrases",
                column: "PhareLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Phrases_TranslationLanguageId",
                table: "Phrases",
                column: "TranslationLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Phrases_UserId",
                table: "Phrases",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PhraseTestMapping_PhraseId",
                table: "PhraseTestMapping",
                column: "PhraseId");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_BaseLanguageId",
                table: "Tests",
                column: "BaseLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_TranslationLanguageId",
                table: "Tests",
                column: "TranslationLanguageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhraseTestMapping");

            migrationBuilder.DropTable(
                name: "Phrases");

            migrationBuilder.DropTable(
                name: "Tests");

            migrationBuilder.DropTable(
                name: "Languages");
        }
    }
}