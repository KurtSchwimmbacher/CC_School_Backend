using System;
using Code_CloudSchool.Data;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.Models;
using Code_CloudSchool.Services;
using Microsoft.EntityFrameworkCore;

namespace Code_CloudSchool.Tests;

public class StudentAuthServiceTests
{
    private readonly StudentAuthService _service;

    public StudentAuthServiceTests()
    {
                // use an in-memory db for testing
        var options = new DbContextOptionsBuilder<AppDBContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new AppDBContext(options);

        // seed the in-memory database
        var student = new Student
        {
            StudentNumber = "3250000",
            Name = "test student",
            LastName = "test last name",
            Gender = "Male",
            Email = "3250000@codecloudschool.com"

        };

        context.Students.Add(student);
        context.SaveChanges();


        // Initialize the LecturerAuthService with the in-memory context
        _service = new StudentAuthService(context);
    }


    // Test Case 1: EmailExists -> email exists in DB
    [Fact]
    public async Task EmailExists_EmailExists_ReturnsStudent()
    {
        // Arrange
        // use existing email in DB
        string testEmail = "3250000@codecloudschool.com";

        // Act
        var Student = await _service.EmailExists(testEmail);

        // Assert
        Assert.NotNull(Student);
        Assert.Equal(testEmail, Student.Email);

    }

    // Test Case 2: EmailExists -> email does not exist in DB
    [Fact]
    public async Task EmailExists_EmailNotFound_ReturnsNull()
    {
        // Arrange
        // non existent email
        string nonEmail = "noEmail@ccSchool.com";

        // Act
        var Student = await _service.EmailExists(nonEmail);

        // Assert
        Assert.Null(Student);
    }

    // Test Case 3: GenerateStudentNumber -> generates student number
    [Fact]
    public async Task GenerateStudentNumber_Success_ReturnsStudentNumber()
    {
        // Arrange
        Student student = new Student
        {
            Id = 5,
            Name = "Kurt",
            Gender = "Male"
        };

        string expectedStudentNumber = "3250005";

        // Act
        var studentNumber = await _service.GenerateStudentNumber(student);

        // Assert
        Assert.NotNull(studentNumber);
        Assert.Equal(expectedStudentNumber, studentNumber);
    }


    // Test Case 4: GenerateEmailAddress -> generates email address
    [Fact]
    public async Task GenerateEmailAddress_Successful_ReturnsEmail()
    {
        // Arrange
        string StudentNumber = "3250005";
        string expectedEmail = "3250005@codecloudschool.com";

        // Act
        var email = await _service.GenerateEmailAddress(StudentNumber);

        // Assert
        Assert.NotNull(email);
        Assert.Equal(expectedEmail, email);
    }

    // Test Case 5: HashPassword -> hashes password correctly
    [Fact]
    public async Task HashPassword_Successful_ReturnsHashedPassword()
    {
        // Arrange
        string password = "password123";

        // Act
        var hashedPassword = await _service.HashPassword(password);

        // Assert
        Assert.NotNull(hashedPassword);
        // Ensure hashed password is not the same as the original
        Assert.NotEqual(password, hashedPassword); 
    }

    // Test Case 6: RegisterStudent -> student is registered successfully
    [Fact] 
    public async Task RegisterStudent_Successful_ReturnsTrue()
    {
        // Arrange 
        var student = new Student
        {
            Name = "John",
            LastName = "Doe",
            Gender = "Male",
            Email = "john.doe@codecloudschool.com"
        };

        // Act
        var result = await _service.RegisterStudent(student);

        // Assert
        Assert.True(result);
    }

}
