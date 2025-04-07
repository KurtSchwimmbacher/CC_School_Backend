using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Code_CloudSchool.Migrations
{
    /// <inheritdoc />
    public partial class FixAssignmentLecturerRelationshipV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_LecturerReg_LecturerRegId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_User_LecturerUser_Id",
                table: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_LecturerRegId",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "LecturerRegId",
                table: "Assignments");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_LecturerReg_LecturerUser_Id",
                table: "Assignments",
                column: "LecturerUser_Id",
                principalTable: "LecturerReg",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_LecturerReg_LecturerUser_Id",
                table: "Assignments");

            migrationBuilder.AddColumn<int>(
                name: "LecturerRegId",
                table: "Assignments",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_LecturerRegId",
                table: "Assignments",
                column: "LecturerRegId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_LecturerReg_LecturerRegId",
                table: "Assignments",
                column: "LecturerRegId",
                principalTable: "LecturerReg",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_User_LecturerUser_Id",
                table: "Assignments",
                column: "LecturerUser_Id",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
