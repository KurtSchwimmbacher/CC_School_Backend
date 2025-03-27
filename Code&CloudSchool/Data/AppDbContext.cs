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

    // Define a DbSet for LecturerReg, representing another table in the database
    public DbSet<LecturerReg> LecturerReg { get; set; } = default!;
}