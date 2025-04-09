using System;
using System.ComponentModel.DataAnnotations;

namespace Code_CloudSchool.DTOs;

// This DTO is used to update an existing assignment submission.
// It supports partial updates by allowing optional fields where appropriate.
public class UpdateSubmissionDTO
{
    // The unique identifier of the submission that is being updated.
    // This field is required to ensure the system knows which record to modify.
    [Required]
    public int SubmissionId { get; set; }
        
    // Optional field for updating the file path of the submission.
    // Nullable to allow updates where the file path doesn't change.
    public string? FilePath { get; set; }
        
    // Optional field for updating the submission date.
    // Nullable so that existing dates remain unchanged if not explicitly updated.
    public DateTime? SubmissionDate { get; set; }
}
