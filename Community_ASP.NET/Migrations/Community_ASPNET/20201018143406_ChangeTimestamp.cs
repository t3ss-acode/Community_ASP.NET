using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Community_ASP.NET.Migrations.Community_ASPNET
{
    public partial class ChangeTimestamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "LoginLog");

            migrationBuilder.AddColumn<DateTime>(
                name: "Timestamp",
                table: "Messages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Timestamp",
                table: "LoginLog",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "LoginLog");

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Messages",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "LoginLog",
                type: "rowversion",
                rowVersion: true,
                nullable: true);
        }
    }
}
