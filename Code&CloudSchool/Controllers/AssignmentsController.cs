using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Code_CloudSchool.Controllers
{
    [Route("api/[controller]")] // Base route: /api/assignments
    [ApiController]
    public class AssignmentsController : ControllerBase
    {
        private readonly IAssignmentService _assignmentService; // Injected service for assignment operations

        // Constructor with dependency injection
        public AssignmentsController(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }

        [HttpPost] // POST /api/assignments
        public async Task<ActionResult<Assignment>> CreateAssignment(Assignment assignment)
        {
            try
            {
                // Log request details for debugging
                Console.WriteLine($"Received assignment: {System.Text.Json.JsonSerializer.Serialize(assignment)}");
                Console.WriteLine($"LecturerUser_Id: {assignment.LecturerUser_Id}");

                var result = await _assignmentService.CreateAssignment(assignment);
                return Ok(result); // 200 OK with created assignment
            }
            catch (Exception ex)
            {
                Console.WriteLine($"FULL ERROR: {ex.ToString()}");
                return StatusCode(500, $"Internal server error: {ex.Message}"); // 500 on failure
            }
        }

        [HttpGet("lecturer/{lecturerId}")] // GET /api/assignments/lecturer/5
        public async Task<ActionResult<List<Assignment>>> GetAssignmentsByLecturer(int lecturerId)
        {
            try
            {
                var assignments = await _assignmentService.GetAssignmentsByLecturer(lecturerId);
                return Ok(assignments); // 200 OK with assignments list
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")] // GET /api/assignments/3
        public async Task<ActionResult<Assignment>> GetAssignmentById(int id)
        {
            try
            {
                var assignment = await _assignmentService.GetAssignmentById(id);
                if (assignment == null) return NotFound(); // 404 if not found
                return Ok(assignment); // 200 OK with assignment
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut] // PUT /api/assignments
        public async Task<ActionResult<Assignment>> UpdateAssignment(Assignment assignment)
        {
            try
            {
                var result = await _assignmentService.UpdateAssignment(assignment);
                return Ok(result); // 200 OK with updated assignment
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")] // DELETE /api/assignments/3
        public async Task<ActionResult<bool>> DeleteAssignment(int id)
        {
            try
            {
                var result = await _assignmentService.DeleteAssignment(id);
                if (!result) return NotFound(); // 404 if not found
                return Ok(result); // 200 OK with true/false
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet] // GET /api/assignments
        public async Task<ActionResult<List<Assignment>>> GetAllAssignments()
        {
            try
            {
                var assignments = await _assignmentService.GetAllAssignments();
                return Ok(assignments); // 200 OK with all assignments
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("filter")] // GET /api/assignments/filter?dueDate=2023-12-31&isCompleted=true
        public async Task<ActionResult<List<Assignment>>> GetAssignmentsByFilter(
            [FromQuery] DateTime? dueDate, 
            [FromQuery] bool? isCompleted)
        {
            try
            {
                var assignments = await _assignmentService.GetAssignmentsByFilter(dueDate, isCompleted);
                return Ok(assignments); // 200 OK with filtered assignments
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}