using System;
using Code_CloudSchool.Data;
using Code_CloudSchool.DTOs;
using Code_CloudSchool.Models;
using Code_CloudSchool.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

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
        var student = new Student
        {
            StudentNumber = "12345",
            Name = "test",
            Gender = "Male"
        };

        var assignment = new Assignment
        {
            Assignment_ID = 1,
            Title = "Test Assignment",
            Description = "This is a test assignment"
        };

        var submission = new Submission
        {
            Submission_ID = 1,
            FilePath = "test-file-path.txt",
            Student = student, // Properly associate the student
            Assignment = assignment, // Properly associate the assignment
            Grade = new Grade
            {
                Score = 0,
                Feedback = "None"
            }
        };

        context.Students.Add(student); // Add the student to the database
        context.Assignments.Add(assignment); // Add the assignment to the database
        context.Submissions.Add(submission); // Add the submission to the database

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


    // Test Case 5: UpdateGrade -> update and return the modified grade
    [Fact]
    public async Task UpdateGrade_UpdateSuccessful_ReturnsGrade()
    {
        // Arrange
        var gradeDTO = new CreateGradeDto
        {
            SubmissionId = 1,
            Score = 70
        };

        // Retrieve existing grade
        var oldGrade = await _service.GetGradeBySubmissionId(gradeDTO.SubmissionId);
        Assert.NotNull(oldGrade);

        // update the grade
        oldGrade.Score = gradeDTO.Score;

        // Act
        var grade = await _service.UpdateGrade(oldGrade);

        // Assert 
        Assert.NotNull(grade);
        Assert.Equal(gradeDTO.Score, grade.Score);
    }


    // Test Case 6: DeleteGrade -> should delete the grade and return true
    [Fact]
    public async Task DeleteGrade_GradeExists_ReturnsTrue()
    {
        // Arrange
        var gradeId = 1;

        // Act
        var result = await _service.DeleteGrade(gradeId);

        // Assert
        Assert.True(result);
    }

    // Test Case 7: DeleteGrade -> should return false when grade doesnt exist
    [Fact]
    public async Task DeleteGrade_GradeDoesntExist_ReturnsFalse()
    {
        // Arrange
        var gradeId = 999; // Non-existent grade ID

        // Act
        var result = await _service.DeleteGrade(gradeId);

        // Assert
        Assert.False(result);
    }

    // Test Case 8: GetGradesForAssignment -> should return a list of grades for the assignment
    [Fact]
    public async Task GetGradesForAssignment_AssignmentExists_ReturnsGrades()
    {
        // Arrange 
        // should be existing
        var assignmentId = 1;

        // Act
        var grades = await _service.GetGradesForAssignment(assignmentId);

        // Assert 
        Assert.NotNull(grades);

    }

    // Test Case 9: GetGradesForAssignment -> AssignmentDoesntExist_ReturnsEmptyList
    [Fact]
    public async Task GetGradesForAssignment_AssignmentDoesntExist_ReturnsEmptyList()
    {
        // Arrange 
        var assignmentId = 999; // Non-existent assignment ID

        // Act
        var grades = await _service.GetGradesForAssignment(assignmentId);

        // Assert
        Assert.NotNull(grades);
        Assert.Empty(grades);
    }

    // Test Case 10: GetGradesByStudent -> should return a list of grades for the student
    [Fact]
    public async Task GetGradesByStudent_StudentExists_ReturnsGrades()
    {
        // Arrange
        var studentId = "12345"; //existing student ID

        // Act
        var grades = await _service.GetGradesByStudent(studentId);

        // Assert
        Assert.NotNull(grades);
        Assert.NotEmpty(grades);
    }

    // Test Case 11: HasSubmissionBeenGraded -> should return true if the submission has been graded
    [Fact]
    public async Task HasSubmissionBeenGraded_SubmissionGraded_ReturnsTrue()
    {
        // Arrange
        var submissionId = 1;

        // Act
        var result = await _service.HasSubmissionBeenGraded(submissionId);

        // Assert
        Assert.True(result);
    }

    // Test Case 12: HasSubmissionBeenGraded -> should return false if the submission has not been graded
    [Fact]
    public async Task HasSubmissionBeenGraded_SubmissionNotGraded_ReturnsFalse()
    {
        // Arrange
        var submissionId = 999; // Non-existent submission ID

        // Act
        var result = await _service.HasSubmissionBeenGraded(submissionId);

        // Assert
        Assert.False(result);
    }
}
