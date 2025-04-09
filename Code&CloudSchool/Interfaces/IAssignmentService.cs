using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Code_CloudSchool.Models;

namespace Code_CloudSchool.Interfaces
{
    // Defines the contract for assignment-related operations
    // Implemented by AssignmentService for dependency injection
    public interface IAssignmentService
    {
        // Creates a new assignment in the system
        // Returns the created assignment with generated ID
        // Throws exception if lecturer validation fails
        Task<Assignment> CreateAssignment(Assignment assignment);

        // Retrieves all assignments created by a specific lecturer
        // lecturerId: The ID of the lecturer to filter by
        // Returns empty list if no assignments found
        Task<List<Assignment>> GetAssignmentsByLecturer(int lecturerId);

        // Gets a single assignment by its unique identifier
        // id: The Assignment_ID to search for
        // Returns null if no matching assignment exists
        Task<Assignment> GetAssignmentById(int id);

        // Updates an existing assignment's details
        // assignment: The modified assignment object
        // Returns the updated assignment
        Task<Assignment> UpdateAssignment(Assignment assignment);

        // Deletes an assignment from the system
        // id: The ID of the assignment to remove
        // Returns true if deletion succeeded, false if assignment not found
        Task<bool> DeleteAssignment(int id);

        // Retrieves all assignments in the system
        // Primarily for administrative/reporting purposes
        Task<List<Assignment>> GetAllAssignments();

        // Gets assignments with optional filters
        // dueDate: Optional filter for assignments due before this date
        // isComplete: Optional filter for completion status (based on graded submissions)
        // Returns filtered list of assignments
        Task<List<Assignment>> GetAssignmentsByFilter(DateTime? dueDate, bool? isComplete);
    }
}