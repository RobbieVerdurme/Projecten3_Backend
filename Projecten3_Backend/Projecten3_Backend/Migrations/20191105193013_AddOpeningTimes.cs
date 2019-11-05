using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Projecten3_Backend.Migrations
{
    public partial class AddOpeningTimes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OpeningTime");

            migrationBuilder.CreateTable(
                name: "OpeningTimes",
                columns: table => new
                {
                    OpeningTimesId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Interval = table.Column<string>(nullable: true),
                    TherapistId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpeningTimes", x => x.OpeningTimesId);
                    table.ForeignKey(
                        name: "FK_OpeningTimes_Therapist_TherapistId",
                        column: x => x.TherapistId,
                        principalTable: "Therapist",
                        principalColumn: "TherapistId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OpeningTimes_TherapistId",
                table: "OpeningTimes",
                column: "TherapistId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OpeningTimes");

            migrationBuilder.CreateTable(
                name: "OpeningTime",
                columns: table => new
                {
                    OpeningTimeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClosingHourAfternoon = table.Column<string>(nullable: true),
                    ClosingHourMorning = table.Column<string>(nullable: true),
                    Day = table.Column<int>(nullable: false),
                    OpeningHourAfternoon = table.Column<string>(nullable: true),
                    OpeningHourMorning = table.Column<string>(nullable: true),
                    TherapistId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpeningTime", x => x.OpeningTimeId);
                    table.ForeignKey(
                        name: "FK_OpeningTime_Therapist_TherapistId",
                        column: x => x.TherapistId,
                        principalTable: "Therapist",
                        principalColumn: "TherapistId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OpeningTime_TherapistId",
                table: "OpeningTime",
                column: "TherapistId");
        }
    }
}
