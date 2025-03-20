using System;

namespace Code_CloudSchool.DTOs;

public class LecturerDTO
{
    public int LecturerId { get; set; }
    public string LecEmail { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string LecName { get; set; } = default!;

    //public List<CourseDTO> Courses { get; set; } = new List<CourseDTO>();
}
