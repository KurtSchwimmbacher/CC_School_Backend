using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code_CloudSchool.Models;

public class Classes
{
    [Key] //this tells the DB that public int classID is the primary key for the table
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // this tells the DB to auto increment the classID 
    public int classID { get; set; }

    [Required] //this tells the DB that the className is a required field
    public string className { get; set; } = string.Empty; //this is a nullable field
    public string classDescription { get; set; } = string.Empty; //this is a nullable field

    public DateTime? classTime { get; set; } //this is the time the class is scheduled to start
    public DateTime? classEndTime { get; set; } //this is the time the class is scheduled to end 
    public List<Students> Students { get; set; } = []; // this is a list of students that are in the class
    public List<Courses> Courses { get; set; } = []; // this is a list of courses for that the class belongs to 
    public List<Lecturers> Lecturers { get; set; } = []; // this is a list of lecturers that are teaching the class
}
