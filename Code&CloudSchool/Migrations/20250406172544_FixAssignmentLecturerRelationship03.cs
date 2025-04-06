using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Code_CloudSchool.Migrations
{
    /// <inheritdoc />
    public partial class FixAssignmentLecturerRelationship03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_User_LecturerId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Assignments_Assignment_ID",
                table: "Submissions");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Assignments_LecturerId",
                table: "Assignments");

            migrationBuilder.RenameColumn(
                name: "LecturerId",
                table: "Assignments",
                newName: "LecturerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_LecturerUserId",
                table: "Assignments",
                column: "LecturerUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_User_LecturerUserId",
                table: "Assignments",
                column: "LecturerUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Assignments_Assignment_ID",
                table: "Submissions",
                column: "Assignment_ID",
                principalTable: "Assignments",
                principalColumn: "Assignment_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_User_LecturerUserId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Assignments_Assignment_ID",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_LecturerUserId",
                table: "Assignments");

            migrationBuilder.RenameColumn(
                name: "LecturerUserId",
                table: "Assignments",
                newName: "LecturerId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Assignments_LecturerId",
                table: "Assignments",
                column: "LecturerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_User_LecturerId",
                table: "Assignments",
                column: "LecturerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Assignments_Assignment_ID",
                table: "Submissions",
                column: "Assignment_ID",
                principalTable: "Assignments",
                principalColumn: "LecturerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
