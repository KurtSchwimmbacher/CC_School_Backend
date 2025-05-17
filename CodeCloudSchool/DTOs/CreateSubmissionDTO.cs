using System;
using System.ComponentModel.DataAnnotations;

namespace Code_CloudSchool.DTOs;

public class CreateSubmissionDTO
{
        [Required(ErrorMessage = "Assignment ID is required")]
        public int AssignmentId { get; set; }

        [Required(ErrorMessage = "Student ID is required")]
        public int? StudentId { get; set; }

        [Required(ErrorMessage = "File path is required")]
        public string? FilePath { get; set; } = string.Empty;

        public DateTime SubmissionDate { get; set; } = DateTime.UtcNow;

}
