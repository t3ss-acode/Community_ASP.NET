using Microsoft.EntityFrameworkCore.Migrations;

namespace Community_ASP.NET.Migrations
{
    public partial class usergroupUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Group_User_UserId",
                schema: "Community",
                table: "Group");

            migrationBuilder.DropIndex(
                name: "IX_Group_UserId",
                schema: "Community",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "Community",
                table: "Group");

            migrationBuilder.CreateTable(
                name: "UserGroups",
                schema: "Community",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    GroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroups", x => new { x.UserId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_UserGroups_Group_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "Community",
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGroups_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Community",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_GroupId",
                schema: "Community",
                table: "UserGroups",
                column: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserGroups",
                schema: "Community");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                schema: "Community",
                table: "Group",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Group_UserId",
                schema: "Community",
                table: "Group",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Group_User_UserId",
                schema: "Community",
                table: "Group",
                column: "UserId",
                principalSchema: "Community",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
