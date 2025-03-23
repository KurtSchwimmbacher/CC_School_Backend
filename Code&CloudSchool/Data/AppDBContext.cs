using System;
using Code_CloudSchool.Models;
using Microsoft.EntityFrameworkCore;

namespace Code_CloudSchool.Data;

public class AppDBContext : DbContext
{
    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

    public DbSet<Courses> Courses { get; set; }
    public DbSet<Majors> Majors { get; set; }
    public DbSet<Students> Students { get; set; }

    //Add Relationships below 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Majors>()
            .HasMany(m => m.Courses)
            .WithMany(c => c.Majors)
            .UsingEntity(joinTbl => joinTbl.ToTable("MajorCourses"));
    }
}
