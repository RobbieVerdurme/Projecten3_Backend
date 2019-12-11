using Microsoft.EntityFrameworkCore.Migrations;

namespace Projecten3_Backend.Migrations
{
    public partial class AddFeedbackAndStarsUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Feedback",
                table: "ChallengeUser",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "ChallengeUser",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Feedback",
                table: "ChallengeUser");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "ChallengeUser");
        }
    }
}
