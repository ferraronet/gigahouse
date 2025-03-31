using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GigaHouse.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDates_UserProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FinishedAt",
                table: "UserProducts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartedAt",
                table: "UserProducts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinishedAt",
                table: "UserProducts");

            migrationBuilder.DropColumn(
                name: "StartedAt",
                table: "UserProducts");
        }
    }
}
