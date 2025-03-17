using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code_CloudSchool.Models;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get; set;}  //PK
    
    public string Name {get; set;} = string.Empty;

    public string lastName {get; set;} = string.Empty;

    [EmailAddress]
    public string Email {get; set;} = string.Empty;

    [Required]
    public required string Password {get; set;}

    public string Role {get; set;} = "User"; //Admin - Student - Lecturer


}
