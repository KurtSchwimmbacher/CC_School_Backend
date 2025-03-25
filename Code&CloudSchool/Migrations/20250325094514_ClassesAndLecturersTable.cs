using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Code_CloudSchool.Migrations
{
    /// <inheritdoc />
    public partial class ClassesAndLecturersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Students",
                newName: "StudentNumber");

            migrationBuilder.AddColumn<int>(
                name: "ClassesclassID",
                table: "Students",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClassesclassID",
                table: "Courses",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    classID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    className = table.Column<string>(type: "text", nullable: false),
                    classDescription = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.classID);
                });

            migrationBuilder.CreateTable(
                name: "Lecturers",
                columns: table => new
                {
                    lecturersId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClassesclassID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lecturers", x => x.lecturersId);
                    table.ForeignKey(
                        name: "FK_Lecturers_Classes_ClassesclassID",
                        column: x => x.ClassesclassID,
                        principalTable: "Classes",
                        principalColumn: "classID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_ClassesclassID",
                table: "Students",
                column: "ClassesclassID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ClassesclassID",
                table: "Courses",
                column: "ClassesclassID");

            migrationBuilder.CreateIndex(
                name: "IX_Lecturers_ClassesclassID",
                table: "Lecturers",
                column: "ClassesclassID");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Classes_ClassesclassID",
                table: "Courses",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Classes_ClassesclassID",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Classes_ClassesclassID",
                table: "Students");

            migrationBuilder.DropTable(
                name: "Lecturers");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Students_ClassesclassID",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Courses_ClassesclassID",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "ClassesclassID",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ClassesclassID",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "StudentNumber",
                table: "Students",
                newName: "Id");
        }
    }
}
