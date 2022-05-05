using Microsoft.EntityFrameworkCore.Migrations;

namespace Glocomx.Migrations
{
    public partial class removedsessionid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LiveSessionId",
                table: "Schedules");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LiveSessionId",
                table: "Schedules",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
