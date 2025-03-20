using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PLCCommunication_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateS7ReadEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IP",
                table: "S7ReadTask",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Port",
                table: "S7ReadTask",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IP",
                table: "S7ReadTask");

            migrationBuilder.DropColumn(
                name: "Port",
                table: "S7ReadTask");
        }
    }
}
