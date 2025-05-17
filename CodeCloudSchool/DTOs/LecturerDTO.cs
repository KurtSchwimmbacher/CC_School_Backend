using System;

namespace Code_CloudSchool.DTOs;

public class LecturerDTO
{
    public int LecturerId { get; set; }
    public string LecEmail { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string LecName { get; set; } = default!;

    public string LectName { get; set; } = string.Empty;

    public string Department { get; set; } = string.Empty;

    public DateTime DateOfJoining { get; set; }
    
    public bool IsActive { get; set; } = true;

    //public List<CourseDTO> Courses { get; set; } = new List<CourseDTO>();
}
