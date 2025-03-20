using System;
using Code_CloudSchool.Models;

namespace Code_CloudSchool.Interfaces;

public interface IStudentAuth
{
    // returns a hashed password as a string
    public Task<string> HashPassword(string password);

    // returns a student number as a string
    public Task<string> GenerateStudentNumber(Student student);

    // returns email address based on student number
    public Task<string> GenerateEmailAddress(string studentNumber);

    // returns true if student was registered successfully
    public Task<bool> RegisterStudent(Student student);

    // returns a student object if email address exists
    public Task<Student?> EmailExists(string email);

    public Task<string> LoginStudent(string password, string email);

    public Task<bool> ValidatePassword(Student student, string password);    

}
