using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Code_CloudSchool.Models;

// Define the application's database context, inheriting from DbContext
public class AppDBContext : DbContext
{
    // Constructor that accepts DbContext options and passes them to the base DbContext class
    public AppDBContext(DbContextOptions<AppDBContext> options)
        : base(options)
    {
    }

    // Define a DbSet for Announcements, representing a table in the database
    public DbSet<Announcements> Announcements { get; set; } = default!;


    //Add Relationships below 

    protected override void OnModelCreating(ModelBuilder modelBuilder) //this is a method that is called when the model is being created
    {
        base.OnModelCreating(modelBuilder);


     

         // Prevent cascade delete (optional: change to Cascade if needed).
        modelBuilder.Entity<Announcements>()
            .HasOne(a => a.Lecturer)
            .WithMany()
            .HasForeignKey(a => a.LecturerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Announcements>()
            .HasOne(a => a.Course)
            .WithMany()
            .HasForeignKey(a => a.courseCode)
            .OnDelete(DeleteBehavior.Restrict);
    }
}