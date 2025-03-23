using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks.Dataflow;

namespace Code_CloudSchool.Models;

public class Admin : User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // auto generate the value
    public int AdminId { get; set; }

    public string AdminEmail { get; set; } = string.Empty;

    public DateTime JoinedDate { get; set; } = DateTime.UtcNow;

    public String AdminRole { get; set; } = "Moderator"; //super admin / faculty head / moderator

    public String? AssignedDepartments { get; set; } = String.Empty; // IT / HR / Lecturer
}
