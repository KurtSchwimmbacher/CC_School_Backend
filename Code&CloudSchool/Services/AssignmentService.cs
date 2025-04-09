using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Code_CloudSchool.Data;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.Models;
using Microsoft.EntityFrameworkCore;

namespace Code_CloudSchool.Services;

// Service layer for handling assignment-related business logic
// Implements IAssignmentService interface for dependency injection
public class AssignmentService : IAssignmentService
{
    private readonly AppDBContext _context; // Database context for EF Core operations

    // Constructor injection of database context
    public AssignmentService(AppDBContext context)
    {
        _context = context; // Initialised via dependency injection
    }

    // Creates a new assignment in the database with validation
    public async Task<Assignment> CreateAssignment(Assignment assignment)
    {
        try 
        {
            // Verify the lecturer exists and has correct role
            var lecturerUser = await _context.Lecturers
                .FirstOrDefaultAsync(l => l.LecturerId == assignment.LecturerUser_Id);

            // Validation: Ensure lecturer exists
            if (lecturerUser == null)
                throw new Exception($"User {assignment.LecturerUser_Id} not found");

            // Validation: Ensure user is actually a lecturer
            if (lecturerUser.Discriminator != "LecturerReg")
                throw new Exception($"User {assignment.LecturerUser_Id} is not a lecturer");

            _context.Assignments.Add(assignment);
            await _context.SaveChangesAsync(); // Persist changes to database
            return assignment;
        }
        catch (Exception ex)
        {
            // Log full error details for debugging
            Console.WriteLine($"FULL ERROR: {ex.ToString()}");
            throw; // Re-throw to preserve stack trace for error handling
        }
    }

    // Retrieves all assignments for a specific lecturer
    public async Task<List<Assignment>> GetAssignmentsByLecturer(int lecturerId)
    {
        return await _context.Assignments
            .Where(a => a.LecturerUser_Id == lecturerId) // Filter by lecturer ID
            .ToListAsync(); // Execute query asynchronously
    }

    // Gets a single assignment by its unique ID
    public async Task<Assignment> GetAssignmentById(int id)
    {
        Assignment? assignment = await _context.Assignments.FindAsync(id);
        return assignment; // Returns null if not found
    }

    // Updates an existing assignment
    public async Task<Assignment> UpdateAssignment(Assignment assignment)
    {
        // Mark entity as modified to trigger update
        _context.Entry(assignment).State = EntityState.Modified;
        await _context.SaveChangesAsync(); // Save changes
        return assignment;
    }

    // Deletes an assignment by ID
    public async Task<bool> DeleteAssignment(int id)
    {
        var assignment = await _context.Assignments.FindAsync(id);
        if (assignment == null) return false; // Not found

        _context.Assignments.Remove(assignment);
        await _context.SaveChangesAsync(); // Commit deletion
        return true; // Success
    }

    // Gets all assignments in the system (admin/reporting use)
    public async Task<List<Assignment>> GetAllAssignments()
    {
        return await _context.Assignments.ToListAsync();
    }

    // Gets assignments with optional filters
    public async Task<List<Assignment>> GetAssignmentsByFilter(DateTime? dueDate, bool? isCompleted)
    {
        // Start with base query
        var query = _context.Assignments.AsQueryable();

        // Apply due date filter if provided
        if (dueDate.HasValue)
            query = query.Where(a => a.DueDate <= dueDate.Value);

        // Apply completion status filter if provided
        if (isCompleted.HasValue)
            // Check if any submissions have grades (completed)
            query = query.Where(a => a.Submissions.Any(s => s.Grade != null) == isCompleted);

        return await query.ToListAsync();
    }
}