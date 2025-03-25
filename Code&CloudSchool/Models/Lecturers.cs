using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code_CloudSchool.Models;

public class Lecturers
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int lecturersId { get; set; }

}
