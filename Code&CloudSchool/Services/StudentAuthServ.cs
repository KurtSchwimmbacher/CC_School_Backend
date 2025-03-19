using System;
using Code_CloudSchool.Data;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.Models;

namespace Code_CloudSchool.Services;

public class StudentAuthServ : StudentAuthServices
{

    // dependency injection

    private readonly AppDbContext _context;

    // constructor 
    public StudentAuthServ(AppDbContext context)
    {
        _context = context;
    }

    public Task<string> HashPassword(string password)
    {
        string HashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(password, 13);
        return Task.FromResult(HashedPassword);
    }

    // public Task<string> GenerateStudentNumber()
    // {

    // }

}
