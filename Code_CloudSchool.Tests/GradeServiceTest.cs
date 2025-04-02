using System;
using Code_CloudSchool.Data;
using Code_CloudSchool.DTOs;
using Code_CloudSchool.Models;
using Code_CloudSchool.Services;
using Microsoft.EntityFrameworkCore;

namespace Code_CloudSchool.Tests;

public class GradeServiceTest
{

    private readonly GradeService _service;

    public GradeServiceTest()
    {
        // Use an in-memory database for testing
        var options = new DbContextOptionsBuilder<AppDBContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new AppDBContext(options);

        // Seed the in-memory database
        context.Submissions.Add(new Submission
        {
            Submission_ID = 1,
            FilePath = "test-file-path.txt",
            Assignment = new Assignment
            {
                Assignment_ID = 1,
                Title = "Test Assignment",
                Description = "This is a test assignment",
            },
            Grade = new Grade
            {
                Score = 0,
                Feedback = "None"
            }
        });

        context.Submissions.Add(new Submission
        {
            Submission_ID = 50,
            FilePath = "test-file-path.txt",
            // Assignment = new Assignment
            // {
            //     Assignment_ID = 10,
            //     Title = "Test Assignment",
            //     Description = "This is a test assignment",
            // }
        });

        context.SaveChanges();


        // Initialize the GradeService with the in-memory context
        _service = new GradeService(context);
    }


    // Test Case 1: GradeSubmission -> Should throw ArgumentException if submission doesn't exist

    [Fact]
    public async Task GradeSubmission_SubmissionDoesntExist_ThrowsException()
    {
        // Arrange
        var gradeDTO = new CreateGradeDto
        {
            // non existent submission ID
            SubmissionId = 999,
            Score = 85,
            Feedback = "Good job!"
        };

        // Act
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _service.GradeSubmission(gradeDTO));

        // Assert
        Assert.Equal("Submission with ID 999 not found", exception.Message);
    }

    // Test Case 2: GradeSubmission -> Should create and return a Grade when submission exists
    [Fact]
    public async Task GradeSubmission_SubmissionExists_CreatesGrade()
    {
        // Arrange
        var gradeDTO = new CreateGradeDto
        {
            // existing ID
            SubmissionId = 1,
            Score = 85,
            Feedback = "Good job!"
        };

        // Act
        var result = await _service.GradeSubmission(gradeDTO);

        // Assert
        Assert.NotNull(result);
        // just to check if the created grade matches its expected val :)
        Assert.Equal(gradeDTO.Score, result.Score);
    }


    // Test Case 3: GetGradeBySubmissionId -> Should return a grade when submission exists
    [Fact]
    public async Task GetGradeBySubmissionId_SubmissionExists_ReturnsGrade()
    {
        // Arrange 
        var gradeDTO = new CreateGradeDto
        {
            SubmissionId = 1,
            Score = 0,
            Feedback = "Good job!"
        };

        // Act
        var fetchedGrade = await _service.GetGradeBySubmissionId(gradeDTO.SubmissionId);

        // Assert
        Assert.NotNull(fetchedGrade);
        Assert.Equal(gradeDTO.Score, fetchedGrade.Score);
    }


    // Test Case 4: GetGradeBySubmissionId -> Should return null when no grade exists for the submission
    [Fact]
    public async Task GetGradeBySubmissionId_NoGradeExists_ReturnsNull()
    {
        // Arrange
        var gradeDTO = new CreateGradeDto
        {
            SubmissionId = 10
        };

        // Act
        var fetchedGrade = await _service.GetGradeBySubmissionId(gradeDTO.SubmissionId);

        // Assert
        Assert.Null(fetchedGrade);
    }
}
