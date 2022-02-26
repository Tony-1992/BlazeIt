using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazeIt.Server.Migrations
{
    public partial class InitDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TABLE_Feedbacks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Votes = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TABLE_Feedbacks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TABLE_Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TABLE_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FeedbackUser",
                columns: table => new
                {
                    VotedOnId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VotersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackUser", x => new { x.VotedOnId, x.VotersId });
                    table.ForeignKey(
                        name: "FK_FeedbackUser_TABLE_Feedbacks_VotedOnId",
                        column: x => x.VotedOnId,
                        principalTable: "TABLE_Feedbacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeedbackUser_TABLE_Users_VotersId",
                        column: x => x.VotersId,
                        principalTable: "TABLE_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TABLE_Todos",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Completed = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TABLE_Todos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TABLE_Todos_TABLE_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "TABLE_Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackUser_VotersId",
                table: "FeedbackUser",
                column: "VotersId");

            migrationBuilder.CreateIndex(
                name: "IX_TABLE_Todos_UserId",
                table: "TABLE_Todos",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeedbackUser");

            migrationBuilder.DropTable(
                name: "TABLE_Todos");

            migrationBuilder.DropTable(
                name: "TABLE_Feedbacks");

            migrationBuilder.DropTable(
                name: "TABLE_Users");
        }
    }
}
