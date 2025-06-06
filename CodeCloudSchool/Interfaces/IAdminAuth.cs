using System;
using Code_CloudSchool.Models;

namespace Code_CloudSchool.Interfaces;

public interface IAdminAuth
{

    public Task<string> HashPassword (string password);

    public Task<string> GenerateAdminEmail(string name, string surname, string adminRole);

    public Task<string> LoginAdmin(string password, string email);

    public Task<Admin> RegisterAdmin (Admin admin);

    public Task<Admin?> EmailExists(string email);

    public Task<bool> ValidatePassword(Admin admin, string password);

}
