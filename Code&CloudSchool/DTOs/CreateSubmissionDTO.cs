using System;
using System.ComponentModel.DataAnnotations;

namespace Code_CloudSchool.DTOs;

// This Data Transfer Object (DTO) is used to handle incoming data when a student submits an assignment.
// It ensures only the required and relevant fields are passed from the client to the backend.
// Validation attributes help ensure data integrity before processing.
public class CreateSubmissionDTO
{
    // The ID of the assignment being submitted.
    // This is a required field, as the submission must be linked to a specific assignment.
    [Required(ErrorMessage = "Assignment ID is required")]
    public int AssignmentId { get; set; }
        
    // The ID of the student submitting the assignment.
    // Also a required field to associate the submission with a specific student.
    [Required(ErrorMessage = "Student ID is required")]
    public int StudentId { get; set; }
        
    // The file path or location of the submitted file (e.g., PDF, Word document).
    // This is typically a reference to a file stored in a cloud bucket or local directory.
    [Required(ErrorMessage = "File path is required")]
    public string FilePath { get; set; }
        
    // The date and time the assignment was submitted.
    // By default, this is automatically set to the current UTC time when the DTO is created.
    public DateTime SubmissionDate { get; set; } = DateTime.UtcNow;
}
