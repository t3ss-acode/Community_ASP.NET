using Microsoft.EntityFrameworkCore.Migrations;

namespace Community_ASP.NET.Migrations.Community_ASPNET
{
    public partial class fixedSpelling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoginLog_AspNetUsers_userId",
                table: "LoginLog");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "LoginLog",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_LoginLog_userId",
                table: "LoginLog",
                newName: "IX_LoginLog_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoginLog_AspNetUsers_UserId",
                table: "LoginLog",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoginLog_AspNetUsers_UserId",
                table: "LoginLog");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "LoginLog",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_LoginLog_UserId",
                table: "LoginLog",
                newName: "IX_LoginLog_userId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoginLog_AspNetUsers_userId",
                table: "LoginLog",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
