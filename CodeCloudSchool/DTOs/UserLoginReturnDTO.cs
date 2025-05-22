using System;

namespace Code_CloudSchool.DTOs;

public class UserLoginReturnDTO
{
    public int UserID { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
    public virtual string Role { get; set; } = "User";
     
}
