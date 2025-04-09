using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Code_CloudSchool.Data;
using Code_CloudSchool.DTOs;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.Models;
using Microsoft.EntityFrameworkCore;

namespace Code_CloudSchool.Services;

// Service handling submission-related business logic
// Implements ISubmissionService for dependency injection
public class SubmissionService : ISubmissionService
{
    private readonly AppDBContext _context; // Database context for EF Core operations

    // Constructor with dependency injection
    public SubmissionService(AppDBContext context)
    {
        _context = context;
    }

    // Creates a new submission from DTO with validation
    public async Task<Submission> SubmitAssignment(CreateSubmissionDTO submissionDto)
    {
        // Verify assignment exists
        var assignment = await _context.Assignments
            .FirstOrDefaultAsync(a => a.Assignment_ID == submissionDto.AssignmentId) 
            ?? throw new ArgumentException("Assignment not found");

        // Verify student exists
        var student = await _context.Students
            .FirstOrDefaultAsync(s => s.Id == submissionDto.StudentId) 
            ?? throw new ArgumentException("Student not found");

        // Create new submission - Grade is auto-initialised by constructor
        var submission = new Submission
        {
            Assignment_ID = submissionDto.AssignmentId,
            Student_ID = submissionDto.StudentId,
            FilePath = submissionDto.FilePath,
            SubmissionDate = submissionDto.SubmissionDate,
            Assignment = assignment,  // Set navigation property
            Student = student        // Set navigation property
        };

        // Configure auto-created Grade
        submission.Grade.Score = 0; // Default ungraded state
        submission.Grade.Submission_ID = submission.Submission_ID; // Will update on save

        _context.Submissions.Add(submission);
        await _context.SaveChangesAsync();
        
        return submission;
    }
        
    // Legacy method for direct submission creation
    public async Task<Submission> SubmitAssignment(Submission submission)
    {
        _context.Submissions.Add(submission);
        await _context.SaveChangesAsync();
        return submission;
    }

    // Gets all submissions for a specific student with related data
    public async Task<List<Submission>> GetSubmissionsByStudent(int studentId)
    {
        return await _context.Submissions
            .Include(s => s.Assignment) // Eager load assignment details
            .Where(s => s.Student.Id == studentId)
            .ToListAsync();
    }

    // Gets single submission by ID with full related data
    public async Task<Submission> GetSubmissionById(int id)
    {
        return await _context.Submissions
            .Include(s => s.Assignment) // Load assignment
            .Include(s => s.Student)    // Load student
            .FirstOrDefaultAsync(s => s.Submission_ID == id);
    }

    // Updates submission from DTO (partial updates supported)
    public async Task<Submission> UpdateSubmission(UpdateSubmissionDTO submissionDto)
    {
        var submission = await _context.Submissions.FindAsync(submissionDto.SubmissionId);
        if (submission == null) return null;

        // Only update provided fields
        if (!string.IsNullOrEmpty(submissionDto.FilePath))
            submission.FilePath = submissionDto.FilePath;

        if (submissionDto.SubmissionDate.HasValue)
            submission.SubmissionDate = submissionDto.SubmissionDate.Value;

        await _context.SaveChangesAsync();
        return submission;
    }

    // Legacy method for direct submission updates
    public async Task<Submission> UpdateSubmission(Submission submission)
    {
        _context.Entry(submission).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return submission;
    }

    // Deletes submission by ID
    public async Task<bool> DeleteSubmission(int id)
    {
        var submission = await _context.Submissions.FindAsync(id);
        if (submission == null) return false;

        _context.Submissions.Remove(submission);
        await _context.SaveChangesAsync();
        return true;
    }

    // Gets all submissions for a specific assignment
    public async Task<List<Submission>> GetSubmissionsForAssignment(int assignmentId)
    {
        return await _context.Submissions
            .Include(s => s.Student) // Load student details
            .Where(s => s.Assignment.Assignment_ID == assignmentId)
            .ToListAsync();
    }

    // Checks if student has already submitted to prevent duplicates
    public async Task<bool> HasStudentSubmittedAssignment(int studentId, int assignmentId)
    {
        return await _context.Submissions
            .AnyAsync(s => s.Student.Id == studentId && 
                          s.Assignment.Assignment_ID == assignmentId);
    }
}