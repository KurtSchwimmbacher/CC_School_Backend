using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Code_CloudSchool.Data;
using Code_CloudSchool.Models;
using Code_CloudSchool.Services;

public class AssignmentServiceTests
{
    private readonly AssignmentService _service;
    private readonly AppDBContext _context;

    public AssignmentServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDBContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new AppDBContext(options);

        // Seed some assignments for testing
        _context.Assignments.AddRange(
            new Assignment { Assignment_ID = 1, LecturerId = 10, CourseId = 100, Title = "Assignment 1", DueDate = DateTime.UtcNow.AddDays(5) },
            new Assignment { Assignment_ID = 2, LecturerId = 10, CourseId = 200, Title = "Assignment 2", DueDate = DateTime.UtcNow.AddDays(3) },
            new Assignment { Assignment_ID = 3, LecturerId = 20, CourseId = 100, Title = "Assignment 3", DueDate = DateTime.UtcNow.AddDays(-1) }
        );
        _context.SaveChanges();

        _service = new AssignmentService(_context);
    }

    [Fact]
    public async Task CreateAssignment_AddsAssignmentSuccessfully()
    {
        var newAssignment = new Assignment
        {
            LecturerId = 30,
            CourseId = 300,
            Title = "New Assignment",
            DueDate = DateTime.UtcNow.AddDays(10)
        };

        var result = await _service.CreateAssignment(newAssignment);

        Assert.NotNull(result);
        Assert.True(result.Assignment_ID > 0);
        Assert.Equal("New Assignment", result.Title);

        var dbAssignment = await _context.Assignments.FindAsync(result.Assignment_ID);
        Assert.NotNull(dbAssignment);
    }


    [Fact]
    public async Task GetAssignmentById_ExistingId_ReturnsAssignment()
    {
        var assignment = await _service.GetAssignmentById(1);
        Assert.NotNull(assignment);
        Assert.Equal(1, assignment.Assignment_ID);
    }

    [Fact]
    public async Task GetAssignmentById_NonExistingId_ThrowsKeyNotFoundException()
    {
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetAssignmentById(999));
    }


    [Fact]
    public async Task UpdateAssignment_UpdatesSuccessfully()
    {
        var assignment = await _context.Assignments.FindAsync(1);
        assignment.Title = "Updated Title";

        var result = await _service.UpdateAssignment(assignment);

        Assert.Equal("Updated Title", result.Title);

        var updated = await _context.Assignments.FindAsync(1);
        Assert.Equal("Updated Title", updated.Title);
    }

    [Fact]
    public async Task DeleteAssignment_ExistingId_DeletesAndReturnsTrue()
    {
        var success = await _service.DeleteAssignment(1);
        Assert.True(success);

        var deleted = await _context.Assignments.FindAsync(1);
        Assert.Null(deleted);
    }

    [Fact]
    public async Task DeleteAssignment_NonExistingId_ReturnsFalse()
    {
        var success = await _service.DeleteAssignment(999);
        Assert.False(success);
    }


    [Fact]
    public async Task GetAssignmentsByCourseId_ReturnsCorrectAssignments()
    {
        var assignments = await _service.GetAssignmentsByCourseId(100);
        Assert.Equal(2, assignments.Count);
        Assert.All(assignments, a => Assert.Equal(100, a.CourseId));
    }



    [Fact]
    public async Task GetAllAssignments_ReturnsAllAssignments()
    {
        var assignments = await _service.GetAllAssignments();
        Assert.Equal(3, assignments.Count);
    }


    [Fact]
    public async Task GetAssignmentsByFilter_FiltersByDueDate()
    {
        var dueDate = DateTime.UtcNow.AddDays(3);
        var results = await _service.GetAssignmentsByFilter(dueDate, null);

        Assert.All(results, a => Assert.True(a.DueDate <= dueDate));
    }

    [Fact]
    public async Task GetAssignmentsByFilter_FiltersByIsCompleted()
    {
        // Seed an assignment with submissions for testing
        var assignmentWithSubmission = new Assignment
        {
            LecturerId = 50,
            CourseId = 500,
            Title = "Completed Assignment",
            DueDate = DateTime.UtcNow,
            Submissions = new List<Submission>
        {
            new Submission { Grade = new Grade { Score = 85 } }
        }
        };

        _context.Assignments.Add(assignmentWithSubmission);
        await _context.SaveChangesAsync();

        var completed = await _service.GetAssignmentsByFilter(null, true);
        Assert.Contains(completed, a => a.Title == "Completed Assignment");

        var notCompleted = await _service.GetAssignmentsByFilter(null, false);
        Assert.DoesNotContain(notCompleted, a => a.Title == "Completed Assignment");
    }


}
