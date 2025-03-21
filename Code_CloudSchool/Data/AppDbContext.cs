// Data/AppDbContext.cs
using Microsoft.EntityFrameworkCore;
using Code_CloudSchool.Models;

namespace Code_CloudSchool.Data
{
    public class AppDbContext : DbContext
    {
        // Constructor to pass DbContextOptions to the base DbContext class.
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // DbSet properties for each model. These represent the tables in the database.
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<Grade> Grades { get; set; }

        // Configure relationships and constraints in the database schema.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Call the base method to ensure default configurations are applied.

            // Configure the one-to-many relationship between Assignment and Submission.
            modelBuilder.Entity<Assignment>()
                .HasMany(a => a.Submissions) // An Assignment can have many Submissions.
                .WithOne(s => s.Assignment) // A Submission belongs to one Assignment.
                .HasForeignKey(s => s.AssignmentId) // Foreign key in the Submission table.
                .OnDelete(DeleteBehavior.Cascade); // If an Assignment is deleted, its Submissions are also deleted.

            // Configure the one-to-one relationship between Submission and Grade.
            modelBuilder.Entity<Submission>()
                .HasOne(s => s.Grade) // A Submission can have one Grade.
                .WithOne(g => g.Submission) // A Grade belongs to one Submission.
                .HasForeignKey<Grade>(g => g.SubmissionId) // Foreign key in the Grade table.
                .OnDelete(DeleteBehavior.Cascade); // If a Submission is deleted, its Grade is also deleted.
        }
    }
}