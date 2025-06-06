using System;
using Code_CloudSchool.Models;

namespace Code_CloudSchool.Interfaces;

public interface IEmailVerificationService
{
    Task<string> GenerateAndStoreToken(User user);
    Task<bool> SendVerificationEmail(User user, string token);
}
