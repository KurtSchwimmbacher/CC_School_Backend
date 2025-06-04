using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code_CloudSchool.Models;

public class Submission
{
    [Key] // This is the primary key for the Submission table.
    public int Submission_ID { get; set; }

    public int AssignmentId { get; set; } // match naming convention
    public Assignment Assignment { get; set; } = null!;


    public int StudentId { get; set; } // FK to Student.UserId

    [ForeignKey("StudentId")]
    public Student Student { get; set; } = null!;


    [Required] // The FilePath field is required and cannot be null.
    public string FilePath { get; set; } = null!; // Path to the submitted file.

    [Required] // The SubmissionDate field is required and cannot be null.
    public DateTime SubmissionDate { get; set; } // Date and time of submission.





    // Navigation property to the Grade for this submission
    // Initialised in constructor to avoid circular dependency
    public Grade? Grade { get; set; }


    // Constructor ensures Grade is properly initialised with back-reference
    public Submission()
    {
        Grade = new Grade { Submission = this }; // Sets up circular relationship
    }
}