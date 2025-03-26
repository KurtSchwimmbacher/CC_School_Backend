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
    public int courseCode { get; set; }

    public string? courseDescription { get; set; } //this is a nullable field

    public List<Majors> Majors { get; set; } = []; // this is a list of majors that the course belongs to
    public List<Classes> Classes { get; set; } = []; // this is a list of classes that are being offered for the course
    public List<Students> Students { get; set; } = []; // this is a list of students that are taking the course 
}
