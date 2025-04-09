using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code_CloudSchool.Models;

// This class represents the Grade entity in the database.
// Each grade is associated with a submission and stores a numerical score along with optional feedback.
public class Grade
{
    // Primary key for the Grade table.
    // This uniquely identifies each grade record in the database.
    [Key]
    public int Grade_ID { get; set; }

    // Foreign key that links this grade to a specific submission.
    // It is required because every grade must be associated with a submission.
    [Required]
    [ForeignKey("Submission")]
    public int Submission_ID { get; set; }

    // The score awarded for the submission.
    // This is a required field and must be a value between 0 and 100.
    // Using decimal here allows for decimal points (e.g., 87.5).
    [Required]
    [Range(0, 100, ErrorMessage = "Score must be between 0 and 100.")]
    public decimal Score { get; set; }

    // Optional feedback provided by the lecturer.
    // This could be used to justify the grade or give the student constructive comments.
    public string? Feedback { get; set; }

    // Navigation property that represents the submission linked to this grade.
    // Marked as non-nullable using null-forgiving operator to suppress warnings.
    // This allows Entity Framework to automatically handle relationship loading.
    public Submission Submission { get; set; } = null!;
}
