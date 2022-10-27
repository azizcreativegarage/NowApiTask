using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NowApiTask.Migrations
{
    public partial class NewMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "device",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "firstname",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ipaddress",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "lastname",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "device",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "firstname",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ipaddress",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "lastname",
                table: "AspNetUsers");
        }
    }
}
