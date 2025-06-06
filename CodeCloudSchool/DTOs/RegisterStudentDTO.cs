using System;

namespace Code_CloudSchool.DTOs;

public class RegisterStudentDTO
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string PrivateEmail { get; set; }
    public string Gender { get; set; }
    public string? Address { get; set; }
    public string YearLevel { get; set; }
}


