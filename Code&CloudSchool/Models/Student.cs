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
    [StringLength(12)]
    public required string IdNo {get; set;} = string.Empty;

    [Required]
    public required string studentGender {get; set;} = "Other"; //default value -> male / female / other

    [Required]
    [EmailAddress]
    public required string Email {get; set;} = string.Empty;

    
}
