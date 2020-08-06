using Microsoft.EntityFrameworkCore.Migrations;

namespace CNX_Repository.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: false),
                    UserEmail = table.Column<string>(nullable: false),
                    UserPassword = table.Column<string>(nullable: false),
                    UserHomeTown = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonalNote",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    Note = table.Column<string>(maxLength: 140, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalNote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalNote_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonalNote_UserId",
                table: "PersonalNote",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonalNote");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
