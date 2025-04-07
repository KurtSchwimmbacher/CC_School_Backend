﻿// <auto-generated />
using System;
using Code_CloudSchool.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Code_CloudSchool.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20250407185531_FixAssignmentLecturerRelationshipV2")]
    partial class FixAssignmentLecturerRelationshipV2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ClassesLecturerReg", b =>
                {
                    b.Property<int>("ClassesclassID")
                        .HasColumnType("integer");

                    b.Property<int>("LecturersId")
                        .HasColumnType("integer");

                    b.HasKey("ClassesclassID", "LecturersId");

                    b.HasIndex("LecturersId");

                    b.ToTable("ClassLecturers", (string)null);
                });

            modelBuilder.Entity("ClassesStudent", b =>
                {
                    b.Property<int>("ClassesclassID")
                        .HasColumnType("integer");

                    b.Property<int>("StudentsId")
                        .HasColumnType("integer");

                    b.HasKey("ClassesclassID", "StudentsId");

                    b.HasIndex("StudentsId");

                    b.ToTable("ClassStudents", (string)null);
                });

            modelBuilder.Entity("Code_CloudSchool.Models.Announcements", b =>
                {
                    b.Property<int>("AnnouncementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AnnouncementId"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("LecturerId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("AnnouncementId");

                    b.ToTable("Announcements");
                });

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

                    b.Property<int>("LecturerUser_Id")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Assignment_ID");

                    b.HasIndex("LecturerUser_Id");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("Code_CloudSchool.Models.Classes", b =>
                {
                    b.Property<int>("classID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("classID"));

                    b.Property<int>("CourseId")
                        .HasColumnType("integer");

                    b.Property<string>("classDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("classEndTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("className")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("classTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("classID");

                    b.HasIndex("CourseId");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("Code_CloudSchool.Models.Courses", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("courseCode")
                        .HasColumnType("integer");

                    b.Property<string>("courseDescription")
                        .HasColumnType("text");

                    b.Property<string>("courseName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Courses");
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

            modelBuilder.Entity("Code_CloudSchool.Models.Majors", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CreditsRequired")
                        .HasColumnType("integer");

                    b.Property<string>("MajorCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MajorDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MajorName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Majors");
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

                    b.Property<string>("Discriminator")
                        .HasMaxLength(13)
                        .HasColumnType("character varying(13)");

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

                    b.ToTable("User");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("CoursesMajors", b =>
                {
                    b.Property<int>("CoursesId")
                        .HasColumnType("integer");

                    b.Property<int>("MajorsId")
                        .HasColumnType("integer");

                    b.HasKey("CoursesId", "MajorsId");

                    b.HasIndex("MajorsId");

                    b.ToTable("MajorCourses", (string)null);
                });

            modelBuilder.Entity("CoursesStudent", b =>
                {
                    b.Property<int>("CoursesId")
                        .HasColumnType("integer");

                    b.Property<int>("StudentsId")
                        .HasColumnType("integer");

                    b.HasKey("CoursesId", "StudentsId");

                    b.HasIndex("StudentsId");

                    b.ToTable("CourseStudents", (string)null);
                });

            modelBuilder.Entity("Code_CloudSchool.Models.LecturerReg", b =>
                {
                    b.HasBaseType("Code_CloudSchool.Models.User");

                    b.Property<DateTime>("DateOfJoining")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("LecEmail")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<string>("LecLastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("LectName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("LecturerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("LecturerId"));

                    b.ToTable("LecturerReg", (string)null);
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

                    b.Property<int?>("MajorsId")
                        .HasColumnType("integer");

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

                    b.HasIndex("MajorsId");

                    b.HasIndex("StudentNumber")
                        .IsUnique();

                    b.ToTable("Students");
                });

            modelBuilder.Entity("ClassesLecturerReg", b =>
                {
                    b.HasOne("Code_CloudSchool.Models.Classes", null)
                        .WithMany()
                        .HasForeignKey("ClassesclassID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Code_CloudSchool.Models.LecturerReg", null)
                        .WithMany()
                        .HasForeignKey("LecturersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ClassesStudent", b =>
                {
                    b.HasOne("Code_CloudSchool.Models.Classes", null)
                        .WithMany()
                        .HasForeignKey("ClassesclassID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Code_CloudSchool.Models.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Code_CloudSchool.Models.Assignment", b =>
                {
                    b.HasOne("Code_CloudSchool.Models.LecturerReg", "LecturerUser")
                        .WithMany("Assignments")
                        .HasForeignKey("LecturerUser_Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("LecturerUser");
                });

            modelBuilder.Entity("Code_CloudSchool.Models.Classes", b =>
                {
                    b.HasOne("Code_CloudSchool.Models.Courses", "Courses")
                        .WithMany("Classes")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Courses");
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

            modelBuilder.Entity("CoursesMajors", b =>
                {
                    b.HasOne("Code_CloudSchool.Models.Courses", null)
                        .WithMany()
                        .HasForeignKey("CoursesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Code_CloudSchool.Models.Majors", null)
                        .WithMany()
                        .HasForeignKey("MajorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CoursesStudent", b =>
                {
                    b.HasOne("Code_CloudSchool.Models.Courses", null)
                        .WithMany()
                        .HasForeignKey("CoursesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Code_CloudSchool.Models.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Code_CloudSchool.Models.LecturerReg", b =>
                {
                    b.HasOne("Code_CloudSchool.Models.User", null)
                        .WithOne()
                        .HasForeignKey("Code_CloudSchool.Models.LecturerReg", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Code_CloudSchool.Models.Student", b =>
                {
                    b.HasOne("Code_CloudSchool.Models.User", null)
                        .WithOne()
                        .HasForeignKey("Code_CloudSchool.Models.Student", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Code_CloudSchool.Models.Majors", null)
                        .WithMany("Students")
                        .HasForeignKey("MajorsId");
                });

            modelBuilder.Entity("Code_CloudSchool.Models.Assignment", b =>
                {
                    b.Navigation("Submissions");
                });

            modelBuilder.Entity("Code_CloudSchool.Models.Courses", b =>
                {
                    b.Navigation("Classes");
                });

            modelBuilder.Entity("Code_CloudSchool.Models.Majors", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("Code_CloudSchool.Models.Submission", b =>
                {
                    b.Navigation("Grade")
                        .IsRequired();
                });

            modelBuilder.Entity("Code_CloudSchool.Models.LecturerReg", b =>
                {
                    b.Navigation("Assignments");
                });
#pragma warning restore 612, 618
        }
    }
}
