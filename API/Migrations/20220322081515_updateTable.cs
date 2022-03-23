using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class updateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Education_University_University_id",
                table: "Education");

            migrationBuilder.DropForeignKey(
                name: "FK_Profiling_Education_Education_id",
                table: "Profiling");

            migrationBuilder.RenameColumn(
                name: "Education_id",
                table: "Profiling",
                newName: "EducationId");

            migrationBuilder.RenameIndex(
                name: "IX_Profiling_Education_id",
                table: "Profiling",
                newName: "IX_Profiling_EducationId");

            migrationBuilder.RenameColumn(
                name: "University_id",
                table: "Education",
                newName: "UniversityId");

            migrationBuilder.RenameIndex(
                name: "IX_Education_University_id",
                table: "Education",
                newName: "IX_Education_UniversityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Education_University_UniversityId",
                table: "Education",
                column: "UniversityId",
                principalTable: "University",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Profiling_Education_EducationId",
                table: "Profiling",
                column: "EducationId",
                principalTable: "Education",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Education_University_UniversityId",
                table: "Education");

            migrationBuilder.DropForeignKey(
                name: "FK_Profiling_Education_EducationId",
                table: "Profiling");

            migrationBuilder.RenameColumn(
                name: "EducationId",
                table: "Profiling",
                newName: "Education_id");

            migrationBuilder.RenameIndex(
                name: "IX_Profiling_EducationId",
                table: "Profiling",
                newName: "IX_Profiling_Education_id");

            migrationBuilder.RenameColumn(
                name: "UniversityId",
                table: "Education",
                newName: "University_id");

            migrationBuilder.RenameIndex(
                name: "IX_Education_UniversityId",
                table: "Education",
                newName: "IX_Education_University_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Education_University_University_id",
                table: "Education",
                column: "University_id",
                principalTable: "University",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Profiling_Education_Education_id",
                table: "Profiling",
                column: "Education_id",
                principalTable: "Education",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
