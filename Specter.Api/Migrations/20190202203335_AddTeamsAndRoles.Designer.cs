﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Specter.Api.Data;

namespace Specter.Api.Migrations
{
    [DbContext(typeof(SpecterDb))]
    [Migration("20190202203335_AddTeamsAndRoles")]
    partial class AddTeamsAndRoles
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.ToTable("IdentityUserClaims");
                });

            modelBuilder.Entity("Specter.Api.Data.Entities.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Email");

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail");

                    b.Property<string>("NormalizedUserName");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("Preferences")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue("{\"DarkMode\":false}");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Specter.Api.Data.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<bool>("Removed")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Specter.Api.Data.Entities.Delivery", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Order");

                    b.Property<Guid>("ProjectId");

                    b.Property<bool>("Removed")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Deliveries");
                });

            modelBuilder.Entity("Specter.Api.Data.Entities.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(12);

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<bool>("Removed")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("WorkItemIdPrefix");

                    b.HasKey("Id");

                    b.HasIndex("WorkItemIdPrefix")
                        .IsUnique()
                        .HasFilter("[WorkItemIdPrefix] IS NOT NULL");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Specter.Api.Data.Entities.ProjectTeam", b =>
                {
                    b.Property<Guid>("ProjectId");

                    b.Property<Guid>("TeamId");

                    b.HasKey("ProjectId", "TeamId");

                    b.HasIndex("TeamId");

                    b.ToTable("ProjectTeam");
                });

            modelBuilder.Entity("Specter.Api.Data.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("NormalizedName");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Specter.Api.Data.Entities.Team", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ApplicationUserId");

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<bool>("Removed")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Specter.Api.Data.Entities.Template", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<Guid>("CreatedBy");

                    b.Property<string>("Data")
                        .IsRequired();

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<Guid?>("ForkId")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(null);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<short>("Visibility");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("ForkId");

                    b.ToTable("Templates");
                });

            modelBuilder.Entity("Specter.Api.Data.Entities.TemplateHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Data")
                        .IsRequired();

                    b.Property<DateTime>("EditedAt");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Reason");

                    b.Property<Guid>("TemplateId");

                    b.HasKey("Id");

                    b.HasIndex("TemplateId");

                    b.ToTable("TemplatesHistory");
                });

            modelBuilder.Entity("Specter.Api.Data.Entities.Timesheet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CategoryId");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime>("Date");

                    b.Property<Guid?>("DeliveryId");

                    b.Property<string>("Description");

                    b.Property<int>("InternalId");

                    b.Property<bool>("Locked")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<Guid>("ProjectId");

                    b.Property<bool>("Removed")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<float>("Time");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("DeliveryId");

                    b.HasIndex("InternalId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("Timesheets");
                });

            modelBuilder.Entity("Specter.Api.Data.Entities.UserTeamRole", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("RoleId");

                    b.Property<Guid?>("TeamId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("TeamId");

                    b.ToTable("UserTeamRoles");
                });

            modelBuilder.Entity("Specter.Api.Data.Entities.Delivery", b =>
                {
                    b.HasOne("Specter.Api.Data.Entities.Project", "Project")
                        .WithMany("Deliveries")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Specter.Api.Data.Entities.ProjectTeam", b =>
                {
                    b.HasOne("Specter.Api.Data.Entities.Project", "Project")
                        .WithMany("Teams")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Specter.Api.Data.Entities.Team", "Team")
                        .WithMany("Projects")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Specter.Api.Data.Entities.Team", b =>
                {
                    b.HasOne("Specter.Api.Data.Entities.ApplicationUser")
                        .WithMany("Teams")
                        .HasForeignKey("ApplicationUserId");
                });

            modelBuilder.Entity("Specter.Api.Data.Entities.Template", b =>
                {
                    b.HasOne("Specter.Api.Data.Entities.ApplicationUser", "CreatedByUser")
                        .WithMany("Templates")
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Specter.Api.Data.Entities.Template", "ForkTemplate")
                        .WithMany("Forks")
                        .HasForeignKey("ForkId");
                });

            modelBuilder.Entity("Specter.Api.Data.Entities.TemplateHistory", b =>
                {
                    b.HasOne("Specter.Api.Data.Entities.Template", "Template")
                        .WithMany("Edits")
                        .HasForeignKey("TemplateId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Specter.Api.Data.Entities.Timesheet", b =>
                {
                    b.HasOne("Specter.Api.Data.Entities.Category", "Category")
                        .WithMany("Timesheets")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Specter.Api.Data.Entities.Delivery", "Delivery")
                        .WithMany("Timesheets")
                        .HasForeignKey("DeliveryId");

                    b.HasOne("Specter.Api.Data.Entities.Project", "Project")
                        .WithMany("Timesheets")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Specter.Api.Data.Entities.ApplicationUser", "User")
                        .WithMany("Timesheets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Specter.Api.Data.Entities.UserTeamRole", b =>
                {
                    b.HasOne("Specter.Api.Data.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Specter.Api.Data.Entities.Team", "Team")
                        .WithMany("Users")
                        .HasForeignKey("TeamId");

                    b.HasOne("Specter.Api.Data.Entities.ApplicationUser", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
