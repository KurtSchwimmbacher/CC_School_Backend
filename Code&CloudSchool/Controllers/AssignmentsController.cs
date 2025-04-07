using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Code_CloudSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController] public class AssignmentsController : ControllerBase
    {
                private readonly IAssignmentService _assignmentService; // Dependency injection for AssignmentService.

        // Constructor to inject the AssignmentService.
        public AssignmentsController(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }

        [HttpPost] // HTTP POST method to create a new assignment.
        public async Task<ActionResult<Assignment>> CreateAssignment(Assignment assignment)
        {
            try
            {
                // Debug: Log incoming request details
                Console.WriteLine($"Received assignment: {System.Text.Json.JsonSerializer.Serialize(assignment)}");
                Console.WriteLine($"LecturerUser_Id: {assignment.LecturerUser_Id}");

                var result = await _assignmentService.CreateAssignment(assignment); // Call the service method.
                return Ok(result); // Return the created assignment with a 200 OK status.
            }
            catch (Exception ex)
            {
                // Debug: Log full error details
                Console.WriteLine($"FULL ERROR: {ex.ToString()}");
                return StatusCode(500, $"Internal server error: {ex.Message}"); // Handle exceptions.
            }
        }

        [HttpGet("lecturer/{lecturerId}")] // HTTP GET method to get assignments by lecturer ID.
        public async Task<ActionResult<List<Assignment>>> GetAssignmentsByLecturer(int lecturerId)
        {
            try
            {
                var assignments = await _assignmentService.GetAssignmentsByLecturer(lecturerId); // Call the service method.
                return Ok(assignments); // Return the list of assignments with a 200 OK status.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}"); // Handle exceptions.
            }
        }

        [HttpGet("{id}")] // HTTP GET method to get an assignment by its ID.
        public async Task<ActionResult<Assignment>> GetAssignmentById(int id)
        {
            try
            {
                var assignment = await _assignmentService.GetAssignmentById(id); // Call the service method.
                if (assignment == null) return NotFound(); // If no assignment is found, return a 404 Not Found status.
                return Ok(assignment); // Return the assignment with a 200 OK status.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}"); // Handle exceptions.
            }
        }

        [HttpPut] // HTTP PUT method to update an existing assignment.
        public async Task<ActionResult<Assignment>> UpdateAssignment(Assignment assignment)
        {
            try
            {
                var result = await _assignmentService.UpdateAssignment(assignment); // Call the service method.
                return Ok(result); // Return the updated assignment with a 200 OK status.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}"); // Handle exceptions.
            }
        }

        [HttpDelete("{id}")] // HTTP DELETE method to delete an assignment by its ID.
        public async Task<ActionResult<bool>> DeleteAssignment(int id)
        {
            try
            {
                var result = await _assignmentService.DeleteAssignment(id); // Call the service method.
                if (!result) return NotFound(); // If the assignment is not found, return a 404 Not Found status.
                return Ok(result); // Return true with a 200 OK status.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}"); // Handle exceptions.
            }
        }

        [HttpGet] // HTTP GET method to get all assignments.
        public async Task<ActionResult<List<Assignment>>> GetAllAssignments()
        {
            try
            {
                var assignments = await _assignmentService.GetAllAssignments(); // Call the service method.
                return Ok(assignments); // Return the list of assignments with a 200 OK status.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}"); // Handle exceptions.
            }
        }

        [HttpGet("filter")] // HTTP GET method to filter assignments by due date and completion status.
        public async Task<ActionResult<List<Assignment>>> GetAssignmentsByFilter([FromQuery] DateTime? dueDate, [FromQuery] bool? isCompleted)
        {
            try
            {
                var assignments = await _assignmentService.GetAssignmentsByFilter(dueDate, isCompleted); // Call the service method.
                return Ok(assignments); // Return the filtered assignments with a 200 OK status.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}"); // Handle exceptions.
            }
        }
    }
}
