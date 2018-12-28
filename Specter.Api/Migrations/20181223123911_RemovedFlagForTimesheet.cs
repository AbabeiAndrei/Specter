using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Specter.Api.Migrations
{
    public partial class RemovedFlagForTimesheet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Timesheets_Users_UserId1",
                table: "Timesheets");

            migrationBuilder.DropIndex(
                name: "IX_Timesheets_UserId1",
                table: "Timesheets");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Timesheets");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Timesheets",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<bool>(
                name: "Removed",
                table: "Timesheets",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Timesheets_UserId",
                table: "Timesheets",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Timesheets_Users_UserId",
                table: "Timesheets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Timesheets_Users_UserId",
                table: "Timesheets");

            migrationBuilder.DropIndex(
                name: "IX_Timesheets_UserId",
                table: "Timesheets");

            migrationBuilder.DropColumn(
                name: "Removed",
                table: "Timesheets");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Timesheets",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Timesheets",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Timesheets_UserId1",
                table: "Timesheets",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Timesheets_Users_UserId1",
                table: "Timesheets",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
