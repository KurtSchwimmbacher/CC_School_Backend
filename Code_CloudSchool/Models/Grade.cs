// Models/Grade.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code_CloudSchool.Models
{
    public class Grade
    {
        [Key] // Marks this property as the primary key in the database.
        public int Id { get; set; }

        [Required(ErrorMessage = "Submission ID is required.")] // Ensures the SubmissionId property is not null.
        [ForeignKey("Submission")] // Specifies that SubmissionId is a foreign key for the Submission navigation property.
        public int SubmissionId { get; set; }

        // Navigation property to the Submission being graded.
        public Submission Submission { get; set; }

        [Required(ErrorMessage = "Score is required.")] // Ensures the Score property is not null.
        [Range(0, 100, ErrorMessage = "Score must be between 0 and 100.")] // Ensures the Score is within a valid range.
        public decimal Score { get; set; }

        [StringLength(500, ErrorMessage = "Feedback cannot exceed 500 characters.")] // Limits the Feedback to 500 characters.
        public string Feedback { get; set; }
    }
}