using System;
using Code_CloudSchool.Models;
using Microsoft.EntityFrameworkCore;

namespace Code_CloudSchool.Data;

// This class is used to connect to the database
public class AppDBContext : DbContext
{
    // Constructor - use all the base options for db context
    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
    {
    }

    // List the tables / relationships
    public DbSet<User> User { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<Lecturer> Lecturers { get; set; }
    public DbSet<Submission> Submissions { get; set; }
    public DbSet<Grade> Grades { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure TPT: Separate tables for User and Student
        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<Student>().ToTable("Students");

        // Unique constraint for StudentNumber
        modelBuilder.Entity<Student>().HasIndex(s => s.StudentNumber).IsUnique();

        // Configure the one-to-many relationship between Assignment and Submission.
        modelBuilder.Entity<Assignment>()
            .HasMany(a => a.Submissions) // An Assignment can have many Submissions.
            .WithOne(s => s.Assignment) // A Submission belongs to one Assignment.
            .HasForeignKey(s => s.Assignment_ID) // Foreign key in the Submission table.
            .OnDelete(DeleteBehavior.Cascade); // If an Assignment is deleted, its Submissions are also deleted.

        // Configure the one-to-one relationship between Submission and Grade.
        modelBuilder.Entity<Submission>()
            .HasOne(s => s.Grade) // A Submission can have one Grade.
            .WithOne(g => g.Submission) // A Grade belongs to one Submission.
            .HasForeignKey<Grade>(g => g.Submission_ID) // Foreign key in the Grade table.
            .OnDelete(DeleteBehavior.Cascade); // If a Submission is deleted, its Grade is also deleted.

        // Configure the relationship between Assignment and Lecturer
        modelBuilder.Entity<Assignment>()
            .HasOne(a => a.Lecturer) // An Assignment has one Lecturer.
            .WithMany() // A Lecturer can have many Assignments.
            .HasForeignKey(a => a.LecturerId) // Foreign key in the Assignment table.
            .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete (optional: change to Cascade if needed).

        // Configure the relationship between Submission and Student
        modelBuilder.Entity<Submission>()
            .HasOne(s => s.Student) // A Submission has one Student.
            .WithMany() // A Student can have many Submissions.
            .HasForeignKey(s => s.Student_ID) // Foreign key in the Submission table.
            .OnDelete(DeleteBehavior.Cascade); // If a Student is deleted, their Submissions are also deleted.
    }
}