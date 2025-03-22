using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Code_CloudSchool.Data;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.Models;
using Microsoft.EntityFrameworkCore;

namespace Code_CloudSchool.Services;

public class SubmissionService : ISubmissionService
{
    private readonly AppDbContext _context;

        public SubmissionService(AppDbContext context)
        {
            _context = context;
        }

        // Submit a new assignment.
        public async Task<Submission> SubmitAssignment(Submission submission)
        {
            _context.Submissions.Add(submission);
            await _context.SaveChangesAsync();
            return submission;
        }

        // Get all submissions for a specific student.
        public async Task<List<Submission>> GetSubmissionsByStudent(int studentId)
        {
            return await _context.Submissions
                .Where(s => s.Student.Id == studentId)
                .ToListAsync();
        }

        // Get a specific submission by its ID.
        public async Task<Submission> GetSubmissionById(int id)
        {
            return await _context.Submissions.FindAsync(id);
        }

        // Update an existing submission.
        public async Task<Submission> UpdateSubmission(Submission submission)
        {
            _context.Entry(submission).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return submission;
        }

        // Delete a submission by its ID.
        public async Task<bool> DeleteSubmission(int id)
        {
            var submission = await _context.Submissions.FindAsync(id);
            if (submission == null) return false;

            _context.Submissions.Remove(submission);
            await _context.SaveChangesAsync();
            return true;
        }

        // Get all submissions for a specific assignment.
        public async Task<List<Submission>> GetSubmissionsForAssignment(int assignmentId)
        {
            return await _context.Submissions
                .Where(s => s.Assignment.Assignment_ID == assignmentId) // Use Assignment navigation property
                .ToListAsync();
        }


        // Check if a student has already submitted an assignment.
        public async Task<bool> HasStudentSubmittedAssignment(int studentId, int assignmentId)
        {
            return await _context.Submissions
                .AnyAsync(s => s.Student.Id == studentId && s.Assignment.Assignment_ID == assignmentId); // Use Assignment navigation property
        }

}
