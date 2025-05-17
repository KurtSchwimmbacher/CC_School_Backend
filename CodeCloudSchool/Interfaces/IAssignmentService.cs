using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Code_CloudSchool.Models; // Ensure this namespace is correct

namespace Code_CloudSchool.Interfaces
{
    public interface IAssignmentService
    {
        // Create a new assignment.
        Task<Assignment> CreateAssignment(Assignment assignment);

        // Get all assignments for a specific lecturer.
        Task<List<Assignment>> GetAssignmentsByLecturer(int lecturerId);

        // Get a specific assignment by its ID.
        Task<Assignment> GetAssignmentById(int id);

        // Update an existing assignment.
        Task<Assignment> UpdateAssignment(Assignment assignment);

        // Delete an assignment by its ID.
        Task<bool> DeleteAssignment(int id);

        // Get all assignments (for admin or reporting purposes).
        Task<List<Assignment>> GetAllAssignments();

        // Get assignments with optional filtering (e.g., by due date, status, etc.).
        Task<List<Assignment>> GetAssignmentsByFilter(DateTime? dueDate, bool? isComplete);
    }
}