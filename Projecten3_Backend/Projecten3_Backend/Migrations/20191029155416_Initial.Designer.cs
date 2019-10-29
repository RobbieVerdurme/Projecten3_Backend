﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Projecten3_Backend.Models;

namespace Projecten3_Backend.Migrations
{
    [DbContext(typeof(Projecten3_BackendContext))]
    [Migration("20191029155416_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Projecten3_Backend.Model.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int?>("TherapistTypeId");

                    b.Property<int?>("UserId");

                    b.HasKey("CategoryId");

                    b.HasIndex("TherapistTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("Projecten3_Backend.Model.ManyToMany.Challenge", b =>
                {
                    b.Property<int>("ChallengeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CategoryId");

                    b.Property<string>("Description");

                    b.Property<string>("Title");

                    b.Property<int?>("UserId");

                    b.HasKey("ChallengeId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Challenge");
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

            modelBuilder.Entity("Projecten3_Backend.Model.OpeningTime", b =>
                {
                    b.Property<int>("OpeningTimeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClosingHourAfternoon");

                    b.Property<string>("ClosingHourMorning");

                    b.Property<int>("Day");

                    b.Property<string>("OpeningHourAfternoon");

                    b.Property<string>("OpeningHourMorning");

                    b.Property<int?>("TherapistId");

                    b.HasKey("OpeningTimeId");

                    b.HasIndex("TherapistId");

                    b.ToTable("OpeningTime");
                });

            modelBuilder.Entity("Projecten3_Backend.Model.Therapist", b =>
                {
                    b.Property<int>("TherapistId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("HouseNumber");

                    b.Property<string>("LastName");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("PostalCode");

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

                    b.Property<string>("Email");

                    b.Property<string>("FamilyName");

                    b.Property<string>("FirstName");

                    b.Property<string>("Phone");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Projecten3_Backend.Model.Category", b =>
                {
                    b.HasOne("Projecten3_Backend.Model.TherapistType")
                        .WithMany("Categories")
                        .HasForeignKey("TherapistTypeId");

                    b.HasOne("Projecten3_Backend.Model.User")
                        .WithMany("Categories")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Projecten3_Backend.Model.ManyToMany.Challenge", b =>
                {
                    b.HasOne("Projecten3_Backend.Model.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("Projecten3_Backend.Model.User")
                        .WithMany("Challenges")
                        .HasForeignKey("UserId");
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

            modelBuilder.Entity("Projecten3_Backend.Model.OpeningTime", b =>
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
#pragma warning restore 612, 618
        }
    }
}
