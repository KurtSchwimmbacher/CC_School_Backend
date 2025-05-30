using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code_CloudSchool.Models;

// Define the Announcements class representing the announcements table in the database
public class Announcements
{
    // Mark AnnouncementId as the primary key
    [Key]
    // Configure the database to auto-generate the value for AnnouncementId
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AnnouncementId { get; set; }

    // Title of the announcement, initialized as an empty string
    public string Title { get; set; } = string.Empty;

    // Description of the announcement, initialized as an empty string
    public string Description { get; set; } = string.Empty;

    // Date of the announcement, defaulting to the current date and time
    public DateTime Date { get; set; } = DateTime.Now;

    // === New Course relationship ===
    public int CourseId { get; set; } // Foreign key

    [ForeignKey("CourseId")]
    public Courses? Course { get; set; } // Navigation property

    // Foreign key referencing the Lecturer who created the announcement
    public int LecturerId { get; set; }
}