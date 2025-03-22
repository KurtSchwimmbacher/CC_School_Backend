using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Code_CloudSchool.Models;

namespace Code_CloudSchool.Interfaces;

public interface IGradeService
{
        // Grade a submission.
        Task<Grade> GradeSubmission(Grade grade);

        // Get a grade by its submission ID.
        Task<Grade> GetGradeBySubmissionId(int submissionId);

        // Update an existing grade (e.g., to correct a grade or add feedback).
        Task<Grade> UpdateGrade(Grade grade);

        // Delete a grade by its ID.
        Task<bool> DeleteGrade(int id);

        // Get all grades for a specific assignment (useful for lecturers).
        Task<List<Grade>> GetGradesForAssignment(int assignmentId);

        // Get all grades for a specific student (useful for students).
        Task<List<Grade>> GetGradesByStudent(int studentId);

        // Check if a submission has been graded.
        Task<bool> HasSubmissionBeenGraded(int submissionId);

}
