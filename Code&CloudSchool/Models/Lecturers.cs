using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code_CloudSchool.Models;

public class Lecturers
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int lecturersId { get; set; }

    public List<Classes> Classes { get; set; } //this is a list of classes that the lecturer is teaching 

}
