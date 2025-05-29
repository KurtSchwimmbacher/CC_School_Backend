using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code_CloudSchool.Models;

public class Assignment
{
    [Key] // This is the primary key for the Assignment table.
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Assignment_ID { get; set; }

    [Required] // The Title field is required and cannot be null.
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; } // Optional description for the assignment.

    [Required] // The DueDate field is required and cannot be null.
    public DateTime DueDate { get; set; }
    
    // === New Course relationship ===
    public int CourseId { get; set; } // Foreign key

    [ForeignKey("CourseId")]
    public Courses? Course { get; set; } // Navigation property

    // Foreign key reference to Lecturer
    public int LecturerId { get; set; }  // <--- Add this property

    [ForeignKey("LecturerId")]
    public  LecturerReg? Lecturer { get; set; }  // <--- Navigation property

    // Navigation property to the list of submissions for this assignment.
    public ICollection<Submission> Submissions { get; set; } = new List<Submission>();
}