using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFirstWebApp.Data.Migrations
{
    public partial class Aprl24 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Note");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Note",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
