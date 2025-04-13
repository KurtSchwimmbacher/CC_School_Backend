using System;

namespace Code_CloudSchool.DTOs;

public class ClassLecturerDTO
{
    public int ClassId { get; set; }
    public string ClassName { get; set; } = string.Empty;
    public string ClassDescription { get; set; } = string.Empty;
    public List<LecturerDTO> Lecturers { get; set; } = new List<LecturerDTO>();
}
