using System;

namespace Code_CloudSchool.DTOs;

public class TimetableGenerationResultDTO
{
    public int TotalClassesProcessed { get; set; }
    public int SuccessfullyScheduled { get; set; }
    public int UnassignedClasses { get; set; }
    public int SkippedEmptyClasses { get; set; }
    public List<int> UnassignedClassIds { get; set; } = new();
    public List<int> SkippedClassIds { get; set; } = new();
    public string Message { get; set; } = string.Empty;
}

