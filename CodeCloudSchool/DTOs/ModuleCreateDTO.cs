using System;

namespace Code_CloudSchool.DTOs;

public class ModuleCreateDTO
{
    public string GroupTitle { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string SlideUrl { get; set; }
    public string AdditionalResources { get; set; }
    public bool Published { get; set; }
    public int CourseId { get; set; }
}
