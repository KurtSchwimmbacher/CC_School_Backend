using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks.Dataflow;

namespace Code_CloudSchool.Models;

public class Admin : User
{   
    [Required]
    public required int AdminId {get; set; }

    public DateTime JoinedDate {get; set; } = DateTime.UtcNow;

    public String AdminRole {get; set; } = "Moderator"; //super admin / faculty head / moderator

    public String? AssignedDepartments {get; set; } = String.Empty; // IT / HR / Lecturer
}
