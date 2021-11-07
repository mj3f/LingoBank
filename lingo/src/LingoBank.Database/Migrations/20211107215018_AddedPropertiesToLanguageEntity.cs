using Microsoft.EntityFrameworkCore.Migrations;

namespace LingoBank.Database.Migrations
{
    public partial class AddedPropertiesToLanguageEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Languages",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Languages",
                type: "longtext",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Languages");
        }
    }
}
