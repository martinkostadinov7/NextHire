using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class PropsCv : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Experience",
                table: "Cvs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Skills",
                table: "Cvs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "Cvs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Experience",
                table: "Cvs");

            migrationBuilder.DropColumn(
                name: "Skills",
                table: "Cvs");

            migrationBuilder.DropColumn(
                name: "Summary",
                table: "Cvs");
        }
    }
}
