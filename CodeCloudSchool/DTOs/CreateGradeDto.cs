using System;
using System.ComponentModel.DataAnnotations;

namespace Code_CloudSchool.DTOs;

public class CreateGradeDto
{
    [Required(ErrorMessage = "Submission ID is required")]
    public int SubmissionId { get; set; }
    
    [Required(ErrorMessage = "Score is required")]
    [Range(0, 100, ErrorMessage = "Score must be between 0 and 100")]
    public decimal Score { get; set; }
    
    public string? Feedback { get; set; }

}
