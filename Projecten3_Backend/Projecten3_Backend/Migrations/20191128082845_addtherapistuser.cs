using Microsoft.EntityFrameworkCore.Migrations;

namespace Projecten3_Backend.Migrations
{
    public partial class addtherapistuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChallengeImage",
                table: "Challenges",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChallengeImage",
                table: "Challenges");
        }
    }
}
