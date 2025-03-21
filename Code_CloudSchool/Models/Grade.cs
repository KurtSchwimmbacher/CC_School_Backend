// Models/Grade.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code_CloudSchool.Models
{
    public class Grade
    {
        [Key] // This is the primary key for the Grade table.
        public int Grade_ID { get; set; }

        [Required] // The Submission_ID field is required and cannot be null.
        [ForeignKey("Submission")] // This is a foreign key linking to the Submission table.
        public int Submission_ID { get; set; }

        [Required] // The Score field is required and cannot be null.
        [Range(0, 100)] // The Score must be between 0 and 100.
        public decimal Score { get; set; }

        public string Feedback { get; set; } // Optional feedback for the grade.

        // Navigation property to the Submission this grade belongs to.
        public Submission Submission { get; set; }
    }
}