using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quizee.Api.Migrations
{
    /// <inheritdoc />
    public partial class HostIdinMode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
