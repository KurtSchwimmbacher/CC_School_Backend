using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code_CloudSchool.Models;

public class LecturerReg
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public int Id { get;set;}

    [Required]
    [StringLength (50)]
    public string LectName {get; set;} = string.Empty;

    [Required]
    [MaxLength(50)]
    public string LecLastName {get;set;} =string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(150)]
    public string LecEmail {get;set;} = string.Empty;

    public string Password {get;set;} = string.Empty;

    [Required]
    [MaxLength(50)]
    public string PhoneNumber {get;set;} = string.Empty;
    
    [Required]
    public string Department {get;set;} = string.Empty;

    [Required]
    public DateTime DateOfJoining {get;set;} 

    public bool IsActive {get;set;} = true;

}

