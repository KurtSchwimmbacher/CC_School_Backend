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
    public string Title { get; set; } = string.Empty; // Initialize to avoid null issues.

    public string? Description { get; set; } // Optional description for the assignment.

    [Required] // The DueDate field is required and cannot be null.
    public DateTime DueDate { get; set; }

    [ForeignKey("Class")] // This is a foreign key linking to the Class table.
    public int Class_ID { get; set; }

    // Navigation property to the Class this assignment belongs to.
    public Class? Class { get; set; } // Use nullable to avoid issues if Class is not loaded.

    // Navigation property to the list of submissions for this assignment.
    public ICollection<Submission> Submissions { get; set; } = new List<Submission>(); // Initialize to avoid null issues.
}