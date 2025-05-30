namespace Code_CloudSchool.DTOs;

public class CourseDescriptDetailsDTO
{
    public List<WeekBreakdown>? courseWeekBreakdown { get; set; } = [];
    public string courseSlides { get; set; } = string.Empty;
    public List<MarkBreakdown>? courseMarkBreakdown { get; set; } = [];
    public List<SemesterDescription>? courseSemDescriptions { get; set; } = [];
}

public class WeekBreakdown
{
    public string header { get; set; }
    public string description { get; set; }
}

public class MarkBreakdown
{
    public string title { get; set; }
    public string mark { get; set; }
    public List<MarkItem> items { get; set; } = [];
}

public class MarkItem
{
    public string description { get; set; }
    public string mark { get; set; }
}

public class SemesterDescription
{
    public string description { get; set; }
}
