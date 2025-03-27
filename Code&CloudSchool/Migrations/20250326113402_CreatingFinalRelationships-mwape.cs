using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Code_CloudSchool.Migrations
{
    /// <inheritdoc />
    public partial class CreatingFinalRelationshipsmwape : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Classes_ClassesclassID",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_CoursesStudents_Courses_CoursesId",
                table: "CoursesStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_CoursesStudents_Students_StudentsStudentNumber",
                table: "CoursesStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_Lecturers_Classes_ClassesclassID",
                table: "Lecturers");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Classes_ClassesclassID",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_ClassesclassID",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Lecturers_ClassesclassID",
                table: "Lecturers");

            migrationBuilder.DropIndex(
                name: "IX_Courses_ClassesclassID",
                table: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoursesStudents",
                table: "CoursesStudents");

            migrationBuilder.DropColumn(
                name: "ClassesclassID",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ClassesclassID",
                table: "Lecturers");

            migrationBuilder.DropColumn(
                name: "ClassesclassID",
                table: "Courses");

            migrationBuilder.RenameTable(
                name: "CoursesStudents",
                newName: "CourseStudents");

            migrationBuilder.RenameIndex(
                name: "IX_CoursesStudents_StudentsStudentNumber",
                table: "CourseStudents",
                newName: "IX_CourseStudents_StudentsStudentNumber");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Classes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseStudents",
                table: "CourseStudents",
                columns: new[] { "CoursesId", "StudentsStudentNumber" });

            migrationBuilder.CreateTable(
                name: "ClassLecturers",
                columns: table => new
                {
                    ClassesclassID = table.Column<int>(type: "integer", nullable: false),
                    lecturersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassLecturers", x => new { x.ClassesclassID, x.lecturersId });
                    table.ForeignKey(
                        name: "FK_ClassLecturers_Classes_ClassesclassID",
                        column: x => x.ClassesclassID,
                        principalTable: "Classes",
                        principalColumn: "classID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassLecturers_Lecturers_lecturersId",
                        column: x => x.lecturersId,
                        principalTable: "Lecturers",
                        principalColumn: "lecturersId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassStudents",
                columns: table => new
                {
                    ClassesclassID = table.Column<int>(type: "integer", nullable: false),
                    StudentsStudentNumber = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassStudents", x => new { x.ClassesclassID, x.StudentsStudentNumber });
                    table.ForeignKey(
                        name: "FK_ClassStudents_Classes_ClassesclassID",
                        column: x => x.ClassesclassID,
                        principalTable: "Classes",
                        principalColumn: "classID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassStudents_Students_StudentsStudentNumber",
                        column: x => x.StudentsStudentNumber,
                        principalTable: "Students",
                        principalColumn: "StudentNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classes_CourseId",
                table: "Classes",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassLecturers_lecturersId",
                table: "ClassLecturers",
                column: "lecturersId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassStudents_StudentsStudentNumber",
                table: "ClassStudents",
                column: "StudentsStudentNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Courses_CourseId",
                table: "Classes",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseStudents_Courses_CoursesId",
                table: "CourseStudents",
                column: "CoursesId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseStudents_Students_StudentsStudentNumber",
                table: "CourseStudents",
                column: "StudentsStudentNumber",
                principalTable: "Students",
                principalColumn: "StudentNumber",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Courses_CourseId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseStudents_Courses_CoursesId",
                table: "CourseStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseStudents_Students_StudentsStudentNumber",
                table: "CourseStudents");

            migrationBuilder.DropTable(
                name: "ClassLecturers");

            migrationBuilder.DropTable(
                name: "ClassStudents");

            migrationBuilder.DropIndex(
                name: "IX_Classes_CourseId",
                table: "Classes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseStudents",
                table: "CourseStudents");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Classes");

            migrationBuilder.RenameTable(
                name: "CourseStudents",
                newName: "CoursesStudents");

            migrationBuilder.RenameIndex(
                name: "IX_CourseStudents_StudentsStudentNumber",
                table: "CoursesStudents",
                newName: "IX_CoursesStudents_StudentsStudentNumber");

            migrationBuilder.AddColumn<int>(
                name: "ClassesclassID",
                table: "Students",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClassesclassID",
                table: "Lecturers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClassesclassID",
                table: "Courses",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoursesStudents",
                table: "CoursesStudents",
                columns: new[] { "CoursesId", "StudentsStudentNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_Students_ClassesclassID",
                table: "Students",
                column: "ClassesclassID");

            migrationBuilder.CreateIndex(
                name: "IX_Lecturers_ClassesclassID",
                table: "Lecturers",
                column: "ClassesclassID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ClassesclassID",
                table: "Courses",
                column: "ClassesclassID");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Classes_ClassesclassID",
                table: "Courses",
                column: "ClassesclassID",
                principalTable: "Classes",
                principalColumn: "classID");

            migrationBuilder.AddForeignKey(
                name: "FK_CoursesStudents_Courses_CoursesId",
                table: "CoursesStudents",
                column: "CoursesId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CoursesStudents_Students_StudentsStudentNumber",
                table: "CoursesStudents",
                column: "StudentsStudentNumber",
                principalTable: "Students",
                principalColumn: "StudentNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lecturers_Classes_ClassesclassID",
                table: "Lecturers",
                column: "ClassesclassID",
                principalTable: "Classes",
                principalColumn: "classID");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Classes_ClassesclassID",
                table: "Students",
                column: "ClassesclassID",
                principalTable: "Classes",
                principalColumn: "classID");
        }
    }
}
