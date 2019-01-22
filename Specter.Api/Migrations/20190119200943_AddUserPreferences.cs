using Microsoft.EntityFrameworkCore.Migrations;

namespace Specter.Api.Migrations
{
    public partial class AddUserPreferences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Preferences",
                table: "Users",
                nullable: true,
                defaultValue: "{\"DarkMode\":false}");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Preferences",
                table: "Users");
        }
    }
}
