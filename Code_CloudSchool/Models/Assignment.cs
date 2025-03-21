// Models/Assignment.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code_CloudSchool.Models
{
    public class Assignment
    {
        [Key] // Marks this property as the primary key in the database.
        public int Id { get; set; }

        [Required] // Ensures the Title property is not null or empty.
        [StringLength(100)] // Limits the Title to 100 characters.
        public string Title { get; set; }

        [Required] // Ensures the Description property is not null or empty.
        public string Description { get; set; }

        [Required] // Ensures the DueDate property is not null.
        public DateTime DueDate { get; set; }

        [Required] // Ensures the LecturerId property is not null.
        [ForeignKey("Lecturer")] // Specifies that LecturerId is a foreign key for the Lecturer navigation property.
        public int LecturerId { get; set; }

        // Navigation property to the Lecturer who created this assignment.
        public Lecturer Lecturer { get; set; }

        // Navigation property to the list of submissions for this assignment.
        public ICollection<Submission> Submissions { get; set; }
    }
}