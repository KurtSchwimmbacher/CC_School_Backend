using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Code_CloudSchool.Migrations
{
    /// <inheritdoc />
    public partial class updatingNamecaseForTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassStudents_User_StudentsId",
                table: "ClassStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseStudents_User_StudentsId",
                table: "CourseStudents");

            migrationBuilder.RenameColumn(
                name: "StudentsId",
                table: "CourseStudents",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseStudents_StudentsId",
                table: "CourseStudents",
                newName: "IX_CourseStudents_StudentId");

            migrationBuilder.RenameColumn(
                name: "StudentsId",
                table: "ClassStudents",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_ClassStudents_StudentsId",
                table: "ClassStudents",
                newName: "IX_ClassStudents_StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassStudents_User_StudentId",
                table: "ClassStudents",
                column: "StudentId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseStudents_User_StudentId",
                table: "CourseStudents",
                column: "StudentId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassStudents_User_StudentId",
                table: "ClassStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseStudents_User_StudentId",
                table: "CourseStudents");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "CourseStudents",
                newName: "StudentsId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseStudents_StudentId",
                table: "CourseStudents",
                newName: "IX_CourseStudents_StudentsId");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "ClassStudents",
                newName: "StudentsId");

            migrationBuilder.RenameIndex(
                name: "IX_ClassStudents_StudentId",
                table: "ClassStudents",
                newName: "IX_ClassStudents_StudentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassStudents_User_StudentsId",
                table: "ClassStudents",
                column: "StudentsId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseStudents_User_StudentsId",
                table: "CourseStudents",
                column: "StudentsId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
