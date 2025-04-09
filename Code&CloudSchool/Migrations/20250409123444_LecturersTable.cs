using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Code_CloudSchool.Migrations
{
    /// <inheritdoc />
    public partial class LecturersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_User_LecturerId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_User_LecturerRegId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassLecturers_User_LecturersId",
                table: "ClassLecturers");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassStudents_User_StudentsId",
                table: "ClassStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_User_LecturerRegId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseStudents_User_StudentsId",
                table: "CourseStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_Majors_User_LecturerRegId",
                table: "Majors");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_User_Student_ID",
                table: "Submissions");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Majors_MajorsId",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_LecEmail",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_MajorsId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_StudentNumber",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "User");

            migrationBuilder.DropColumn(
                name: "DateOfJoining",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Department",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "User");

            migrationBuilder.DropColumn(
                name: "EnrollmentDate",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "User");

            migrationBuilder.DropColumn(
                name: "LecEmail",
                table: "User");

            migrationBuilder.DropColumn(
                name: "LecLastName",
                table: "User");

            migrationBuilder.DropColumn(
                name: "LectName",
                table: "User");

            migrationBuilder.DropColumn(
                name: "LecturerId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "MajorsId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "User");

            migrationBuilder.DropColumn(
                name: "StudentNumber",
                table: "User");

            migrationBuilder.DropColumn(
                name: "YearLevel",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Lecturers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    LecturerId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LectName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LecLastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LecEmail = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Department = table.Column<string>(type: "text", nullable: false),
                    DateOfJoining = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lecturers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lecturers_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    StudentNumber = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: true),
                    EnrollmentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    YearLevel = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    MajorsId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Majors_MajorsId",
                        column: x => x.MajorsId,
                        principalTable: "Majors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Students_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lecturers_LecEmail",
                table: "Lecturers",
                column: "LecEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_MajorsId",
                table: "Students",
                column: "MajorsId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_StudentNumber",
                table: "Students",
                column: "StudentNumber",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Lecturers_LecturerId",
                table: "Assignments",
                column: "LecturerId",
                principalTable: "Lecturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Lecturers_LecturerRegId",
                table: "Assignments",
                column: "LecturerRegId",
                principalTable: "Lecturers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassLecturers_Lecturers_LecturersId",
                table: "ClassLecturers",
                column: "LecturersId",
                principalTable: "Lecturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassStudents_Students_StudentsId",
                table: "ClassStudents",
                column: "StudentsId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Lecturers_LecturerRegId",
                table: "Courses",
                column: "LecturerRegId",
                principalTable: "Lecturers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseStudents_Students_StudentsId",
                table: "CourseStudents",
                column: "StudentsId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Majors_Lecturers_LecturerRegId",
                table: "Majors",
                column: "LecturerRegId",
                principalTable: "Lecturers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Students_Student_ID",
                table: "Submissions",
                column: "Student_ID",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Lecturers_LecturerId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Lecturers_LecturerRegId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassLecturers_Lecturers_LecturersId",
                table: "ClassLecturers");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassStudents_Students_StudentsId",
                table: "ClassStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Lecturers_LecturerRegId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseStudents_Students_StudentsId",
                table: "CourseStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_Majors_Lecturers_LecturerRegId",
                table: "Majors");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Students_Student_ID",
                table: "Submissions");

            migrationBuilder.DropTable(
                name: "Lecturers");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "User",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfJoining",
                table: "User",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "User",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "User",
                type: "character varying(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "User",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EnrollmentDate",
                table: "User",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "User",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "User",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LecEmail",
                table: "User",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LecLastName",
                table: "User",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LectName",
                table: "User",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LecturerId",
                table: "User",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MajorsId",
                table: "User",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "User",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentNumber",
                table: "User",
                type: "character varying(12)",
                maxLength: 12,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YearLevel",
                table: "User",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_User_LecEmail",
                table: "User",
                column: "LecEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_MajorsId",
                table: "User",
                column: "MajorsId");

            migrationBuilder.CreateIndex(
                name: "IX_User_StudentNumber",
                table: "User",
                column: "StudentNumber",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_User_LecturerId",
                table: "Assignments",
                column: "LecturerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_User_LecturerRegId",
                table: "Assignments",
                column: "LecturerRegId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassLecturers_User_LecturersId",
                table: "ClassLecturers",
                column: "LecturersId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassStudents_User_StudentsId",
                table: "ClassStudents",
                column: "StudentsId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_User_LecturerRegId",
                table: "Courses",
                column: "LecturerRegId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseStudents_User_StudentsId",
                table: "CourseStudents",
                column: "StudentsId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Majors_User_LecturerRegId",
                table: "Majors",
                column: "LecturerRegId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_User_Student_ID",
                table: "Submissions",
                column: "Student_ID",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Majors_MajorsId",
                table: "User",
                column: "MajorsId",
                principalTable: "Majors",
                principalColumn: "Id");
        }
    }
}
