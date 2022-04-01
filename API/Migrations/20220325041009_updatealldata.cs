using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class updatealldata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "University",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "University",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "University",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "University",
                newName: "id");
        }
    }
}
