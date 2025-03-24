using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Code_CloudSchool.Data;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.Models;
using Microsoft.EntityFrameworkCore;


namespace Code_CloudSchool.Services;

public class AssignmentService : IAssignmentService
{
            private readonly AppDbContext _context;

        public AssignmentService(AppDbContext context)
        {
            _context = context;
        }

        // Create a new assignment.
        public async Task<Assignment> CreateAssignment(Assignment assignment)
        {
            _context.Assignments.Add(assignment);
            await _context.SaveChangesAsync();
            return assignment;
        }

        // Get all assignments for a specific lecturer.
        public async Task<List<Assignment>> GetAssignmentsByLecturer(int lecturerId)
        {
            return await _context.Assignments
                .Where(a => a.LecturerId == lecturerId)
                .ToListAsync();
        }

        // Get a specific assignment by its ID.
        public async Task<Assignment> GetAssignmentById(int id)
        {
            return await _context.Assignments.FindAsync(id);
        }

        // Update an existing assignment.
        public async Task<Assignment> UpdateAssignment(Assignment assignment)
        {
            _context.Entry(assignment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return assignment;
        }

        // Delete an assignment by its ID.
        public async Task<bool> DeleteAssignment(int id)
        {
            var assignment = await _context.Assignments.FindAsync(id);
            if (assignment == null) return false;

            _context.Assignments.Remove(assignment);
            await _context.SaveChangesAsync();
            return true;
        }

        // Get all assignments (for admin or reporting purposes).
        public async Task<List<Assignment>> GetAllAssignments()
        {
            return await _context.Assignments.ToListAsync();
        }

        // Get assignments with optional filtering (e.g., by due date, status, etc.).
        public async Task<List<Assignment>> GetAssignmentsByFilter(DateTime? dueDate, bool? isCompleted)
        {
            var query = _context.Assignments.AsQueryable();

            if (dueDate.HasValue)
                query = query.Where(a => a.DueDate <= dueDate.Value);

            if (isCompleted.HasValue)
                query = query.Where(a => a.Submissions.Any(s => s.Grade != null) == isCompleted);

            return await query.ToListAsync();
        }

}
