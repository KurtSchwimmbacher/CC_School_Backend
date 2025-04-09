using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code_CloudSchool.Models;

// Represents a student's submission for an assignment
// Maps to 'Submissions' table in PostgreSQL database
public class Submission
{
    [Key] // Primary key - unique identifier for each submission
    public int Submission_ID { get; set; }

    [Required] // Mandatory field - every submission must link to an assignment
    [ForeignKey("Assignment")] // References primary key in Assignment table
    public int Assignment_ID { get; set; } // Foreign key to assignment

    [Required] // Mandatory field - every submission must have a student
    [ForeignKey("Student")] // References primary key in Student table
    public int Student_ID { get; set; } // Foreign key to student

    [Required] // Mandatory field - submission must have file reference
    public string? FilePath { get; set; } // Server path to submitted file (e.g., PDF)

    [Required] // Mandatory field - submission timestamp required
    public DateTime SubmissionDate { get; set; } // When submission was uploaded

    // Navigation property to linked assignment
    // Initialised as non-null to prevent null reference issues
    public Assignment Assignment { get; set; } = null!;

    // Navigation property to student who submitted
    // Initialised as non-null to prevent null reference issues
    public Student Student { get; set; } = null!;

    // Navigation property to grading information
    // Initialised in constructor to establish circular relationship
    public Grade Grade { get; set; } = null!;

    // Constructor ensures Grade is properly initialised with back-reference
    public Submission()
    {
        // Sets up circular relationship between submission and grade
        Grade = new Grade { Submission = this };
    }
}