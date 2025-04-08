using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Code_CloudSchool.DTOs;

public class CreateGradeDto
{
    [Required(ErrorMessage = "Submission ID is required")]
    [JsonPropertyName("Submission_ID")] 
    public int Submission_ID { get; set; }
    
    [Required(ErrorMessage = "Score is required")]
    [Range(0, 100, ErrorMessage = "Score must be between 0 and 100")]
    public decimal Score { get; set; }
    
    public string? Feedback { get; set; }
}