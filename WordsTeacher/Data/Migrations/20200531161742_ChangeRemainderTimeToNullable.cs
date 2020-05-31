using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WordsTeacher.Data.Migrations
{
    public partial class ChangeRemainderTimeToNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RemaiderTimeUtc",
                table: "Phrases",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RemaiderTimeUtc",
                table: "Phrases",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
