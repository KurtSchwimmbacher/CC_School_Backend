using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Code_CloudSchool.Data;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.Models;
using Code_CloudSchool.DTOs; // DTO support for grade creation
using Microsoft.EntityFrameworkCore;

namespace Code_CloudSchool.Services;

// This service handles all business logic related to grades.
// It implements the IGradeService interface to ensure consistency and abstraction.
public class GradeService : IGradeService
{
    private readonly AppDBContext _context;

    // Constructor injection of the database context
    public GradeService(AppDBContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates and saves a new grade entry for a submission.
    /// Validates that the submission exists before grading.
    /// </summary>
    /// <param name="gradeDto">DTO containing submission ID, score, and optional feedback</param>
    /// <returns>The newly created Grade entity</returns>
    /// <exception cref="ArgumentException">Thrown if submission does not exist</exception>
    public async Task<Grade> GradeSubmission(CreateGradeDto gradeDto)
    {
        // Retrieve the submission from the database, including related assignment info
        var submission = await _context.Submissions
            .Include(s => s.Assignment)
            .FirstOrDefaultAsync(s => s.Submission_ID == gradeDto.Submission_ID);
        
        // Return a client-safe error if the submission doesn't exist
        if (submission == null)
        {
            throw new ArgumentException($"Submission with ID {gradeDto.Submission_ID} not found");
        }

        // Create a new Grade object and populate it with values from the DTO
        var grade = new Grade
        {
            Submission_ID = gradeDto.Submission_ID,
            Score = gradeDto.Score,
            Feedback = gradeDto.Feedback,
            Submission = submission // Link to the navigation property for EF
        };

        // Save the grade to the database
        _context.Grades.Add(grade);
        await _context.SaveChangesAsync();

        return grade;
    }

    /// <summary>
    /// Retrieves a grade associated with a specific submission ID.
    /// </summary>
    public async Task<Grade> GetGradeBySubmissionId(int submissionId)
    {
        return await _context.Grades
            .FirstOrDefaultAsync(g => g.Submission_ID == submissionId);
    }

    /// <summary>
    /// Updates a grade record by marking its state as modified.
    /// </summary>
    public async Task<Grade> UpdateGrade(Grade grade)
    {
        // Mark the entity as modified so EF tracks it as an update
        _context.Entry(grade).State = EntityState.Modified;

        await _context.SaveChangesAsync();
        return grade;
    }

    /// <summary>
    /// Deletes a grade by its unique ID.
    /// </summary>
    /// <param name="id">ID of the grade to delete</param>
    /// <returns>True if successful, false if the grade was not found</returns>
    public async Task<bool> DeleteGrade(int id)
    {
        var grade = await _context.Grades.FindAsync(id);
        if (grade == null) return false;

        _context.Grades.Remove(grade);
        await _context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Retrieves all grades related to a specific assignment.
    /// This joins each grade with its related submission to access assignment details.
    /// </summary>
    public async Task<List<Grade>> GetGradesForAssignment(int assignmentId)
    {
        return await _context.Grades
            .Include(g => g.Submission) // Include Submission to access assignment ID
            .Where(g => g.Submission.Assignment_ID == assignmentId)
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves all grades received by a student using their student number.
    /// Joins grades → submissions → students to perform the match.
    /// </summary>
    public async Task<List<Grade>> GetGradesByStudent(string studentNumber)
    {
        return await _context.Grades
            .Include(g => g.Submission)
                .ThenInclude(s => s.Student) // Include the student relationship for filtering
            .Where(g => g.Submission.Student.StudentNumber == studentNumber)
            .ToListAsync();
    }

    /// <summary>
    /// Checks if a submission has already been graded.
    /// Returns true if a grade record exists for the submission ID.
    /// </summary>
    public async Task<bool> HasSubmissionBeenGraded(int submissionId)
    {
        return await _context.Grades
            .AnyAsync(g => g.Submission_ID == submissionId); 
    }
}
