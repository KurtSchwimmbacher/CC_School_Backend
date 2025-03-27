using System;
using System.ComponentModel.DataAnnotations;

namespace Code_CloudSchool.DTOs;

public class UpdateSubmissionDTO
{
        [Required]
        public int SubmissionId { get; set; }
        
        public string? FilePath { get; set; }
        
        public DateTime? SubmissionDate { get; set; }

}
