using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code_CloudSchool.Models;

public class Assignment
{
    [Key] // This is the primary key for the Assignment table.
    public int Assignment_ID { get; set; }

    [Required] // The Title field is required and cannot be null.
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; } // Optional description for the assignment.

    [Required] // The DueDate field is required and cannot be null.
    public DateTime DueDate { get; set; }
    
    // Foreign key reference to Lecturer's User ID (base class)
    // Changed from LecturerId to reference the User table's Id for proper inheritance mapping
    public int LecturerUserId { get; set; }  // <--- Now references User.Id instead of LecturerReg.LecturerId

    [ForeignKey("LecturerUserId")]  // <--- Updated to match the new foreign key property
    public LecturerReg? Lecturer { get; set; }  // <--- Navigation property remains the same

    // Navigation property to the list of submissions for this assignment.
    public ICollection<Submission> Submissions { get; set; } = new List<Submission>();
}