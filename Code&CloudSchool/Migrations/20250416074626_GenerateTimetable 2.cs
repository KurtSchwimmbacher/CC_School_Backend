using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Code_CloudSchool.Migrations
{
    /// <inheritdoc />
    public partial class GenerateTimetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "classEndTime",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "classTime",
                table: "Classes");

            migrationBuilder.AddColumn<int>(
                name: "TimeSlotId",
                table: "Classes",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TimeSlots",
                columns: table => new
                {
                    TimeSlotId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Day = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "interval", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSlots", x => x.TimeSlotId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classes_TimeSlotId",
                table: "Classes",
                column: "TimeSlotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_TimeSlots_TimeSlotId",
                table: "Classes",
                column: "TimeSlotId",
                principalTable: "TimeSlots",
                principalColumn: "TimeSlotId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_TimeSlots_TimeSlotId",
                table: "Classes");

            migrationBuilder.DropTable(
                name: "TimeSlots");

            migrationBuilder.DropIndex(
                name: "IX_Classes_TimeSlotId",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "TimeSlotId",
                table: "Classes");

            migrationBuilder.AddColumn<DateTime>(
                name: "classEndTime",
                table: "Classes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "classTime",
                table: "Classes",
                type: "timestamp with time zone",
                nullable: true);
        }
    }
}
