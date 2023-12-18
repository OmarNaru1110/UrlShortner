using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Naru_Shortner.Migrations
{
    public partial class EditedTheDBModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_URLs_ShortURL",
                table: "URLs");

            migrationBuilder.DropColumn(
                name: "ShortURL",
                table: "URLs");

            migrationBuilder.RenameColumn(
                name: "LongURL",
                table: "URLs",
                newName: "ShortenedURL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShortenedURL",
                table: "URLs",
                newName: "LongURL");

            migrationBuilder.AddColumn<string>(
                name: "ShortURL",
                table: "URLs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_URLs_ShortURL",
                table: "URLs",
                column: "ShortURL",
                unique: true);
        }
    }
}
