using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code_CloudSchool.Models;

public class Modules
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int moduleId { get; set; }

    // Stores Week 1 for example
    [Required]
    public string GroupTitle { get; set; } = string.Empty;

    // stores Week 1: practical
    [Required]
    public string Title { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    [Url]
    public string SlideUrl { get; set; } = string.Empty;

    public string AdditionalResources { get; set; } = string.Empty;

    public Boolean? published { get; set; } = false;

    // relation to course 
    public int CourseId { get; set; }
    public Courses Course { get; set; }

}
