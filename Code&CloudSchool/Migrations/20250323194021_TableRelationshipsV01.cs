using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Code_CloudSchool.Migrations
{
    /// <inheritdoc />
    public partial class TableRelationshipsV01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoursesMajors_Courses_CoursesId",
                table: "CoursesMajors");

            migrationBuilder.DropForeignKey(
                name: "FK_CoursesMajors_Majors_MajorsId",
                table: "CoursesMajors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoursesMajors",
                table: "CoursesMajors");

            migrationBuilder.RenameTable(
                name: "CoursesMajors",
                newName: "MajorCourses");

            migrationBuilder.RenameIndex(
                name: "IX_CoursesMajors_MajorsId",
                table: "MajorCourses",
                newName: "IX_MajorCourses_MajorsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MajorCourses",
                table: "MajorCourses",
                columns: new[] { "CoursesId", "MajorsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MajorCourses_Courses_CoursesId",
                table: "MajorCourses",
                column: "CoursesId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MajorCourses_Majors_MajorsId",
                table: "MajorCourses",
                column: "MajorsId",
                principalTable: "Majors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MajorCourses_Courses_CoursesId",
                table: "MajorCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_MajorCourses_Majors_MajorsId",
                table: "MajorCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MajorCourses",
                table: "MajorCourses");

            migrationBuilder.RenameTable(
                name: "MajorCourses",
                newName: "CoursesMajors");

            migrationBuilder.RenameIndex(
                name: "IX_MajorCourses_MajorsId",
                table: "CoursesMajors",
                newName: "IX_CoursesMajors_MajorsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoursesMajors",
                table: "CoursesMajors",
                columns: new[] { "CoursesId", "MajorsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CoursesMajors_Courses_CoursesId",
                table: "CoursesMajors",
                column: "CoursesId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CoursesMajors_Majors_MajorsId",
                table: "CoursesMajors",
                column: "MajorsId",
                principalTable: "Majors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
