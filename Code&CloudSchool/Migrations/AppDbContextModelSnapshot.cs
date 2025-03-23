﻿// <auto-generated />
using System;
using Code_CloudSchool.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Code_CloudSchool.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Code_CloudSchool.Models.Assignment", b =>
                {
                    b.Property<int>("Assignment_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Assignment_ID"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("LecturerId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Assignment_ID");

                    b.HasIndex("LecturerId");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("Code_CloudSchool.Models.Grade", b =>
                {
                    b.Property<int>("Grade_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Grade_ID"));

                    b.Property<string>("Feedback")
                        .HasColumnType("text");

                    b.Property<decimal>("Score")
                        .HasColumnType("numeric");

                    b.Property<int>("Submission_ID")
                        .HasColumnType("integer");

                    b.HasKey("Grade_ID");

                    b.HasIndex("Submission_ID")
                        .IsUnique();

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("Code_CloudSchool.Models.Lecturer", b =>
                {
                    b.Property<int>("LecturerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("LecturerId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LecturerId");

                    b.ToTable("Lecturers");
                });

            modelBuilder.Entity("Code_CloudSchool.Models.Submission", b =>
                {
                    b.Property<int>("Submission_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Submission_ID"));

                    b.Property<int>("Assignment_ID")
                        .HasColumnType("integer");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Student_ID")
                        .HasColumnType("integer");

                    b.Property<DateTime>("SubmissionDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Submission_ID");

                    b.HasIndex("Assignment_ID");

                    b.HasIndex("Student_ID");

                    b.ToTable("Submissions");
                });

            modelBuilder.Entity("Code_CloudSchool.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Code_CloudSchool.Models.Student", b =>
                {
                    b.HasBaseType("Code_CloudSchool.Models.User");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("EnrollmentDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("StudentNumber")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("character varying(12)");

                    b.Property<string>("YearLevel")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasIndex("StudentNumber")
                        .IsUnique();

                    b.ToTable("Students", (string)null);
                });

            modelBuilder.Entity("Code_CloudSchool.Models.Assignment", b =>
                {
                    b.HasOne("Code_CloudSchool.Models.Lecturer", "Lecturer")
                        .WithMany()
                        .HasForeignKey("LecturerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Lecturer");
                });

            modelBuilder.Entity("Code_CloudSchool.Models.Grade", b =>
                {
                    b.HasOne("Code_CloudSchool.Models.Submission", "Submission")
                        .WithOne("Grade")
                        .HasForeignKey("Code_CloudSchool.Models.Grade", "Submission_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Submission");
                });

            modelBuilder.Entity("Code_CloudSchool.Models.Submission", b =>
                {
                    b.HasOne("Code_CloudSchool.Models.Assignment", "Assignment")
                        .WithMany("Submissions")
                        .HasForeignKey("Assignment_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Code_CloudSchool.Models.Student", "Student")
                        .WithMany()
                        .HasForeignKey("Student_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assignment");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Code_CloudSchool.Models.Student", b =>
                {
                    b.HasOne("Code_CloudSchool.Models.User", null)
                        .WithOne()
                        .HasForeignKey("Code_CloudSchool.Models.Student", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Code_CloudSchool.Models.Assignment", b =>
                {
                    b.Navigation("Submissions");
                });

            modelBuilder.Entity("Code_CloudSchool.Models.Submission", b =>
                {
                    b.Navigation("Grade")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
