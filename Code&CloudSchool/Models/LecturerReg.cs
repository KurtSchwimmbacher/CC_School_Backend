using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code_CloudSchool.Models;

public class LecturerReg : User
{

    // Define the LecturerReg class representing the lecturer registration table in the database
    // Mark Id as the primary key
    //[Key]
    // Configure the database to auto-generate the value for Id
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
   
    public int LecturerId { get; set; }

    // Lecturer's first name, required with a maximum length of 50 characters
    [Required]
    [StringLength(50)]
    public string LectName { get; set; } = string.Empty;

    // Lecturer's last name, required with a maximum length of 50 characters
    [Required]
    [MaxLength(50)]
    public string LecLastName { get; set; } = string.Empty;

    // Lecturer's email, required, must be a valid email format, with a maximum length of 150 characters
    [Required]
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

//For relationships 
    public List<Classes> Classes { get; set; } //this is a list of classes that the lecturer is teaching 

    public List<Majors> Majors { get; set; } //this is a list of majors that the lecturer is teaching
    public List<Courses> Courses { get; set; } //this is a list of courses that the lecturer is teaching
    public List<Assignment> Assignments { get; set; } //this is a list of assignments that the lecturer is giving to the students


}


