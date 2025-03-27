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
    [DbContext(typeof(AppDBContext))]
    partial class AppDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ClassesLecturers", b =>
                {
                    b.Property<int>("ClassesclassID")
                        .HasColumnType("integer");

                    b.Property<int>("lecturersId")
                        .HasColumnType("integer");

                    b.HasKey("ClassesclassID", "lecturersId");

                    b.HasIndex("lecturersId");

                    b.ToTable("ClassLecturers", (string)null);
                });

            modelBuilder.Entity("ClassesStudents", b =>
                {
                    b.Property<int>("ClassesclassID")
                        .HasColumnType("integer");

                    b.Property<int>("StudentsStudentNumber")
                        .HasColumnType("integer");

                    b.HasKey("ClassesclassID", "StudentsStudentNumber");

                    b.HasIndex("StudentsStudentNumber");

                    b.ToTable("ClassStudents", (string)null);
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

            modelBuilder.Entity("Code_CloudSchool.Models.Lecturers", b =>
                {
                    b.Property<int>("lecturersId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("lecturersId"));

                    b.HasKey("lecturersId");

                    b.ToTable("Lecturers");
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

            modelBuilder.Entity("Code_CloudSchool.Models.Students", b =>
                {
                    b.Property<int>("StudentNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("StudentNumber"));

                    b.Property<int?>("MajorsId")
                        .HasColumnType("integer");

                    b.HasKey("StudentNumber");

                    b.HasIndex("MajorsId");

                    b.ToTable("Students");
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

            modelBuilder.Entity("CoursesStudents", b =>
                {
                    b.Property<int>("CoursesId")
                        .HasColumnType("integer");

                    b.Property<int>("StudentsStudentNumber")
                        .HasColumnType("integer");

                    b.HasKey("CoursesId", "StudentsStudentNumber");

                    b.HasIndex("StudentsStudentNumber");

                    b.ToTable("CourseStudents", (string)null);
                });

            modelBuilder.Entity("ClassesLecturers", b =>
                {
                    b.HasOne("Code_CloudSchool.Models.Classes", null)
                        .WithMany()
                        .HasForeignKey("ClassesclassID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Code_CloudSchool.Models.Lecturers", null)
                        .WithMany()
                        .HasForeignKey("lecturersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ClassesStudents", b =>
                {
                    b.HasOne("Code_CloudSchool.Models.Classes", null)
                        .WithMany()
                        .HasForeignKey("ClassesclassID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Code_CloudSchool.Models.Students", null)
                        .WithMany()
                        .HasForeignKey("StudentsStudentNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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

            modelBuilder.Entity("Code_CloudSchool.Models.Students", b =>
                {
                    b.HasOne("Code_CloudSchool.Models.Majors", null)
                        .WithMany("Students")
                        .HasForeignKey("MajorsId");
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

            modelBuilder.Entity("CoursesStudents", b =>
                {
                    b.HasOne("Code_CloudSchool.Models.Courses", null)
                        .WithMany()
                        .HasForeignKey("CoursesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Code_CloudSchool.Models.Students", null)
                        .WithMany()
                        .HasForeignKey("StudentsStudentNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Code_CloudSchool.Models.Courses", b =>
                {
                    b.Navigation("Classes");
                });

            modelBuilder.Entity("Code_CloudSchool.Models.Majors", b =>
                {
                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}
