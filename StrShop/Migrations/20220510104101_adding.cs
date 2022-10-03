﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StrShop.Migrations
{
    public partial class adding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Password", "salt" },
                values: new object[] { "XZqzux2qpgJMP4glwjCgGA==", new byte[] { 247, 154, 24, 4, 65, 97, 241, 175 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Password", "salt" },
                values: new object[] { "9MHqWBpJr+Az9Zvo7evrQA==", new byte[] { 65, 22, 199, 179, 175, 244, 175, 53 } });
        }
    }
}
