// Interfaces/ISubmissionService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Code_CloudSchool.Models;

namespace Code_CloudSchool.Interfaces
{
    public interface ISubmissionService
    {
        // Submit a new assignment.
        Task<Submission> SubmitAssignment(Submission submission);

        // Get all submissions for a specific student.
        Task<List<Submission>> GetSubmissionsByStudent(int studentId);

        // Get a specific submission by its ID.
        Task<Submission> GetSubmissionById(int id);

        // Update an existing submission (e.g., to resubmit or correct a file).
        Task<Submission> UpdateSubmission(Submission submission);

        // Delete a submission by its ID.
        Task<bool> DeleteSubmission(int id);

        // Get all submissions for a specific assignment (useful for lecturers).
        Task<List<Submission>> GetSubmissionsForAssignment(int assignmentId);

        // Check if a student has already submitted an assignment.
        Task<bool> HasStudentSubmittedAssignment(int studentId, int assignmentId);
    }
}