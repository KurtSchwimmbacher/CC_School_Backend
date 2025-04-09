using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Code_CloudSchool.DTOs;
using Code_CloudSchool.Models;

namespace Code_CloudSchool.Interfaces;

// This interface defines the contract for handling all assignment submission operations.
// It ensures consistency across any services implementing this logic.
public interface ISubmissionService
{
    // ================================
    // DTO-based methods 
    // ================================

    // Handles the creation of a new submission using a DTO, which is useful for data validation
    // and avoiding direct exposure of the domain model to external consumers.
    Task<Submission> SubmitAssignment(CreateSubmissionDTO submissionDto);

    // Handles the update of an existing submission using a DTO.
    // This allows safe and controlled updates without modifying the entire model directly.
    Task<Submission> UpdateSubmission(UpdateSubmissionDTO submissionDto);
    
    // ================================
    // Model-based methods 
    // ================================

    // An overload that allows submitting an assignment using the actual model instead of a DTO.
    // This may be used internally or for backward compatibility with older systems.
    Task<Submission> SubmitAssignment(Submission submission);

    // Updates an existing submission using the domain model directly.
    // Can be used in trusted internal contexts where validation has already been handled.
    Task<Submission> UpdateSubmission(Submission submission);

    // ================================
    // Query methods
    // ================================

    // Retrieves all submissions made by a specific student.
    // Useful for displaying a student's history of submitted work.
    Task<List<Submission>> GetSubmissionsByStudent(int studentId);

    // Retrieves a single submission by its unique ID.
    // This is useful when a user views or edits a specific submission.
    Task<Submission> GetSubmissionById(int id);

    // Retrieves all submissions related to a specific assignment.
    // Useful for lecturers wanting to review or grade all responses to one assignment.
    Task<List<Submission>> GetSubmissionsForAssignment(int assignmentId);
    
    // ================================
    // Management and validation methods
    // ================================

    // Deletes a submission by ID.
    // Returns true if the deletion was successful, false otherwise.
    Task<bool> DeleteSubmission(int id);

    // Checks whether a particular student has already submitted a specific assignment.
    // Useful for enforcing one-time submissions or displaying status on the frontend.
    Task<bool> HasStudentSubmittedAssignment(int studentId, int assignmentId);
}
