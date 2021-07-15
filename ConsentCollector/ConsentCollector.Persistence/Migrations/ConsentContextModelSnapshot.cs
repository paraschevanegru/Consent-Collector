﻿// <auto-generated />
using System;
using ConsentCollector.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ConsentCollector.Persistence.Migrations
{
    [DbContext(typeof(ConsentContext))]
    partial class ConsentContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ConsentCollector.Entities.Consent.Answer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<bool>("Agree")
                        .HasColumnType("bit")
                        .HasColumnName("Agree");

                    b.Property<DateTime>("AnswerDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("AnswerDate");

                    b.Property<Guid>("IdQuestion")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdSurvey")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdUser")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("IdQuestion");

                    b.HasIndex("IdSurvey");

                    b.HasIndex("IdUser");

                    b.ToTable("Answer");
                });

            modelBuilder.Entity("ConsentCollector.Entities.Consent.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<Guid>("IdSurvey")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Text");

                    b.HasKey("Id");

                    b.HasIndex("IdSurvey");

                    b.HasIndex("IdUser");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("ConsentCollector.Entities.Consent.Notification", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Description");

                    b.Property<Guid>("IdSurvey")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Title");

                    b.HasKey("Id");

                    b.HasIndex("IdSurvey");

                    b.HasIndex("IdUser");

                    b.ToTable("Notification");
                });

            modelBuilder.Entity("ConsentCollector.Entities.Consent.Question", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<bool>("Optional")
                        .HasColumnType("bit")
                        .HasColumnName("Optional");

                    b.Property<string>("Questions")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Questions");

                    b.HasKey("Id");

                    b.ToTable("Question");
                });

            modelBuilder.Entity("ConsentCollector.Entities.Consent.Survey", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Description");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("ExpirationDate");

                    b.Property<DateTime>("LaunchDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("LaunchDate");

                    b.Property<string>("LegalBasis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("LegalBasis");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Subject");

                    b.HasKey("Id");

                    b.ToTable("Survey");
                });

            modelBuilder.Entity("ConsentCollector.Entities.Consent.SurveyQuestion", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<Guid>("IdQuestion")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("IdQuestion");

                    b.Property<Guid>("IdSurvey")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("IdSurvey");

                    b.HasKey("Id");

                    b.HasIndex("IdQuestion");

                    b.HasIndex("IdSurvey");

                    b.ToTable("SurveyQuestion");
                });

            modelBuilder.Entity("ConsentCollector.Entities.Consent.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("Password");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Role");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("Username");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ConsentCollector.Entities.Consent.UserDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Email");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("Firstname");

                    b.Property<Guid>("IdUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("Lastname");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("Number");

                    b.HasKey("Id");

                    b.HasIndex("IdUser")
                        .IsUnique();

                    b.ToTable("UserDetail");
                });

            modelBuilder.Entity("ConsentCollector.Entities.Consent.Answer", b =>
                {
                    b.HasOne("ConsentCollector.Entities.Consent.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("IdQuestion")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConsentCollector.Entities.Consent.Survey", "Survey")
                        .WithMany("Answers")
                        .HasForeignKey("IdSurvey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConsentCollector.Entities.Consent.User", "User")
                        .WithMany("Answers")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");

                    b.Navigation("Survey");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ConsentCollector.Entities.Consent.Comment", b =>
                {
                    b.HasOne("ConsentCollector.Entities.Consent.Survey", "Survey")
                        .WithMany("Comments")
                        .HasForeignKey("IdSurvey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConsentCollector.Entities.Consent.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Survey");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ConsentCollector.Entities.Consent.Notification", b =>
                {
                    b.HasOne("ConsentCollector.Entities.Consent.Survey", "Survey")
                        .WithMany("Notifications")
                        .HasForeignKey("IdSurvey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConsentCollector.Entities.Consent.User", "User")
                        .WithMany("Notifications")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Survey");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ConsentCollector.Entities.Consent.SurveyQuestion", b =>
                {
                    b.HasOne("ConsentCollector.Entities.Consent.Question", "Question")
                        .WithMany("SurveyQuestion")
                        .HasForeignKey("IdQuestion")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConsentCollector.Entities.Consent.Survey", "Survey")
                        .WithMany("SurveyQuestion")
                        .HasForeignKey("IdSurvey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");

                    b.Navigation("Survey");
                });

            modelBuilder.Entity("ConsentCollector.Entities.Consent.UserDetail", b =>
                {
                    b.HasOne("ConsentCollector.Entities.Consent.User", "User")
                        .WithOne("Detail")
                        .HasForeignKey("ConsentCollector.Entities.Consent.UserDetail", "IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ConsentCollector.Entities.Consent.Question", b =>
                {
                    b.Navigation("Answers");

                    b.Navigation("SurveyQuestion");
                });

            modelBuilder.Entity("ConsentCollector.Entities.Consent.Survey", b =>
                {
                    b.Navigation("Answers");

                    b.Navigation("Comments");

                    b.Navigation("Notifications");

                    b.Navigation("SurveyQuestion");
                });

            modelBuilder.Entity("ConsentCollector.Entities.Consent.User", b =>
                {
                    b.Navigation("Answers");

                    b.Navigation("Comments");

                    b.Navigation("Detail");

                    b.Navigation("Notifications");
                });
#pragma warning restore 612, 618
        }
    }
}
