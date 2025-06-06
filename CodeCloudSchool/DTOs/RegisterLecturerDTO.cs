using System;

namespace Code_CloudSchool.DTOs;

public class RegisterLecturerDTO
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public string PrivateEmail { get; set; }
    public string Department { get; set; }
    public DateTime DateOfJoining { get; set; }
}
