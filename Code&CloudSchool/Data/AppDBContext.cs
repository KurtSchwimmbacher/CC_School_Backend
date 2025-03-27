using System;
using Code_CloudSchool.Models;
using Microsoft.EntityFrameworkCore;

namespace Code_CloudSchool.Data;

public class AppDBContext : DbContext
{
    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

    //below are the tables that we are going to be working with
    public DbSet<Majors> Majors { get; set; } //adding majors to the database so that we can use it in the database and in our application
    public DbSet<Courses> Courses { get; set; }
    public DbSet<Classes> Classes { get; set; }
    public DbSet<Lecturers> Lecturers { get; set; }
    public DbSet<Students> Students { get; set; }



    //Add Relationships below 

    protected override void OnModelCreating(ModelBuilder modelBuilder) //this is a method that is called when the model is being created
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Majors>() //this is the entity that we are going to be working with
            .HasMany(m => m.Courses) // a major has many courses
            .WithMany(c => c.Majors) // a course has many majors
            .UsingEntity(joinTbl => joinTbl.ToTable("MajorCourses")); // this is the name of the join table used to handle the many to many relationship


        //one course has many studdents and one student can take many courses 
        modelBuilder.Entity<Courses>()
            .HasMany(c => c.Students)
            .WithMany(s => s.Courses)
            .UsingEntity(joinTbl => joinTbl.ToTable("CourseStudents"));

        // one class can have many students and one student can take many classes 
        modelBuilder.Entity<Classes>()
            .HasMany(cl => cl.Students)
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
