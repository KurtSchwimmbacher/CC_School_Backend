using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Code_CloudSchool.Migrations
{
    /// <inheritdoc />
    public partial class CompletedEntityRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LecturerRegId",
                table: "Majors",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LecturerRegId",
                table: "Courses",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LecturerRegId",
                table: "Assignments",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_LecEmail",
                table: "User",
                column: "LecEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Majors_LecturerRegId",
                table: "Majors",
                column: "LecturerRegId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_LecturerRegId",
                table: "Courses",
                column: "LecturerRegId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_LecturerRegId",
                table: "Assignments",
                column: "LecturerRegId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_User_LecturerRegId",
                table: "Assignments",
                column: "LecturerRegId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_User_LecturerRegId",
                table: "Courses",
                column: "LecturerRegId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Majors_User_LecturerRegId",
                table: "Majors",
                column: "LecturerRegId",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_User_LecturerRegId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_User_LecturerRegId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Majors_User_LecturerRegId",
                table: "Majors");

            migrationBuilder.DropIndex(
                name: "IX_User_LecEmail",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Majors_LecturerRegId",
                table: "Majors");

            migrationBuilder.DropIndex(
                name: "IX_Courses_LecturerRegId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_LecturerRegId",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "LecturerRegId",
                table: "Majors");

            migrationBuilder.DropColumn(
                name: "LecturerRegId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "LecturerRegId",
                table: "Assignments");
        }
    }
}
