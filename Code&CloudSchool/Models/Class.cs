using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code_CloudSchool.Models;

public class Class
{
    [Key] // This is the primary key for the Class table.
    public int Class_ID { get; set; }

    [Required] // The Subject field is required and cannot be null.
    public string Subject { get; set; } = string.Empty;

    [ForeignKey("Teacher")] // Foreign key linking to the Teacher table.
    public int Teacher_ID { get; set; }

    [ForeignKey("Student")] // Foreign key linking to the Student table.
    public int Student_ID { get; set; }

    // Navigation property to the Teacher who teaches this class.
    public Teacher? Teacher { get; set; }

    // Navigation property to the Student enrolled in this class.
    public Student? Student { get; set; }

    // Navigation property to the list of assignments for this class.
    public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
}

public class Teacher
{
}