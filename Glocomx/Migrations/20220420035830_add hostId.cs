using Microsoft.EntityFrameworkCore.Migrations;

namespace Glocomx.Migrations
{
    public partial class addhostId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_AspNetUsers_HostId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_HostId",
                table: "Schedules");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_AspNetUsers_HostId",
                table: "Schedules",
                column: "HostId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "HostId",
                table: "Schedules",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_HostId",
                table: "Schedules",
                column: "HostId");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_AspNetUsers_HostId",
                table: "Schedules");
        }
    }
}
