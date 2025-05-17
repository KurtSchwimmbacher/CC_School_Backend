using System;

namespace Code_CloudSchool.DTOs;

public class TimetableDTO
{
    public int ClassID { get; set; }
    public string ClassName { get; set; }
    public string ClassDescription { get; set; }
    public int TimeSlotId { get; set; }
    public string Day { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
    public List<string> Students { get; set; }
    public List<string> Lecturers { get; set; }
}
