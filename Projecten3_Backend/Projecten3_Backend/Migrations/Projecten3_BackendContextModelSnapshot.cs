﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Projecten3_Backend.Models;

namespace Projecten3_Backend.Migrations
{
    [DbContext(typeof(Projecten3_BackendContext))]
    partial class Projecten3_BackendContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Projecten3_Backend.Model.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int?>("TherapistTypeId");

                    b.HasKey("CategoryId");

                    b.HasIndex("TherapistTypeId");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("Projecten3_Backend.Model.Challenge", b =>
                {
                    b.Property<int>("ChallengeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CategoryId");

                    b.Property<string>("ChallengeImage");

                    b.Property<string>("Description");

                    b.Property<string>("Title");

                    b.HasKey("ChallengeId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Challenges");
                });

            modelBuilder.Entity("Projecten3_Backend.Model.Company", b =>
                {
                    b.Property<int>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City");

                    b.Property<DateTime>("Contract");

                    b.Property<string>("Country");

                    b.Property<int>("HouseNumber");

                    b.Property<string>("Mail");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.Property<int>("PostalCode");

                    b.Property<string>("Site");

                    b.Property<string>("Street");

                    b.HasKey("CompanyId");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("Projecten3_Backend.Model.LeaderboardScore", b =>
                {
                    b.Property<int>("LeaderboardScoreId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<int>("Score");

                    b.Property<int?>("UserId");

                    b.HasKey("LeaderboardScoreId");

                    b.HasIndex("UserId");

                    b.ToTable("LeaderboardScore");
                });

            modelBuilder.Entity("Projecten3_Backend.Model.ManyToMany.CategoryUser", b =>
                {
                    b.Property<int>("CategoryUserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId");

                    b.Property<int>("UserId");

                    b.HasKey("CategoryUserId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("CategoryUser");
                });

            modelBuilder.Entity("Projecten3_Backend.Model.ManyToMany.ChallengeUser", b =>
                {
                    b.Property<int>("ChallengeUserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ChallengeId");

                    b.Property<DateTime?>("CompletedDate");

                    b.Property<string>("Feedback");

                    b.Property<int>("Rating");

                    b.Property<int>("UserId");

                    b.HasKey("ChallengeUserId");

                    b.HasIndex("ChallengeId");

                    b.HasIndex("UserId");

                    b.ToTable("ChallengeUser");
                });

            modelBuilder.Entity("Projecten3_Backend.Model.ManyToMany.TherapistUser", b =>
                {
                    b.Property<int>("TherapistUserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("TherapistId");

                    b.Property<int>("UserId");

                    b.HasKey("TherapistUserId");

                    b.HasIndex("TherapistId");

                    b.HasIndex("UserId");

                    b.ToTable("TherapistUser");
                });

            modelBuilder.Entity("Projecten3_Backend.Model.OpeningTimes", b =>
                {
                    b.Property<int>("OpeningTimesId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Interval");

                    b.Property<int?>("TherapistId");

                    b.HasKey("OpeningTimesId");

                    b.HasIndex("TherapistId");

                    b.ToTable("OpeningTimes");
                });

            modelBuilder.Entity("Projecten3_Backend.Model.Therapist", b =>
                {
                    b.Property<int>("TherapistId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<int>("HouseNumber");

                    b.Property<string>("LastName");

                    b.Property<string>("PhoneNumber");

                    b.Property<int>("PostalCode");

                    b.Property<string>("Street");

                    b.Property<int?>("TherapistTypeId");

                    b.Property<string>("Website");

                    b.HasKey("TherapistId");

                    b.HasIndex("TherapistTypeId");

                    b.ToTable("Therapist");
                });

            modelBuilder.Entity("Projecten3_Backend.Model.TherapistType", b =>
                {
                    b.Property<int>("TherapistTypeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Type");

                    b.HasKey("TherapistTypeId");

                    b.ToTable("TherapistType");
                });

            modelBuilder.Entity("Projecten3_Backend.Model.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CompanyId");

                    b.Property<DateTime>("Contract");

                    b.Property<string>("Email");

                    b.Property<int>("ExperiencePoints");

                    b.Property<string>("FamilyName");

                    b.Property<string>("FirstName");

                    b.Property<string>("Phone");

                    b.Property<int?>("TherapistId");

                    b.HasKey("UserId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("TherapistId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Projecten3_Backend.Model.Category", b =>
                {
                    b.HasOne("Projecten3_Backend.Model.TherapistType")
                        .WithMany("Categories")
                        .HasForeignKey("TherapistTypeId");
                });

            modelBuilder.Entity("Projecten3_Backend.Model.Challenge", b =>
                {
                    b.HasOne("Projecten3_Backend.Model.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("Projecten3_Backend.Model.LeaderboardScore", b =>
                {
                    b.HasOne("Projecten3_Backend.Model.User")
                        .WithMany("LeaderboardScores")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Projecten3_Backend.Model.ManyToMany.CategoryUser", b =>
                {
                    b.HasOne("Projecten3_Backend.Model.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Projecten3_Backend.Model.User", "User")
                        .WithMany("Categories")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Projecten3_Backend.Model.ManyToMany.ChallengeUser", b =>
                {
                    b.HasOne("Projecten3_Backend.Model.Challenge", "Challenge")
                        .WithMany()
                        .HasForeignKey("ChallengeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Projecten3_Backend.Model.User", "User")
                        .WithMany("Challenges")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Projecten3_Backend.Model.ManyToMany.TherapistUser", b =>
                {
                    b.HasOne("Projecten3_Backend.Model.Therapist", "Therapist")
                        .WithMany("Clients")
                        .HasForeignKey("TherapistId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Projecten3_Backend.Model.User", "User")
                        .WithMany("Therapists")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Projecten3_Backend.Model.OpeningTimes", b =>
                {
                    b.HasOne("Projecten3_Backend.Model.Therapist")
                        .WithMany("OpeningTimes")
                        .HasForeignKey("TherapistId");
                });

            modelBuilder.Entity("Projecten3_Backend.Model.Therapist", b =>
                {
                    b.HasOne("Projecten3_Backend.Model.TherapistType", "TherapistType")
                        .WithMany()
                        .HasForeignKey("TherapistTypeId");
                });

            modelBuilder.Entity("Projecten3_Backend.Model.User", b =>
                {
                    b.HasOne("Projecten3_Backend.Model.Company", "Company")
                        .WithMany("CompanyMembers")
                        .HasForeignKey("CompanyId");

                    b.HasOne("Projecten3_Backend.Model.Therapist")
                        .WithMany("ClientList")
                        .HasForeignKey("TherapistId");
                });
#pragma warning restore 612, 618
        }
    }
}
