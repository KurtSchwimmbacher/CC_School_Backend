using System;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.Models;

namespace Code_CloudSchool.Services;

public class StudentAuthService : IStudentAuth
{
    public Task<string> EmailExists(string email)
    {
        throw new NotImplementedException();
    }

    public Task<string> GenerateEmailAddress(string studentNumber)
    {
        throw new NotImplementedException();
    }

    public Task<string> GenerateStudentNumber()
    {
        throw new NotImplementedException();
    }

    public Task<string> HashPassword(string password)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RegisterStudent(Student student)
    {
        throw new NotImplementedException();
    }
}
