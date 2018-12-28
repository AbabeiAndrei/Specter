using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Specter.Api.Migrations
{
    public partial class AddCategoryDeliveryAndProjectToTimesheet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Timesheets_Users_UserId",
                table: "Timesheets");

            migrationBuilder.DropIndex(
                name: "IX_Timesheets_UserId",
                table: "Timesheets");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Timesheets",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Timesheets",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DeliveryId",
                table: "Timesheets",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Timesheets_ApplicationUserId",
                table: "Timesheets",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Timesheets_Users_ApplicationUserId",
                table: "Timesheets",
                column: "ApplicationUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Timesheets_Users_ApplicationUserId",
                table: "Timesheets");

            migrationBuilder.DropIndex(
                name: "IX_Timesheets_ApplicationUserId",
                table: "Timesheets");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Timesheets");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Timesheets");

            migrationBuilder.DropColumn(
                name: "DeliveryId",
                table: "Timesheets");

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
    }
}
