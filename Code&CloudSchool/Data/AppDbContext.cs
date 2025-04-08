using System;
using Code_CloudSchool.Models;
using Microsoft.EntityFrameworkCore;

namespace Code_CloudSchool.Data;

public class AppDBContext : DbContext
{
    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

    //below are the tables that we are going to be working with

    public DbSet<User> User { get; set; }
    public DbSet<Majors> Majors { get; set; } //adding majors to the database so that we can use it in the database and in our application
    public DbSet<Courses> Courses { get; set; }
    public DbSet<Classes> Classes { get; set; }
    public DbSet<LecturerReg> Lecturers { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<Submission> Submissions { get; set; }
    public DbSet<Grade> Grades { get; set; }

    // Define a DbSet for Announcements, representing a table in the database
    public DbSet<Announcements> Announcements { get; set; } = default!;


    //Add Relationships below 

    protected override void OnModelCreating(ModelBuilder modelBuilder) //this is a method that is called when the model is being created
    {
        base.OnModelCreating(modelBuilder);


        // Configure TPT: Separate tables for User and Student
        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<Student>().ToTable("Students");
        modelBuilder.Entity<Admin>().ToTable("Admins");

        // Explicitly map StudentNumber to the Students table
        modelBuilder.Entity<Student>()
            .Property(s => s.StudentNumber)
            .HasMaxLength(12);

        // Unique constraint for StudentNumber
        modelBuilder.Entity<Student>().HasIndex(s => s.StudentNumber).IsUnique();

        // Ensure StudentNumber is not mapped to the User table
        modelBuilder.Entity<User>()
        .Ignore(u => ((Student)u).StudentNumber);

        //one course has many students and one student can take many courses 
        modelBuilder.Entity<Courses>()
            .HasMany(c => c.Student)
            .WithMany(s => s.Courses)
            .UsingEntity(joinTbl => joinTbl.ToTable("CourseStudents"));

        // one class can have many students and one student can take many classes 
        modelBuilder.Entity<Classes>()
            .HasMany(cl => cl.Student)
            .WithMany(s => s.Classes)
            .UsingEntity(joinTbl => joinTbl.ToTable("ClassStudents"));

        // one lecturer teaches many classes and one class can have many lecturers 
        modelBuilder.Entity<Classes>()
            .HasMany(cl => cl.Lecturers)
            .WithMany(l => l.Classes)
            .UsingEntity(joinTbl => joinTbl.ToTable("ClassLecturers"));

        // one course can have many classes and many classes can have one courses
        modelBuilder.Entity<Courses>()
            .HasMany(c => c.Classes)
            .WithOne(cl => cl.Courses)
            .HasForeignKey(cl => cl.CourseId); //this is the foreign key that is going to be used to link the two tables together 


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
            .HasForeignKey(s => s.StudentNumber) // Foreign key in the Submission table.
            .HasPrincipalKey(s => s.StudentNumber) //Map to the Student's studentNumber
            .OnDelete(DeleteBehavior.Cascade); // If a Student is deleted, their Submissions are also deleted.


        // unique constraint for AdminID
        modelBuilder.Entity<Admin>().HasIndex(a => a.AdminId).IsUnique();

        modelBuilder.Entity<Majors>() //this is the entity that we are going to be working with
            .HasMany(m => m.Courses) // a major has many courses
            .WithMany(c => c.Majors) // a course has many majors
            .UsingEntity(joinTbl => joinTbl.ToTable("MajorCourses")); // this is the name of the join table used to handle the many to many relationship

        //one course has many studdents and one student can take many courses 
        modelBuilder.Entity<Courses>()
            .HasMany(c => c.Student)
            .WithMany(s => s.Courses)
            .UsingEntity(joinTbl => joinTbl.ToTable("CourseStudents"));

        // one class can have many students and one student can take many classes 
        modelBuilder.Entity<Classes>()
            .HasMany(cl => cl.Student)
            .WithMany(s => s.Classes)
            .UsingEntity(joinTbl => joinTbl.ToTable("ClassStudents"));

        // one lecturer teaches many classes and one class can have many lecturers 
        modelBuilder.Entity<Classes>()
            .HasMany(cl => cl.Lecturers)
            .WithMany(l => l.Classes)
            .UsingEntity(joinTbl => joinTbl.ToTable("ClassLecturers"));

        // one course can have many classes and many classes can have one courses
        modelBuilder.Entity<Courses>()
            .HasMany(c => c.Classes)
            .WithOne(cl => cl.Courses)
            .HasForeignKey(cl => cl.CourseId); //this is the foreign key that is going to be used to link the two tables together 


    }
}