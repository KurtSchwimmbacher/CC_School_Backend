using System;
using Code_CloudSchool.Models;

namespace Code_CloudSchool.Interfaces;

public interface IStudentAuth
{
    // returns a hashed password as a string
    public Task<string> HashPassword(string password);

    // returns a student number as a string
    public Task<string> GenerateStudentNumber();

    // returns email address based on student number
    public Task<string> GenerateEmailAddress(string studentNumber);

    // returns true if student was registered successfully
    public Task<bool> RegisterStudent(Student student);

    // returns a student object if email address exists
    public Task<string> EmailExists(string email);

}
