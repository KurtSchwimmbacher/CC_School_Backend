using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Code_CloudSchool.Models;

public class Student : User
{
    [Required]
    [StringLength(50)]
    public required string StudentNumber { get; set; } = string.Empty; 

    [Required]
    public required string Gender { get; set; } = "Other"; // Default: Male / Female / Other

    public string? Address { get; set; }

    [Required]
    public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;

    [Required]
    [StringLength(50)]
    public string YearLevel { get; set; } = "1st Year";

    [Required]
    [StringLength(50)]
    public string Status { get; set; } = "Active"; // Active, Graduated, Suspended, Expelled, etc.

    // Override Role to always be "Student"
    public Student()
    {
        Role = "Student";
    }
}
