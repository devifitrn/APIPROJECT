using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class nn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "AccountRole");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "AccountRole",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
