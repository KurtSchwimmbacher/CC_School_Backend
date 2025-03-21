// Models/Submission.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code_CloudSchool.Models
{
    public class Submission
    {
        [Key] // Marks this property as the primary key in the database.
        public int Id { get; set; }

        [Required(ErrorMessage = "Assignment ID is required.")] // Ensures the AssignmentId property is not null.
        [ForeignKey("Assignment")] // Specifies that AssignmentId is a foreign key for the Assignment navigation property.
        public int AssignmentId { get; set; }

        // Navigation property to the Assignment being submitted.
        public Assignment Assignment { get; set; }

        [Required(ErrorMessage = "Student ID is required.")] // Ensures the StudentId property is not null.
        [ForeignKey("Student")] // Specifies that StudentId is a foreign key for the Student navigation property.
        public int StudentId { get; set; }

        // Navigation property to the Student who submitted the assignment.
        public Student Student { get; set; }

        [Required(ErrorMessage = "File path is required.")] // Ensures the FilePath property is not null or empty.
        public string FilePath { get; set; }

        [Required(ErrorMessage = "Submission date is required.")] // Ensures the SubmissionDate property is not null.
        public DateTime SubmissionDate { get; set; }

        // Navigation property to the Grade (if the submission has been graded).
        public Grade Grade { get; set; }
    }
}