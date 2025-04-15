using System;
using System.ComponentModel.DataAnnotations;

namespace Code_CloudSchool.Models;

public class Login
{

[Required]
    [Phone]
    public string PhoneNumber { get; set; }  // For SMS/WhatsApp OTP

    [Required]
    public int OTP { get; set; }          // 6-digit code from user

    // Optional: For traditional username/password + 2FA
    public string Email { get; set; }        
    public string Password { get; set; }     

}


