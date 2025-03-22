using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code_CloudSchool.Models;

public class Lecturer
{
        public int LecturerId { get; set; }
        public string Name { get; set; } = string.Empty; // Temporary property

}
