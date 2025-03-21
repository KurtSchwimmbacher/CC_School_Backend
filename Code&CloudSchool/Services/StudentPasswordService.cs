using System;
using Code_CloudSchool.Data;
using Code_CloudSchool.DTOs;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.Models;
using Microsoft.EntityFrameworkCore;

namespace Code_CloudSchool.Services;

public class StudentPasswordService : IUpdateStudentPassword
{

    private readonly AppDbContext _context;

    public StudentPasswordService( AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> UpdateStudentPassword(string studentNumber, StudentPasswordDTO studentPasswordDTO)
    {
        // Find the student by student number
        Student? student = await _context.Students.FirstOrDefaultAsync(s => s.StudentNumber == studentNumber);

        if (student == null)
        {
            return false; // Student not found
        }

        // Verify the old password
        bool isPasswordValid = BCrypt.Net.BCrypt.EnhancedVerify(studentPasswordDTO.OldPassword, student.Password);
        if (!isPasswordValid)
        {
            return false; // Incorrect old password
        }


        // Hash the new password
        string hashedNewPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(studentPasswordDTO.NewPassword, 13);

        student.Password = hashedNewPassword;
        _context.Students.Update(student);
        await _context.SaveChangesAsync();

        return true;
    }
}
