using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Code_CloudSchool.DTOs;

public class AdminRegisterDTO
{
    [Required]
    public required string FName { get; set; }

    [Required]
    public required string LName { get; set; }

    [Required]
    public required string Password { get; set; }

    public string? phoneNumber {get; set; } = string.Empty;

    [Required]
    public required string AdminRole { get; set; }

    [Required]
    public required string Department { get; set; }

}
