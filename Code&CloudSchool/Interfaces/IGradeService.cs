using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Code_CloudSchool.Models;
using Code_CloudSchool.DTOs; // Added for DTO support

namespace Code_CloudSchool.Interfaces;

public interface IGradeService
{
    /// <summary>
    /// Grades a submission using data from the CreateGradeDto
    /// </summary>
    /// <param name="gradeDto">Data transfer object containing grade information</param>
    /// <returns>The created Grade entity</returns>
    Task<Grade> GradeSubmission(CreateGradeDto gradeDto); // Changed to use DTO

    // Get a grade by its submission ID.
    Task<Grade?> GetGradeBySubmissionId(int submissionId);

    // Update an existing grade (e.g., to correct a grade or add feedback).
    Task<Grade> UpdateGrade(Grade grade);

    // Delete a grade by its ID.
    Task<bool> DeleteGrade(int id); 

    // Get all grades for a specific assignment (useful for lecturers).
    Task<List<Grade>> GetGradesForAssignment(int assignmentId);

    // Get all grades for a specific student (useful for students).
    Task<List<Grade>> GetGradesByStudent(string studentId);

    // Check if a submission has been graded.
    Task<bool> HasSubmissionBeenGraded(int submissionId);
}