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

public class SubmissionService : ISubmissionService
{
    private readonly AppDBContext _context;

    public SubmissionService(AppDBContext context)
    {
        _context = context;
    }

    // Submit a new assignment using DTO
    public async Task<Submission> SubmitAssignment(CreateSubmissionDTO submissionDto)
    {
        // Load required entities
        var assignment = await _context.Assignments
            .FirstOrDefaultAsync(a => a.Assignment_ID == submissionDto.AssignmentId)
            ?? throw new ArgumentException("Assignment not found");

        var student = await _context.Students
            .FirstOrDefaultAsync(s => s.StudentNumber == submissionDto.StudentId)
            ?? throw new ArgumentException("Student not found");

        // Create submission - Grade will be auto-initialized by constructor
        var submission = new Submission
        {
            Assignment_ID = submissionDto.AssignmentId,
            Student_ID = submissionDto.StudentId,
            FilePath = submissionDto.FilePath,
            SubmissionDate = submissionDto.SubmissionDate,
            Assignment = assignment,
            Student = student
        };

        // Configure the auto-created Grade
        submission.Grade.Score = 0; // Default ungraded value
        submission.Grade.Submission_ID = submission.Submission_ID; // Will be set on save

        _context.Submissions.Add(submission);
        await _context.SaveChangesAsync();

        return submission;
    }

    // Submit a new assignment using existing model (legacy support)
    public async Task<Submission> SubmitAssignment(Submission submission)
    {
        _context.Submissions.Add(submission);
        await _context.SaveChangesAsync();
        return submission;
    }

    // Get all submissions for a specific student with related data
    public async Task<List<Submission>> GetSubmissionsByStudent(string studentId)
    {
        return await _context.Submissions
            .Include(s => s.Assignment)
            .Where(s => s.Student.StudentNumber == studentId)
            .ToListAsync();
    }

    // Get a specific submission by ID with related data
    public async Task<Submission> GetSubmissionById(int id)
    {
        var submission = await _context.Submissions
            .Include(s => s.Assignment)
            .Include(s => s.Student)
            .FirstOrDefaultAsync(s => s.Submission_ID == id);

        if (submission == null)
        {
            throw new ArgumentException("Submission not found");
        }

        return submission;
    }

    // Update an existing submission using DTO
    public async Task<Submission> UpdateSubmission(UpdateSubmissionDTO submissionDto)
    {
        var submission = await _context.Submissions.FindAsync(submissionDto.SubmissionId);
        if (submission == null)
        {
            throw new ArgumentException("Submission not found");
        }

        if (!string.IsNullOrEmpty(submissionDto.FilePath))
            submission.FilePath = submissionDto.FilePath;

        if (submissionDto.SubmissionDate.HasValue)
            submission.SubmissionDate = submissionDto.SubmissionDate.Value;

        await _context.SaveChangesAsync();
        return submission;
    }

    // Update an existing submission using model (legacy support)
    public async Task<Submission> UpdateSubmission(Submission submission)
    {
        _context.Entry(submission).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return submission;
    }

    // Delete a submission by its ID
    public async Task<bool> DeleteSubmission(int id)
    {
        var submission = await _context.Submissions.FindAsync(id);
        if (submission == null) return false;

        _context.Submissions.Remove(submission);
        await _context.SaveChangesAsync();
        return true;
    }

    // Get all submissions for a specific assignment with related data
    public async Task<List<Submission>> GetSubmissionsForAssignment(int assignmentId)
    {
        return await _context.Submissions
            .Include(s => s.Student)
            .Where(s => s.Assignment.Assignment_ID == assignmentId)
            .ToListAsync();
    }

    // Check if a student has already submitted a specific assignment
    public async Task<bool> HasStudentSubmittedAssignment(string studentId, int assignmentId)
    {
        return await _context.Submissions
            .AnyAsync(s => s.Student.StudentNumber == studentId &&
                          s.Assignment.Assignment_ID == assignmentId);
    }
}
