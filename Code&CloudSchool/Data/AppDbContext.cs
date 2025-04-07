using System;
using Code_CloudSchool.Models;
using Microsoft.EntityFrameworkCore;

namespace Code_CloudSchool.Data;

public class AppDBContext : DbContext
{
    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

    // Below are the tables that we are going to be working with
    public DbSet<User> User { get; set; }
    
    public DbSet<Majors> Majors { get; set; } // Adding majors to the database so that we can use it in the database and in our application
    public DbSet<Courses> Courses { get; set; }
    public DbSet<Classes> Classes { get; set; }
    public DbSet<LecturerReg> Lecturers { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<Submission> Submissions { get; set; }
    public DbSet<Grade> Grades { get; set; }

    // Define a DbSet for Announcements, representing a table in the database
    public DbSet<Announcements> Announcements { get; set; } = default!;

    // Add Relationships below 
    protected override void OnModelCreating(ModelBuilder modelBuilder) // This is a method that is called when the model is being created
    {
        base.OnModelCreating(modelBuilder);

        // Configure inheritance FIRST
        modelBuilder.Entity<LecturerReg>()
            .HasBaseType<User>()
            .ToTable("LecturerReg");

        // Configure auto-increment for LecturerId without making it a key
        modelBuilder.Entity<LecturerReg>()
            .Property(l => l.LecturerId)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Majors>() // This is the entity that we are going to be working with
            .HasMany(m => m.Courses) // A major has many courses
            .WithMany(c => c.Majors) // A course has many majors
            .UsingEntity(joinTbl => joinTbl.ToTable("MajorCourses")); // This is the name of the join table used to handle the many-to-many relationship

        // One course has many students and one student can take many courses 
        modelBuilder.Entity<Courses>()
            .HasMany(c => c.Students)
            .WithMany(s => s.Courses)
            .UsingEntity(joinTbl => joinTbl.ToTable("CourseStudents"));

        // One class can have many students and one student can take many classes 
        modelBuilder.Entity<Classes>()
            .HasMany(cl => cl.Students)
            .WithMany(s => s.Classes)
            .UsingEntity(joinTbl => joinTbl.ToTable("ClassStudents"));

        // One lecturer teaches many classes and one class can have many lecturers 
        modelBuilder.Entity<Classes>()
            .HasMany(cl => cl.Lecturers)
            .WithMany(l => l.Classes)
            .UsingEntity(joinTbl => joinTbl.ToTable("ClassLecturers"));

        // One course can have many classes and many classes can have one course
        modelBuilder.Entity<Courses>()
            .HasMany(c => c.Classes)
            .WithOne(cl => cl.Courses)
            .HasForeignKey(cl => cl.CourseId); // This is the foreign key that is going to be used to link the two tables together 

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
            .HasOne(a => a.LecturerUser) // An Assignment has one Lecturer.
            .WithMany(l => l.Assignments)
            .HasForeignKey(a => a.LecturerUser_Id)// Foreign key in the Assignment table.
            // .HasPrincipalKey(u => u.Id)  // Explicitly point to User.Id
            .OnDelete(DeleteBehavior.Restrict); // Prevent deletion of User if there are assignments linked to it

        // Configure the relationship between Submission and Student
        modelBuilder.Entity<Submission>()
            .HasOne(s => s.Student) // A Submission has one Student.
            .WithMany() // A Student can have many Submissions.
            .HasForeignKey(s => s.Student_ID) // Foreign key in the Submission table.
            .OnDelete(DeleteBehavior.Cascade); // If a Student is deleted, their Submissions are also deleted.
    }
}