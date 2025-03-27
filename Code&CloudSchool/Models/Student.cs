using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Code_CloudSchool.Models;

public class Student : User
{
    [StringLength(12)]
    public string StudentNumber { get; set; } = string.Empty;


    public string Email { get; set; } = string.Empty;

    [Required]
    public required string Gender { get; set; } = "Other"; // Default: Male / Female / Other

    public string? Address { get; set; }

    public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;

    [StringLength(50)]
    public string YearLevel { get; set; } = "1st Year";

    [StringLength(50)]
    public string Status { get; set; } = "Pending"; // Active, Graduated, Suspended, Expelled, etc.

    // Override Role to always be "Student"
    public Student()
    {
        Role = "Student";
    }

    //for relationships
    public List<Courses> Courses { get; set; } //this is a list of courses that the student is taking
    public List<Classes> Classes { get; set; } //this is a list of classes that the student is taking 
}
