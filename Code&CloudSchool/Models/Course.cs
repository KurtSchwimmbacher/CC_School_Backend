using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code_CloudSchool.Models;

public class Course
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get; set;}  //PK

    [Required]
    public required string courseCode {get; set;}

    [MaxLength(50)]
    public string courseTitle {get; set;} = string.Empty;

    public string courseDescription {get; set;} = string.Empty;

    public string courseStatus {get; set;} = "Active"; //active - inactive - suspended - etc

    public DateTime dateCreated {get; set;} = DateTime.UtcNow;

    public DateTime dateUpdated {get; set;} = DateTime.UtcNow;
}
