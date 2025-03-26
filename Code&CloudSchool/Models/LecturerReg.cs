using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code_CloudSchool.Models;

// Define the LecturerReg class representing the lecturer registration table in the database
public class LecturerReg
{
    // Mark Id as the primary key
    [Key]
    // Configure the database to auto-generate the value for Id
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

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

    // Password for the lecturer's account (consider hashing before storing)
    public string Password { get; set; } = string.Empty;

    // Lecturer's phone number, required, with a maximum length of 50 characters
    [Required]
    [MaxLength(50)]
    public string PhoneNumber { get; set; } = string.Empty;

    // Department the lecturer belongs to, required
    [Required]
    public string Department { get; set; } = string.Empty;

    // Date when the lecturer joined, required
    [Required]
    public DateTime DateOfJoining { get; set; }

    // Status of the lecturer (active or inactive), defaults to active
    public bool IsActive { get; set; } = true;
}