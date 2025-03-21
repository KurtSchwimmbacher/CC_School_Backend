using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Code_CloudSchool.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserStudentModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdNo",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "firstName",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "yearLevel",
                table: "Students",
                newName: "YearLevel");

            migrationBuilder.RenameColumn(
                name: "phoneNumber",
                table: "Students",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "lastName",
                table: "Students",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "enrollmentDate",
                table: "Students",
                newName: "EnrollmentDate");

            migrationBuilder.RenameColumn(
                name: "studentStatus",
                table: "Students",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "studentGender",
                table: "Students",
                newName: "StudentNumber");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Students",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Students",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Students",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Students",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Students",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "YearLevel",
                table: "Students",
                newName: "yearLevel");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Students",
                newName: "phoneNumber");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Students",
                newName: "lastName");

            migrationBuilder.RenameColumn(
                name: "EnrollmentDate",
                table: "Students",
                newName: "enrollmentDate");

            migrationBuilder.RenameColumn(
                name: "StudentNumber",
                table: "Students",
                newName: "studentGender");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Students",
                newName: "studentStatus");

            migrationBuilder.AlterColumn<string>(
                name: "lastName",
                table: "Students",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "IdNo",
                table: "Students",
                type: "character varying(12)",
                maxLength: 12,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "firstName",
                table: "Students",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
