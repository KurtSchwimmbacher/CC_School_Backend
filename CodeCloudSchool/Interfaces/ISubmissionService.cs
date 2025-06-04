using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Code_CloudSchool.DTOs;
using Code_CloudSchool.Models;

namespace Code_CloudSchool.Interfaces;

public interface ISubmissionService
{
    // DTO-based methods
    Task<Submission> SubmitAssignment(CreateSubmissionDTO submissionDto);
    Task<Submission> UpdateSubmission(UpdateSubmissionDTO submissionDto);

    // Model-based methods (legacy support)
    Task<Submission> SubmitAssignment(Submission submission);
    Task<Submission> UpdateSubmission(Submission submission);

    // Query methods
    Task<List<Submission>> GetSubmissionsByStudent(int studentId);
    Task<Submission> GetSubmissionById(int id);
    Task<List<Submission>> GetSubmissionsForAssignment(int assignmentId);

    // Management methods
    Task<bool> DeleteSubmission(int id);
    Task<bool> HasStudentSubmittedAssignment(int studentId, int assignmentId);
    

    Task<Submission> GetSubmissionByAssignmentAndStudent(int assignmentId, int studentId);

}