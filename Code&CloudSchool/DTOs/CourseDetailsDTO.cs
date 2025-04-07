using System;

namespace Code_CloudSchool.DTOs;

public class CourseDetailsDTO
{
    public int? CourseId { get; set; }

    public string? CourseName { get; set; }
    public int? CourseCode { get; set; }
    public string? CourseDescription { get; set; }
}
