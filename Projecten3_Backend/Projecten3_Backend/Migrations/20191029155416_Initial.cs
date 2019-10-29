using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Projecten3_Backend.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TherapistType",
                columns: table => new
                {
                    TherapistTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TherapistType", x => x.TherapistTypeId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    FamilyName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Therapist",
                columns: table => new
                {
                    TherapistId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Website = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    HouseNumber = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    TherapistTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Therapist", x => x.TherapistId);
                    table.ForeignKey(
                        name: "FK_Therapist_TherapistType_TherapistTypeId",
                        column: x => x.TherapistTypeId,
                        principalTable: "TherapistType",
                        principalColumn: "TherapistTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    TherapistTypeId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK_Category_TherapistType_TherapistTypeId",
                        column: x => x.TherapistTypeId,
                        principalTable: "TherapistType",
                        principalColumn: "TherapistTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Category_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OpeningTime",
                columns: table => new
                {
                    OpeningTimeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Day = table.Column<int>(nullable: false),
                    OpeningHourMorning = table.Column<string>(nullable: true),
                    ClosingHourMorning = table.Column<string>(nullable: true),
                    OpeningHourAfternoon = table.Column<string>(nullable: true),
                    ClosingHourAfternoon = table.Column<string>(nullable: true),
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

            migrationBuilder.CreateTable(
                name: "TherapistUser",
                columns: table => new
                {
                    TherapistUserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TherapistId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TherapistUser", x => x.TherapistUserId);
                    table.ForeignKey(
                        name: "FK_TherapistUser_Therapist_TherapistId",
                        column: x => x.TherapistId,
                        principalTable: "Therapist",
                        principalColumn: "TherapistId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TherapistUser_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Challenge",
                columns: table => new
                {
                    ChallengeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Challenge", x => x.ChallengeId);
                    table.ForeignKey(
                        name: "FK_Challenge_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Challenge_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_TherapistTypeId",
                table: "Category",
                column: "TherapistTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_UserId",
                table: "Category",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Challenge_CategoryId",
                table: "Challenge",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Challenge_UserId",
                table: "Challenge",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OpeningTime_TherapistId",
                table: "OpeningTime",
                column: "TherapistId");

            migrationBuilder.CreateIndex(
                name: "IX_Therapist_TherapistTypeId",
                table: "Therapist",
                column: "TherapistTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TherapistUser_TherapistId",
                table: "TherapistUser",
                column: "TherapistId");

            migrationBuilder.CreateIndex(
                name: "IX_TherapistUser_UserId",
                table: "TherapistUser",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Challenge");

            migrationBuilder.DropTable(
                name: "OpeningTime");

            migrationBuilder.DropTable(
                name: "TherapistUser");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Therapist");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "TherapistType");
        }
    }
}
