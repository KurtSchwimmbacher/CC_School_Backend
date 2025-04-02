using System;
using Xunit;
using Moq;
using Code_CloudSchool.Services;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.Data;
using Microsoft.EntityFrameworkCore;
using Code_CloudSchool.Models;
using Code_CloudSchool.DTOs;
using System.Linq;
using System.Linq.Expressions;
using NuGet.ContentModel;

namespace Code_CloudSchool.Tests;

public class StudentPasswordServiceTest
{

    private readonly StudentPasswordService _service;

    public StudentPasswordServiceTest()
    {
        // Use an in-memory database for testing
        var options = new DbContextOptionsBuilder<AppDBContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        var context = new AppDBContext(options);


        // Hash the password before seeding the database
        var hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword("oldPass", 13);

        // Seed the in-memory database
        context.Students.Add(new Student { StudentNumber = "12345", Password = hashedPassword, Gender = "Male" });
        context.SaveChanges();

        // Pass the in-memory context to the service
        _service = new StudentPasswordService(context);
    }




    // Test Case 1: Returns false when student is not found
    [Fact]
    public async Task UpdateStudentPassword_StudentNotFound_ReturnsFalse()
    {
        // Arrange
        var studentNumber = "3250000";
        var studentPasswordDTO = new StudentPasswordDTO
        {
            OldPassword = "oldPass",
            NewPassword = "newPass"
        };


        // Act
        var result = await _service.UpdateStudentPassword(studentNumber, studentPasswordDTO);

        // Assert
        Assert.False(result);
    }



    // Test Case 2: Returns false when old password is incorrect
    [Fact]
    public async Task UpdateStudentPassword_PasswordMismatch_ReturnsFalse()
    {
        // Arrange 
        var studentNumber = "12345";
        var studentPasswordDTO = new StudentPasswordDTO
        {
            OldPassword = "hashedPassword",
            NewPassword = "newPass"
        };

        // Act
        var result = await _service.UpdateStudentPassword(studentNumber, studentPasswordDTO);

        // Assert
        Assert.False(result);

    }


    // Test Case 3: Returns true and update the password when old password is correct
    [Fact]
    public async Task UpdateStudentPassword_PasswordMatch_ReturnsTrue()
    {
        // Arrange 
        var studentNumber = "12345";
        var studentPasswordDTO = new StudentPasswordDTO
        {
            OldPassword = "oldPass",
            NewPassword = "newPass"
        };

        // Act
        var result = await _service.UpdateStudentPassword(studentNumber, studentPasswordDTO);

        // Assert
        Assert.True(result);

    }


}
