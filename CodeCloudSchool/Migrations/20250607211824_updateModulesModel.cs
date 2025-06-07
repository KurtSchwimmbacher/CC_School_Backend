using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Code_CloudSchool.Migrations
{
    /// <inheritdoc />
    public partial class updateModulesModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "published",
                table: "Modules",
                type: "boolean",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "published",
                table: "Modules");
        }
    }
}
