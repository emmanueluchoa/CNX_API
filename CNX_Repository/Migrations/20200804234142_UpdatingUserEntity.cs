using Microsoft.EntityFrameworkCore.Migrations;

namespace CNX_Repository.Migrations
{
    public partial class UpdatingUserEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalNote_Users_UserId",
                table: "PersonalNote");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonalNote",
                table: "PersonalNote");

            migrationBuilder.RenameTable(
                name: "PersonalNote",
                newName: "PersonalNotes");

            migrationBuilder.RenameIndex(
                name: "IX_PersonalNote_UserId",
                table: "PersonalNotes",
                newName: "IX_PersonalNotes_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UserEmail",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonalNotes",
                table: "PersonalNotes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserEmail",
                table: "Users",
                column: "UserEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalNotes_Users_UserId",
                table: "PersonalNotes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalNotes_Users_UserId",
                table: "PersonalNotes");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserEmail",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserName",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonalNotes",
                table: "PersonalNotes");

            migrationBuilder.RenameTable(
                name: "PersonalNotes",
                newName: "PersonalNote");

            migrationBuilder.RenameIndex(
                name: "IX_PersonalNotes_UserId",
                table: "PersonalNote",
                newName: "IX_PersonalNote_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "UserEmail",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonalNote",
                table: "PersonalNote",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalNote_Users_UserId",
                table: "PersonalNote",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
