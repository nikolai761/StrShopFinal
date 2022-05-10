using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StrShop.Migrations
{
    public partial class qwr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Password", "salt" },
                values: new object[] { "9MHqWBpJr+Az9Zvo7evrQA==", new byte[] { 65, 22, 199, 179, 175, 244, 175, 53 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Password", "salt" },
                values: new object[] { "xZhDxctlQPPAYkcdYTAZOw==", new byte[] { 36, 116, 197, 22, 90, 80, 76, 68 } });
        }
    }
}
