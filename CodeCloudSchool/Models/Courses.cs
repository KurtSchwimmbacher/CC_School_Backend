using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code_CloudSchool.Models;

public class Courses
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string courseName { get; set; } = string.Empty;

    [Required]
    public int? courseCode { get; set; }

    public string? courseDescription { get; set; } //this is a nullable field

    // public int MajorsId { get; set; } //this is the foreign key that is going to be used to link the two tables together
    // public Majors? Majors { get; set; } // this shows the relationship between the course and the major that is being taught

    public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public List<Majors> Majors { get; set; } = []; // this is a list of majors that are being taught in the course
    public List<Classes> Classes { get; set; } = []; // this is a list of classes that are being offered for the course
    public List<Student> Student { get; set; } = []; // this is a list of students that are taking the course 
}
