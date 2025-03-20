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

    public Task<string> GenerateStudentNumber(Student student)
    {
        string studentNumber = "3";
        string year = DateTime.UtcNow.Year.ToString().Substring(2);
        studentNumber += year;
        string userID = student.Id.ToString();

        for (int i = 0; i < 4 - userID.Length; i++)
        {
            studentNumber += "0";
        }

        studentNumber += userID;

        return Task.FromResult(studentNumber);

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
}
