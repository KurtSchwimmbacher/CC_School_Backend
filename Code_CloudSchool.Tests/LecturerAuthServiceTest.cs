using System;
using Code_CloudSchool.Data;
using Code_CloudSchool.Models;
using Code_CloudSchool.Services;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace Code_CloudSchool.Tests;

public class LecturerAuthServiceTest
{

    private readonly LecturerAuthService _service;

    public LecturerAuthServiceTest()
    {
        // use an in-memory db for testing
        var options = new DbContextOptionsBuilder<AppDBContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new AppDBContext(options);

        // seed the in-memory database
        var lecturer = new LecturerReg
        {
            Name = "Test Lecturer",
            LecEmail = "TestLec@example.com",
            Password = "password123"
        };

        context.Lecturers.Add(lecturer);
        context.SaveChanges();


        // Initialize the LecturerAuthService with the in-memory context
        _service = new LecturerAuthService(context);
    }

    // Test Case 1: RegisterLecturer -> creates lecturer 
    // [Fact]
    // public async Task RegisterLecturer_CreatesLecturer_ReturnsTrue()
    // {
    //     // Arrange
    //     var lecturer = new LecturerReg
    //     {
    //         Name = "John Doe",
    //         LecEmail = "johndoe@example.com",
    //         Password = "password123"
    //     };

    //     // Act
    //     var result = await _service.RegisterLecturer(lecturer);

    //     // Assert
    //     Assert.True(result);
    // }

    // // Test Case 2: RegisterLecturer -> lecturer already exists
    // [Fact]
    // public async Task RegisterLecturer_LecturerExists_ReturnsFalse()
    // {
    //     // Arrange
    //     // already created lecturer in the db
    //     var lecturer = new LecturerReg
    //     {
    //         Name = "John Doe",
    //         LecEmail = "johndoe@example.com",
    //         Password = "password123"
    //     };

    //     // Act
    //     var result = await _service.RegisterLecturer(lecturer);
    //     // create the same lecturer again
    //     // this should return false since the email already exists
    //     var result2 = await _service.RegisterLecturer(lecturer);

    //     // Assert
    //     // email should already exist in the db
    //     Assert.False(result2);
    // }

    // Test Case 3: HashPassword -> hashes password correctly
    [Fact]
    public async Task HashPassword_HashesPassword_ReturnsHashedPassword()
    {
        // Arrange
        var password = "password123";

        // Act
        var hashedPassword = await _service.HashPassword(password);

        // Assert
        Assert.NotNull(hashedPassword);
        Assert.NotEqual(password, hashedPassword); // Ensure the hashed password is not the same as the original password
    }


    // Test Case 4: EmailExists -> email exists in the db
    [Fact]
    public async Task EmailExists_EmailExists_ReturnsLecturer()
    {
        // Arrange
        var lecturerTest = new LecturerReg
        {
            Name = "Test Lecturer",
            LecEmail = "TestLec@example.com",
            Password = "password123"
        };

        // Act 
        var lecturer = await _service.EmailExists(lecturerTest.LecEmail);

        // Assert
        Assert.NotNull(lecturer);
        Assert.Equal(lecturerTest.Name, lecturer.Name);

    }

    // Test Case 5: EmailExists -> email does not exist in the db
    [Fact]
    public async Task EmailExists_EmailDoesntExist_ReturnsNull()
    {
        // Asset
        string nonEmail = "doesnt exist";

        // Act
        var lecturer = await _service.EmailExists(nonEmail);

        // Assert
        Assert.Null(lecturer);
    }


    [Fact]
    public async Task GenerateEmailAddress_ReturnsCorrectFormat()
    {
        // Arrange
        var name = "JaneDoe";

        // Act
        var email = await _service.GenerateEmailAdress(name);

        // Assert
        Assert.Equal("JaneDoe@codecloudschool.com", email);
    }
    [Fact]
    public async Task ValidatePassword_CorrectPassword_ReturnsTrue()
    {
        // Arrange
        var password = "mysecret";
        var hashed = await _service.HashPassword(password);

        var lecturer = new LecturerReg
        {
            Name = "Validator",
            LecEmail = "validator@cc.com",
            Password = hashed
        };

        // Act
        var result = await _service.ValidatePassword(lecturer, password);

        // Assert
        Assert.True(result);
    }


    [Fact]
    public async Task ValidatePassword_WrongPassword_ReturnsFalse()
    {
        // Arrange
        var password = "correctpw";
        var wrongPassword = "wrongpw";

        var hashed = await _service.HashPassword(password);

        var lecturer = new LecturerReg
        {
            Name = "Validator",
            LecEmail = "validator@cc.com",
            Password = hashed
        };

        // Act
        var result = await _service.ValidatePassword(lecturer, wrongPassword);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task RegisterLecturer_NewLecturer_ReturnsLecturer()
    {
        // Arrange
        var lecturer = new LecturerReg
        {
            Name = "NewLecturer",
            LecEmail = "dummy@placeholder.com", // will be overwritten
            Password = "pass123"
        };

        // Act
        var result = await _service.RegisterLecturer(lecturer);

        // Assert
        Assert.NotNull(result);
        Assert.Contains("@codecloudschool.com", result.LecEmail);
    }

    [Fact]
    public async Task RegisterLecturer_DuplicateEmail_ReturnsNull()
    {
        // Arrange
        var lecturer = new LecturerReg
        {
            Name = "Test Lecturer",
            LecEmail = "TestLec@example.com",
            Password = "password123"
        };

        // Act
        var result = await _service.RegisterLecturer(lecturer);

        // Assert
        Assert.Null(result);
    }
    [Fact]
    public async Task LoginLecturer_ValidCredentials_ReturnsLecturer()
    {
        // Arrange
        var password = "loginpass";
        var hashed = await _service.HashPassword(password);

        var lecturer = new LecturerReg
        {
            Name = "LoginTest",
            LecEmail = "logintest@example.com",
            Password = hashed
        };

        var context = new AppDBContext(new DbContextOptionsBuilder<AppDBContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);

        context.Lecturers.Add(lecturer);
        await context.SaveChangesAsync();

        var loginService = new LecturerAuthService(context);

        // Act
        var result = await loginService.LoginLecturer(lecturer.LecEmail, password);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(lecturer.Name, result.Name);
    }

    [Fact]
    public async Task LoginLecturer_InvalidPassword_ReturnsNull()
    {
        // Arrange
        var password = "rightpw";
        var wrongPassword = "wrongpw";
        var hashed = await _service.HashPassword(password);

        var lecturer = new LecturerReg
        {
            Name = "LoginTest",
            LecEmail = "logintest@example.com",
            Password = hashed
        };

        var context = new AppDBContext(new DbContextOptionsBuilder<AppDBContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);

        context.Lecturers.Add(lecturer);
        await context.SaveChangesAsync();

        var loginService = new LecturerAuthService(context);

        // Act
        var result = await loginService.LoginLecturer(lecturer.LecEmail, wrongPassword);

        // Assert
        Assert.Null(result);
    }


}
