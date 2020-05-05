using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WordsTeacher.Data.Migrations
{
    public partial class AddDeletionTimeAndBasePronunciation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RemaiderTime",
                table: "Phrases");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteTimeUtc",
                table: "Tests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BasePhrasePronunciation",
                table: "Phrases",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteTimeUtc",
                table: "Phrases",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RemaiderTimeUtc",
                table: "Phrases",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteTimeUtc",
                table: "Languages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleteTimeUtc",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "BasePhrasePronunciation",
                table: "Phrases");

            migrationBuilder.DropColumn(
                name: "DeleteTimeUtc",
                table: "Phrases");

            migrationBuilder.DropColumn(
                name: "RemaiderTimeUtc",
                table: "Phrases");

            migrationBuilder.DropColumn(
                name: "DeleteTimeUtc",
                table: "Languages");

            migrationBuilder.AddColumn<DateTime>(
                name: "RemaiderTime",
                table: "Phrases",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
