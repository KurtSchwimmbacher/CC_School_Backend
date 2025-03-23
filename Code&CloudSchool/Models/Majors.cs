using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code_CloudSchool.Models;

public class Majors
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int majorId { get; set; }

    [Required]
    public string MajorName { get; set; }
    [Required]
    public string MajorCode { get; set; }
    public string MajorDescription { get; set; }

    [Required]
    public int CreditsRequired { get; set; }

    public List<Courses> Courses { get; set; } = [];
    public List<Students> Students { get; set; } = [];

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