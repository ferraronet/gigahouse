using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GigaHouse.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTimesPerDay_Task : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TimesPerDay",
                table: "Tasks",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimesPerDay",
                table: "Tasks");
        }
    }
}
