using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PLCCommunication_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModbuConfigId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_T_ModbusTCPConfig",
                table: "T_ModbusTCPConfig");

            migrationBuilder.DropColumn(
                name: "ConfigId",
                table: "T_ModbusTCPConfig");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "T_ModbusTCPConfig",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_T_ModbusTCPConfig",
                table: "T_ModbusTCPConfig",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_T_ModbusTCPConfig",
                table: "T_ModbusTCPConfig");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "T_ModbusTCPConfig");

            migrationBuilder.AddColumn<Guid>(
                name: "ConfigId",
                table: "T_ModbusTCPConfig",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_T_ModbusTCPConfig",
                table: "T_ModbusTCPConfig",
                column: "ConfigId");
        }
    }
}
