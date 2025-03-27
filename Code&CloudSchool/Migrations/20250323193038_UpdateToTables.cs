using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Code_CloudSchool.Migrations
{
    /// <inheritdoc />
    public partial class UpdateToTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Majors_MajorsId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_MajorsId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "MajorsId",
                table: "Courses");

            migrationBuilder.AddColumn<int>(
                name: "CoursesId",
                table: "Students",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CoursesMajors",
                columns: table => new
                {
                    CoursesId = table.Column<int>(type: "integer", nullable: false),
                    MajorsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursesMajors", x => new { x.CoursesId, x.MajorsId });
                    table.ForeignKey(
                        name: "FK_CoursesMajors_Courses_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoursesMajors_Majors_MajorsId",
                        column: x => x.MajorsId,
                        principalTable: "Majors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_CoursesId",
                table: "Students",
                column: "CoursesId");

            migrationBuilder.CreateIndex(
                name: "IX_CoursesMajors_MajorsId",
                table: "CoursesMajors",
                column: "MajorsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Courses_CoursesId",
                table: "Students",
                column: "CoursesId",
                principalTable: "Courses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Courses_CoursesId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "CoursesMajors");

            migrationBuilder.DropIndex(
                name: "IX_Students_CoursesId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CoursesId",
                table: "Students");

            migrationBuilder.AddColumn<int>(
                name: "MajorsId",
                table: "Courses",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_MajorsId",
                table: "Courses",
                column: "MajorsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Majors_MajorsId",
                table: "Courses",
                column: "MajorsId",
                principalTable: "Majors",
                principalColumn: "Id");
        }
    }
}
