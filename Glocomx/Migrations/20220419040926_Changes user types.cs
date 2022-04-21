using Microsoft.EntityFrameworkCore.Migrations;

namespace Glocomx.Migrations
{
    public partial class Changesusertypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_AspNetUsers_UserId1",
                table: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Chats_UserId1",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Chats");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Chats",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_UserId",
                table: "Chats",
                column: "UserId");

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_AspNetUsers_UserId",
                table: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Chats_UserId",
                table: "Chats");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Chats",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Chats",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chats_UserId1",
                table: "Chats",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_AspNetUsers_UserId1",
                table: "Chats",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
