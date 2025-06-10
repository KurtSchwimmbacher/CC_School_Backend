using System;
using System.Threading.Tasks;
using Code_CloudSchool.Data;
using Code_CloudSchool.Models;
using Code_CloudSchool.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Code_CloudSchool.Tests;

public class StudentAuthServiceTests
{
    private readonly StudentAuthService _service;
    private readonly AppDBContext _context;

    public StudentAuthServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDBContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new AppDBContext(options);

        // Seed a student
        var student = new Student
        {
            UserId = 1,
            StudentNumber = "3250001",
            Name = "Test",
            LastName = "Student",
            Gender = "Male",
            Email = "3250001@codecloudschool.com",
            Password = BCrypt.Net.BCrypt.EnhancedHashPassword("correct_password", 13)
        };

        _context.Students.Add(student);
        _context.SaveChanges();

        _service = new StudentAuthService(_context);
    }

    [Fact]
    public async Task EmailExists_EmailExists_ReturnsStudent()
    {
        var result = await _service.EmailExists("3250001@codecloudschool.com");
        Assert.NotNull(result);
        Assert.Equal("3250001@codecloudschool.com", result.Email);
    }

    [Fact]
    public async Task EmailExists_EmailNotFound_ReturnsNull()
    {
        var result = await _service.EmailExists("noEmail@ccSchool.com");
        Assert.Null(result);
    }

    [Fact]
    public async Task GenerateStudentNumber_Success_ReturnsStudentNumber()
    {
        var student = new Student { UserId = 5, Gender="Male" };
        var expected = "3250005";

        var result = await _service.GenerateStudentNumber(student);

        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task GenerateEmailAddress_Successful_ReturnsEmail()
    {
        var studentNumber = "3250005";
        var expected = "3250005@codecloudschool.com";

        var result = await _service.GenerateEmailAddress(studentNumber);

        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task HashPassword_Successful_ReturnsHashedPassword()
    {
        var password = "password123";

        var result = await _service.HashPassword(password);

        Assert.NotNull(result);
        Assert.NotEqual(password, result);
    }

    [Fact]
    public async Task RegisterStudent_EmailAlreadyExists_ReturnsNull()
    {
        var existingStudent = new Student
        {
            Email = "3250001@codecloudschool.com",
            Password = "password",
            Gender = "Male",
        };

        var result = await _service.RegisterStudent(existingStudent);

        Assert.Null(result);
    }

    [Fact]
    public async Task RegisterStudent_NewStudent_ReturnsStudentWithGeneratedFields()
    {
        var newStudent = new Student
        {
            Name = "New",
            LastName = "Student",
            Gender = "Female",
            Email = "newstudent@example.com",  // This will be overridden by RegisterStudent
            Password = "new_password"
        };

        var result = await _service.RegisterStudent(newStudent);

        Assert.NotNull(result);
        Assert.NotNull(result.StudentNumber);
        Assert.EndsWith("@codecloudschool.com", result.Email);
        Assert.NotEqual("new_password", result.Password); // Password hashed
    }

    [Fact]
    public async Task ValidatePassword_CorrectPassword_ReturnsTrue()
    {
        var student = await _service.EmailExists("3250001@codecloudschool.com");
        var isValid = await _service.ValidatePassword(student!, "correct_password");

        Assert.True(isValid);
    }

    [Fact]
    public async Task ValidatePassword_IncorrectPassword_ReturnsFalse()
    {
        var student = await _service.EmailExists("3250001@codecloudschool.com");
        var isValid = await _service.ValidatePassword(student!, "wrong_password");

        Assert.False(isValid);
    }

    [Fact]
    public async Task LoginStudent_NonExistentEmail_ReturnsNull()
    {
        var result = await _service.LoginStudent("any_password", "noemail@example.com");
        Assert.Null(result);
    }

    [Fact]
    public async Task LoginStudent_WrongPassword_ReturnsNull()
    {
        var email = "3250001@codecloudschool.com";
        var result = await _service.LoginStudent("wrong_password", email);

        Assert.Null(result);
    }

    [Fact]
    public async Task LoginStudent_CorrectCredentials_ReturnsStudent()
    {
        var email = "3250001@codecloudschool.com";
        var password = "correct_password";

        var result = await _service.LoginStudent(password, email);

        Assert.NotNull(result);
        Assert.Equal(email, result!.Email);
    }
}
