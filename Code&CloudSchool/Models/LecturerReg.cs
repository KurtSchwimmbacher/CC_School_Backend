using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code_CloudSchool.Models;

public class LecturerReg : User
{

    [EmailAddress]
    [MaxLength(150)]
    public string LecEmail { get; set; } = string.Empty;

    [Required]
    public string Department { get; set; } = string.Empty;

    [Required]
    public DateTime DateOfJoining { get; set; }

    public bool IsActive { get; set; } = true;

    public LecturerReg()
    {
        Role = "Lecturer";
    }

    public List<Classes> Classes { get; set; } //this is a list of classes that the lecturer is teaching 


}
