using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code_CloudSchool.Models;

public class Courses
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int courseId { get; set; }

    [Required]
    public string courseName { get; set; }

    [Required]
    public int courseCode { get; set; }

    public string? courseDescription { get; set; }

    public List<Majors> Majors { get; set; } = [];
    public List<Students> Students { get; set; } = [];
}
