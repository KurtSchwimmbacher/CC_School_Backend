using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code_CloudSchool.Models;

public class Majors
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string? MajorName { get; set; }
    [Required]
    public string? MajorCode { get; set; }
    public string MajorDescription { get; set; } = string.Empty; //this is a nullable field

    [Required]
    public int? CreditsRequired { get; set; }

    public List<Courses> Courses { get; set; } = []; // this is a list of courses that the major has
    public List<Student> Students { get; set; } = []; // this is a list of students that are in the major 

}

//---
//sojfeo: [
//   "Id": 1,
//   "MajorName": "Software Engineering",
//   "MajorCode": "SWE",
//   "MajorDescription": "Learn how to build software applications",
//   "CreditsRequired": 120, 
//   "Courses": [ 
//     {    }
//   ],
//   "Students": [