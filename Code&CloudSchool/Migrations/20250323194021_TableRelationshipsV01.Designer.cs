﻿// <auto-generated />
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
    [Migration("20250323194021_TableRelationshipsV01")]
    partial class TableRelationshipsV01
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

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
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("CoursesId")
                        .HasColumnType("integer");

                    b.Property<int?>("MajorsId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CoursesId");

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

            modelBuilder.Entity("Code_CloudSchool.Models.Students", b =>
                {
                    b.HasOne("Code_CloudSchool.Models.Courses", null)
                        .WithMany("Students")
                        .HasForeignKey("CoursesId");

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

            modelBuilder.Entity("Code_CloudSchool.Models.Courses", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("Code_CloudSchool.Models.Majors", b =>
                {
                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}
