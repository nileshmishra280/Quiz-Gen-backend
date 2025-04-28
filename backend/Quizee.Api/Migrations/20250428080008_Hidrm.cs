using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quizee.Api.Migrations
{
    /// <inheritdoc />
    public partial class Hidrm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaderboardData_Users_HostId",
                table: "LeaderboardData");

            migrationBuilder.DropIndex(
                name: "IX_LeaderboardData_HostId",
                table: "LeaderboardData");

            migrationBuilder.DropColumn(
                name: "HostId",
                table: "LeaderboardData");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HostId",
                table: "LeaderboardData",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LeaderboardData_HostId",
                table: "LeaderboardData",
                column: "HostId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaderboardData_Users_HostId",
                table: "LeaderboardData",
                column: "HostId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
