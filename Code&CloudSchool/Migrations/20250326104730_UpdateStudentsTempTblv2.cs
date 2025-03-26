using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Code_CloudSchool.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStudentsTempTblv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Courses_CoursesId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_CoursesId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CoursesId",
                table: "Students");

            migrationBuilder.CreateTable(
                name: "CoursesStudents",
                columns: table => new
                {
                    CoursesId = table.Column<int>(type: "integer", nullable: false),
                    StudentsStudentNumber = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursesStudents", x => new { x.CoursesId, x.StudentsStudentNumber });
                    table.ForeignKey(
                        name: "FK_CoursesStudents_Courses_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoursesStudents_Students_StudentsStudentNumber",
                        column: x => x.StudentsStudentNumber,
                        principalTable: "Students",
                        principalColumn: "StudentNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoursesStudents_StudentsStudentNumber",
                table: "CoursesStudents",
                column: "StudentsStudentNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoursesStudents");

            migrationBuilder.AddColumn<int>(
                name: "CoursesId",
                table: "Students",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_CoursesId",
                table: "Students",
                column: "CoursesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Courses_CoursesId",
                table: "Students",
                column: "CoursesId",
                principalTable: "Courses",
                principalColumn: "Id");
        }
    }
}
