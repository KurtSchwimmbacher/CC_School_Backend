using System;
using Code_CloudSchool.Data;
using Code_CloudSchool.DTOs;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.Models;
using Microsoft.EntityFrameworkCore;

namespace Code_CloudSchool.Services;

public class AdminPasswordService : IUpdateAdminPassword
{

    private readonly AppDBContext _context;

    public AdminPasswordService(AppDBContext context)
    {
        _context = context;
    }

    public async Task<bool> UpdateAdminPassword(string AdminEmail, StudentPasswordDTO PasswordDTO)
    {
        // find admin by email
        Admin? admin = await _context.Admins.FirstOrDefaultAsync(a => a.AdminEmail == AdminEmail);

        if (admin == null)
        {
            // admin not found
            return false;
        }

        // verify old password
        bool isPasswordValid = BCrypt.Net.BCrypt.EnhancedVerify(PasswordDTO.OldPassword, admin.Password);
        if (!isPasswordValid)
        {
            return false; //Incorrect old password
        }

        // Hash the pword
        string hashedNewPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(PasswordDTO.NewPassword, 13);
        admin.Password = hashedNewPassword;
        _context.Admins.Update(admin);
        await _context.SaveChangesAsync();

        return true;
    }
}
