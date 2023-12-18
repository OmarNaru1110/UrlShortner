using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Naru_Shortner.Migrations
{
    public partial class smallErrorHehe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShortenedURL",
                table: "URLs",
                newName: "LongUrl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LongUrl",
                table: "URLs",
                newName: "ShortenedURL");
        }
    }
}
