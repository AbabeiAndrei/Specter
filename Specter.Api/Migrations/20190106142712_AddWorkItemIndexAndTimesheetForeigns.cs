using Microsoft.EntityFrameworkCore.Migrations;

namespace Specter.Api.Migrations
{
    public partial class AddWorkItemIndexAndTimesheetForeigns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "WorkItemIdPrefix",
                table: "Projects",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Timesheets_CategoryId",
                table: "Timesheets",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Timesheets_DeliveryId",
                table: "Timesheets",
                column: "DeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_WorkItemIdPrefix",
                table: "Projects",
                column: "WorkItemIdPrefix",
                unique: true,
                filter: "[WorkItemIdPrefix] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Timesheets_Categories_CategoryId",
                table: "Timesheets",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Timesheets_Deliveries_DeliveryId",
                table: "Timesheets",
                column: "DeliveryId",
                principalTable: "Deliveries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Timesheets_Categories_CategoryId",
                table: "Timesheets");

            migrationBuilder.DropForeignKey(
                name: "FK_Timesheets_Deliveries_DeliveryId",
                table: "Timesheets");

            migrationBuilder.DropIndex(
                name: "IX_Timesheets_CategoryId",
                table: "Timesheets");

            migrationBuilder.DropIndex(
                name: "IX_Timesheets_DeliveryId",
                table: "Timesheets");

            migrationBuilder.DropIndex(
                name: "IX_Projects_WorkItemIdPrefix",
                table: "Projects");

            migrationBuilder.AlterColumn<string>(
                name: "WorkItemIdPrefix",
                table: "Projects",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
