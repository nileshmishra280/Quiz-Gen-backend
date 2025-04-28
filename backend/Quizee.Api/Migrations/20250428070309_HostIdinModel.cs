using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quizee.Api.Migrations
{
    /// <inheritdoc />
    public partial class HostIdinModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HostId",
                table: "LeaderboardData",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HostId",
                table: "LeaderboardData");
        }
    }
}
