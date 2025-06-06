using System;
using Code_CloudSchool.Data;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.Models;
using Microsoft.EntityFrameworkCore;

namespace Code_CloudSchool.Services;

public class AdminAuthService : IAdminAuth
{

    // dependency inj
    private readonly AppDBContext _context;

    // constructor
    public AdminAuthService(AppDBContext context)
    {
        _context = context;
    }


    //Implement interface
    public async Task<Admin?> EmailExists(string email)
    {
        // compares passed email to emails to find if email exists
        Admin? adminFromDB = await _context.Admins.FirstOrDefaultAsync(a => a.AdminEmail == email);

        // returns null if no email
        // returns admin if email found
        return adminFromDB;
    }

    public Task<string> GenerateAdminEmail(string name, string surname, string adminRole)
    {
        // takes in name + role
        // result should look like => KurtSchwimmbacher.Moderator@codecloudschool.com
        return Task.FromResult(name + surname + "." + adminRole + "@codecloudschool.com");
    }

    public Task<string> HashPassword(string password)
    {
        // returns BCRYPT hashed password
        // but cooler because one line :)
        return Task.FromResult(BCrypt.Net.BCrypt.EnhancedHashPassword(password, 13));
    }

    public Task<string> LoginAdmin(string password, string email)
    {
        // 1. Check if user exists
        Admin? adminFromDB = EmailExists(email).Result;

        // 1.1 if not return not found
        if (adminFromDB == null)
        {
            return Task.FromResult("Admin Doesn't Exist");
        }

        // if admin exists
        // 2. Does password match?
        // passes admin and pword to function -> fetches boolean
        if (!ValidatePassword(adminFromDB, password).Result)
        {
            return Task.FromResult("Password is incorrect");
        }

        // 3. If admin exists && pword matches return success
        return Task.FromResult("Login Successful");
    }

    public async Task<Admin> RegisterAdmin(Admin admin)
    {
        // 1. does admin exist?
        Admin? doesAdminExist = EmailExists(admin.AdminEmail).Result;

        if (doesAdminExist != null)
        {
            // if admin exists cant register again
            return null;
        }

        // if admin doesnt exist yet:

        // take in password && hash
        // updates existing pword field
        admin.Password = HashPassword(admin.Password).Result;

        // make admin email
        admin.AdminEmail = GenerateAdminEmail(admin.Name, admin.LastName, admin.AdminRole).Result;
        
        _context.Admins.Update(admin);
        await _context.SaveChangesAsync();

        return admin;

    }

    public Task<bool> ValidatePassword(Admin admin, string password)
    {
        /// uses BCrypt => returns true if passwords match
        return Task.FromResult(BCrypt.Net.BCrypt.EnhancedVerify(password, admin.Password));
    }
}
