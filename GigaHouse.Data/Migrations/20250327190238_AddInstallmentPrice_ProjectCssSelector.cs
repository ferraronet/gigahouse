using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GigaHouse.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddInstallmentPrice_ProjectCssSelector : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InstallmentPrice",
                table: "ProjectCssSelectors",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstallmentPrice",
                table: "ProjectCssSelectors");
        }
    }
}
