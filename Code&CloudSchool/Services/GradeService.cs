using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Code_CloudSchool.Data;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.Models;
using Code_CloudSchool.DTOs; // Added for DTO support
using Microsoft.EntityFrameworkCore;

namespace Code_CloudSchool.Services;

public class GradeService : IGradeService
{
    private readonly AppDBContext _context;

    public GradeService(AppDBContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Grades a submission by first validating the submission exists,
    /// then creating and saving the grade record
    /// </summary>
    /// <param name="gradeDto">Contains submission ID, score, and optional feedback</param>
    /// <returns>The created Grade entity</returns>
    /// <exception cref="ArgumentException">Thrown if submission doesn't exist</exception>
    public async Task<Grade> GradeSubmission(CreateGradeDto gradeDto)
    {
        // First validate the submission exists
        var submission = await _context.Submissions
            .Include(s => s.Assignment) // Include assignment data if needed
            .FirstOrDefaultAsync(s => s.Submission_ID == gradeDto.SubmissionId);
        
        if (submission == null)
        {
            throw new ArgumentException($"Submission with ID {gradeDto.SubmissionId} not found");
        }

        // Create new Grade entity from DTO
        var grade = new Grade
        {
            Submission_ID = gradeDto.SubmissionId,
            Score = gradeDto.Score,
            Feedback = gradeDto.Feedback,
            Submission = submission // Set navigation property
        };

        _context.Grades.Add(grade);
        await _context.SaveChangesAsync();
        return grade;
    }

    // Get a grade by its submission ID.
    public async Task<Grade> GetGradeBySubmissionId(int submissionId)
    {
        return await _context.Grades
            .FirstOrDefaultAsync(g => g.Submission_ID == submissionId);
    }

    // Update an existing grade.
    public async Task<Grade> UpdateGrade(Grade grade)
    {
        _context.Entry(grade).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return grade;
    }

    // Delete a grade by its ID.
    public async Task<bool> DeleteGrade(int id)
    {
        var grade = await _context.Grades.FindAsync(id);
        if (grade == null) return false;

        _context.Grades.Remove(grade);
        await _context.SaveChangesAsync();
        return true;
    }

    // Get all grades for a specific assignment.
    public async Task<List<Grade>> GetGradesForAssignment(int assignmentId)
    {
        return await _context.Grades
            .Include(g => g.Submission)
            .Where(g => g.Submission.Assignment_ID == assignmentId)
            .ToListAsync();
    }

    // Get all grades for a specific student.
    public async Task<List<Grade>> GetGradesByStudent(int studentId)
    {
        return await _context.Grades
            .Include(g => g.Submission)
            .Where(g => g.Submission.Student.Id == studentId)
            .ToListAsync();
    }

    // Check if a submission has been graded.
    public async Task<bool> HasSubmissionBeenGraded(int submissionId)
    {
        return await _context.Grades
            .AnyAsync(g => g.Submission_ID == submissionId); 
    }
}