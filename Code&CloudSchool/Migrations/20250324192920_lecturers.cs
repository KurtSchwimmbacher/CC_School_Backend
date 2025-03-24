using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Code_CloudSchool.Migrations
{
    /// <inheritdoc />
    public partial class lecturers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lecturer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    LecEmail = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Department = table.Column<string>(type: "text", nullable: false),
                    DateOfJoining = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lecturer", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lecturer");
        }
    }
}
