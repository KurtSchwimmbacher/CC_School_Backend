using System;
using Code_CloudSchool.Models;
using Microsoft.EntityFrameworkCore;

namespace Code_CloudSchool.Data;

// This class is used to connect to the database
public class AppDbContext : DbContext
{

    // Constructor - use all the base options for db context
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // list the tables / relationships
    public DbSet<User> User { get; set; }
    public DbSet<Student> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure TPT: Separate tables for User and Student
        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<Student>().ToTable("Students");

        // Unique constraint for StudentNumber
        modelBuilder.Entity<Student>().HasIndex(s => s.StudentNumber).IsUnique();
    }



}
