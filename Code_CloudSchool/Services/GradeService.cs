// Services/GradeService.cs
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Code_CloudSchool.Data;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.Models;
using Microsoft.EntityFrameworkCore;

namespace Code_CloudSchool.Services
{
    public class GradeService : IGradeService
    {
        private readonly AppDbContext _context;

        public GradeService(AppDbContext context)
        {
            _context = context;
        }

        // Grade a submission.
        public async Task<Grade> GradeSubmission(Grade grade)
        {
            _context.Grades.Add(grade);
            await _context.SaveChangesAsync();
            return grade;
        }

        // Get a grade by its submission ID.
        public async Task<Grade> GetGradeBySubmissionId(int submissionId)
        {
            return await _context.Grades
                .FirstOrDefaultAsync(g => g.SubmissionId == submissionId);
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
                .Where(g => g.Submission.AssignmentId == assignmentId)
                .ToListAsync();
        }

        // Get all grades for a specific student.
        public async Task<List<Grade>> GetGradesByStudent(int studentId)
        {
            return await _context.Grades
                .Include(g => g.Submission)
                .Where(g => g.Submission.StudentId == studentId)
                .ToListAsync();
        }

        // Check if a submission has been graded.
        public async Task<bool> HasSubmissionBeenGraded(int submissionId)
        {
            return await _context.Grades
                .AnyAsync(g => g.SubmissionId == submissionId);
        }
    }
}