using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code_CloudSchool.Models;

public class Students
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int StudentNumber { get; set; }

    //TODO: Add more properties here 

    public List<Courses> Courses { get; set; } //this is a list of courses that the student is taking
    public List<Classes> Classes { get; set; } //this is a list of classes that the student is taking 
}
