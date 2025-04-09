using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code_CloudSchool.Models;

// Represents an assignment in the school management system
// Maps to the 'Assignments' table in PostgreSQL database

public class Assignment
{
    [Key] // This is the primary key for the Assignment table.
    public int Assignment_ID { get; set; }

    [Required] // The Title field is required and cannot be null.
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; } // Optional description for the assignment.

    [Required] // The DueDate field is required and cannot be null.
    public DateTime DueDate { get; set; }
    
    // Links to the lecturer who created this assignment (foreign key)
    public int LecturerUser_Id { get; set; }

    [ForeignKey("LecturerUser_Id")]
     public virtual LecturerReg? LecturerUser { get; set; } // Navigation property to lecturer

    // Stores all student submissions for this assignment
    // Initialised as empty list to avoid null reference exceptions
    public ICollection<Submission> Submissions { get; set; } = new List<Submission>();
}