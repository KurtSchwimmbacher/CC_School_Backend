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

        // configure student inheritance
        modelBuilder.Entity<Student>().HasBaseType<User>();

        // unique constraint for student number
        modelBuilder.Entity<Student>().HasIndex(s => s.StudentNumber).IsUnique();

    }


}
