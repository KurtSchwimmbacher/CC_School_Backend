using System;
using Code_CloudSchool.Data;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.Models;
using Microsoft.EntityFrameworkCore;

namespace Code_CloudSchool.Services;

public class StudentAuthService : IStudentAuth
{

    private readonly AppDbContext _context;

    public StudentAuthService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Student?> EmailExists(string email)
    {
        Student? studentFromDB = await _context.Students.FirstOrDefaultAsync(studentInDB => studentInDB.Email == email);

        return studentFromDB; //if null email not in use
    }

    public Task<string> GenerateEmailAddress(string studentNumber)
    {
        return Task.FromResult(studentNumber + "@codecloudschool.com");
    }

public async Task<string> GenerateStudentNumber(Student student)
{
    string studentNumber = "3";
    string year = DateTime.UtcNow.Year.ToString().Substring(2);

    // Ensure student.Id is populated
    if (student.Id == 0) 
    {
        await _context.Students.AddAsync(student);
        await _context.SaveChangesAsync(); // Save to get the assigned ID
    }

    string userID = student.Id.ToString();

    for (int i = 0; i < 4 - userID.Length; i++)
    {
        studentNumber += "0";
    }

    studentNumber += userID;

    return studentNumber;
}



    public Task<string> HashPassword(string password)
    {
        string HashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(password, 13);
        return Task.FromResult(HashedPassword);
    }

    public Task<bool> RegisterStudent(Student student)
    {
        Student? doesStudentExist = EmailExists(student.Email).Result;
        if (doesStudentExist != null)
        {
            return Task.FromResult(false);
        }

        // if student doesnt exist yet
        student.StudentNumber = GenerateStudentNumber(student).Result;
        student.Email = GenerateEmailAddress(student.StudentNumber).Result;
        student.Password = HashPassword(student.Password).Result;

        // adding user to DB
        _context.Students.Add(student);
        _context.SaveChanges();

        return Task.FromResult(true);
    }



    public Task<bool> ValidatePassword(Student student, string password)
    {
        bool isPasswordValid = BCrypt.Net.BCrypt.EnhancedVerify(password, student.Password);
        return Task.FromResult(isPasswordValid);
    }


    public Task<string> LoginStudent(string password, string email)
    {
        Student? studentFromDB = EmailExists(email).Result;

        // user doesnt exist means cant login
        if (studentFromDB == null)
        {
            return Task.FromResult("Student Doesn't Exist");
        }

        if (!ValidatePassword(studentFromDB, password).Result)
        {
            return Task.FromResult("Password is incorrect");
        }

        return Task.FromResult("Login Successful");
    }

}
