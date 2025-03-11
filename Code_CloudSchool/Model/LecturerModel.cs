using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace Code_CloudSchool.Model;

public class LecturerModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public int LecturerId { get; set; }

    [Required]
    [StringLength(100)]
    public string? LecturerName { get; set; }

    public string? LecturerSurname { get; set; }

    public string? LecturerEmail { get; set; }

    public string? LecturerPhone { get; set; }

}
