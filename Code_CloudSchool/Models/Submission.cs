// Models/Submission.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code_CloudSchool.Models
{
    public class Submission
    {
        [Key] // This is the primary key for the Submission table.
        public int Submission_ID { get; set; }

        [Required] // The Assignment_ID field is required and cannot be null.
        [ForeignKey("Assignment")] // This is a foreign key linking to the Assignment table.
        public int Assignment_ID { get; set; }

        [Required] // The Student_ID field is required and cannot be null.
        [ForeignKey("Student")] // This is a foreign key linking to the Student table.
        public int Student_ID { get; set; }

        [Required] // The FilePath field is required and cannot be null.
        public string FilePath { get; set; } // Path to the submitted file.

        [Required] // The SubmissionDate field is required and cannot be null.
        public DateTime SubmissionDate { get; set; } // Date and time of submission.

        // Navigation property to the Assignment this submission belongs to.
        public Assignment Assignment { get; set; }

        // Navigation property to the Student who made this submission.
        public Student Student { get; set; }

        // Navigation property to the Grade for this submission (if graded).
        public Grade Grade { get; set; }
    }
}