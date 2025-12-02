using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymCRM.Migrations
{
    /// <inheritdoc />
    public partial class addAdmin02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Attended",
                table: "SessionBookings",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AttendedAtUtc",
                table: "SessionBookings",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attended",
                table: "SessionBookings");

            migrationBuilder.DropColumn(
                name: "AttendedAtUtc",
                table: "SessionBookings");
        }
    }
}
