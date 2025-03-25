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
        modelBuilder.Entity<Majors>() //this is the entity that we are going to be working with
            .HasMany(m => m.Courses) // a major has many courses
            .WithMany(c => c.Majors) // a course has many majors
            .UsingEntity(joinTbl => joinTbl.ToTable("MajorCourses")); // this is the name of the join table used to handle the many to many relationship
    }
}
