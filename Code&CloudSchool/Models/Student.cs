using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code_CloudSchool.Models;

public class Student
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get; set;}  //PK

    [Required]
    [StringLength(50)]
    public required string firstName {get; set;} = string.Empty;

    [Required]
    [StringLength(50)]  
    public required string lastName {get; set;} = string.Empty;

    [Required]
    [StringLength(13)]
    public required string IdNo {get; set;} = string.Empty;

    [Required]
    public required string studentGender {get; set;} = "Other"; //default value -> male / female / other

    [EmailAddress]
    public string? Email {get; set;} = string.Empty;

    [Phone]
    public string? phoneNumber {get; set;} = null;

    public string? Address {get; set;} = null;

    [Required]
    public DateTime enrollmentDate {get; set;} = DateTime.UtcNow;

    [Required]
    [StringLength(50)]
    public string yearLevel {get; set;} = "1st Year";

    [Required]
    [StringLength(50)]
    public string studentStatus {get; set;} = "Active"; //active - Graduated - suspended - expelled etc
}
