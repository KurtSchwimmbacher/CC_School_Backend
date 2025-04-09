using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Code_CloudSchool.Models;
using Code_CloudSchool.DTOs; // Added for DTO support to simplify data sent from the client and improve security

namespace Code_CloudSchool.Interfaces;

// This interface defines all the operations related to grading within the application.
// It helps enforce consistency in how grading is handled across services that implement this contract.
public interface IGradeService
{
    // Called when a lecturer or system needs to assign a grade to a submission.
    // Accepts a DTO (CreateGradeDto) that typically contains submission ID, grade value, feedback, etc.
    // Returns the created Grade object, which includes metadata like GradeId, timestamps, etc.
    Task<Grade> GradeSubmission(CreateGradeDto gradeDto); // Changed to use DTO to separate internal model from external requests

    // Used to retrieve a grade based on a specific submission ID.
    // Helpful for both students and lecturers who want to check the result of a particular submission.
    Task<Grade> GetGradeBySubmissionId(int submissionId);

    // Allows an existing grade to be updated. This might be needed if the grade was entered incorrectly
    // or if additional feedback needs to be added after initial grading.
    Task<Grade> UpdateGrade(Grade grade);

    // Permanently removes a grade record from the database using its ID.
    // This might be used in admin workflows or to clean up incorrect entries.
    Task<bool> DeleteGrade(int id);

    // Fetches all grades assigned for a given assignment (based on its ID).
    // Useful for lecturers to view overall performance or generate reports.
    Task<List<Grade>> GetGradesForAssignment(int assignmentId);

    // Retrieves all grades associated with a specific student, using their student number as identifier.
    // Enables students to track their progress across multiple assignments.
    Task<List<Grade>> GetGradesByStudent(string studentNumber);

    // Checks whether a particular submission has already been graded.
    // Prevents duplicate grading and helps with validation during the grading process.
    Task<bool> HasSubmissionBeenGraded(int submissionId);
}
