using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PLCCommunication_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addModbusTCPBackgroundServiceEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ModbusTCPBackgroundService",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProxyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Port = table.Column<int>(type: "int", nullable: false),
                    SlaveID = table.Column<byte>(type: "tinyint", nullable: false),
                    FunctionCode = table.Column<byte>(type: "tinyint", nullable: false),
                    StartAddress = table.Column<int>(type: "int", nullable: false),
                    Num = table.Column<int>(type: "int", nullable: false),
                    IsOpen = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Createtime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModbusTCPBackgroundService", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModbusTCPBackgroundService");
        }
    }
}
