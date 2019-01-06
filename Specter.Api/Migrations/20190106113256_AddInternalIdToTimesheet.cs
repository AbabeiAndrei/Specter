using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Specter.Api.Migrations
{
    public partial class AddInternalIdToTimesheet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "DeliveryId",
                table: "Timesheets",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<int>(
                name: "InternalId",
                table: "Timesheets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "WorkItemIdPrefix",
                table: "Projects",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Timesheets_InternalId",
                table: "Timesheets",
                column: "InternalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Timesheets_InternalId",
                table: "Timesheets");

            migrationBuilder.DropColumn(
                name: "InternalId",
                table: "Timesheets");

            migrationBuilder.DropColumn(
                name: "WorkItemIdPrefix",
                table: "Projects");

            migrationBuilder.AlterColumn<Guid>(
                name: "DeliveryId",
                table: "Timesheets",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);
        }
    }
}
