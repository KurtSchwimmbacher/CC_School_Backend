using System;
using System.ComponentModel.DataAnnotations; // Provides validation attributes for properties
using System.Text.Json.Serialization; // Enables control over JSON property naming during serialization/deserialization

namespace Code_CloudSchool.DTOs;

// This DTO is used when creating a new grade entry.
// It ensures that only the necessary and safe fields are accepted from the client side.
public class CreateGradeDto
{
    // This field is required and represents the ID of the submission being graded.
    // Without this ID, the system wouldn’t know which submission to attach the grade to.
    [Required(ErrorMessage = "Submission ID is required")]
    
    // Ensures the property name appears as "Submission_ID" in the JSON body when received or sent via API.
    [JsonPropertyName("Submission_ID")] 
    public int Submission_ID { get; set; }

    // The numeric score given to the submission.
    // This field is mandatory, and its value must fall within the 0–100 range to be valid.
    [Required(ErrorMessage = "Score is required")]
    [Range(0, 100, ErrorMessage = "Score must be between 0 and 100")]
    public decimal Score { get; set; }

    // Optional written feedback provided by the lecturer or system about the submission.
    // Nullable in case no comments are needed.
    public string? Feedback { get; set; }
}
