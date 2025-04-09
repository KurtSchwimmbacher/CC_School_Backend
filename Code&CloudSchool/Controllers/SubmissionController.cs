using Microsoft.EntityFrameworkCore;
using Code_CloudSchool.Data; 
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.Models;
using Code_CloudSchool.DTOs;
using System.Threading.Tasks;

namespace Code_CloudSchool.Controllers
{
    // Define the base route for this controller as "api/submissions"
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionsController : ControllerBase
    {
        // Dependency injection for the submission service and database context
        private readonly ISubmissionService _submissionService;
        private readonly AppDBContext _context;

        // Constructor for injecting dependencies via DI container
        public SubmissionsController(ISubmissionService submissionService, AppDBContext context)
        {
            _submissionService = submissionService;
            _context = context;
        }

        // ========================================
        // POST: api/submissions
        // Creates a new assignment submission
        // ========================================
        [HttpPost]
        public async Task<ActionResult<Submission>> SubmitAssignment(CreateSubmissionDTO submissionDto)
        {
            try
            {
                // First, validate if the assignment exists in the database
                var assignmentExists = await _context.Assignments
                    .AnyAsync(a => a.Assignment_ID == submissionDto.AssignmentId);
                if (!assignmentExists)
                    return BadRequest("Assignment does not exist");

                // Next, validate if the student exists in the database
                var studentExists = await _context.Students
                    .AnyAsync(s => s.Id == submissionDto.StudentId);
                if (!studentExists)
                    return BadRequest("Student does not exist");

                // Call the service method to handle submission logic
                var result = await _submissionService.SubmitAssignment(submissionDto);

                // Return a 201 Created response with a reference to the new resource
                return CreatedAtAction(nameof(GetSubmissionById), 
                    new { id = result.Submission_ID }, 
                    result);
            }
            catch (Exception ex)
            {
                // If an exception occurs, return a 500 Internal Server Error with a message
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // ========================================
        // GET: api/submissions/{id}
        // Retrieves a submission by its unique ID
        // ========================================
        [HttpGet("{id}")]
        public async Task<ActionResult<Submission>> GetSubmissionById(int id)
        {
            try
            {
                var submission = await _submissionService.GetSubmissionById(id);

                if (submission == null) 
                    return NotFound($"Submission with ID {id} not found");

                return Ok(submission);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // ========================================
        // GET: api/submissions/assignment/{assignmentId}
        // Retrieves all submissions made for a specific assignment
        // ========================================
        [HttpGet("assignment/{assignmentId}")]
        public async Task<ActionResult<List<Submission>>> GetSubmissionsForAssignment(int assignmentId)
        {
            try
            {
                var submissions = await _submissionService.GetSubmissionsForAssignment(assignmentId);
                return Ok(submissions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // ========================================
        // GET: api/submissions/student/{studentId}
        // Retrieves all submissions made by a specific student
        // ========================================
        [HttpGet("student/{studentId}")]
        public async Task<ActionResult<List<Submission>>> GetSubmissionsByStudent(int studentId)
        {
            try
            {
                var submissions = await _submissionService.GetSubmissionsByStudent(studentId);
                return Ok(submissions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // ========================================
        // PUT: api/submissions
        // Updates a submission using an UpdateSubmissionDTO
        // ========================================
        [HttpPut]
        public async Task<ActionResult<Submission>> UpdateSubmission(UpdateSubmissionDTO submissionDto)
        {
            try
            {
                var result = await _submissionService.UpdateSubmission(submissionDto);

                if (result == null)
                    return NotFound($"Submission with ID {submissionDto.SubmissionId} not found");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // ========================================
        // DELETE: api/submissions/{id}
        // Deletes a submission by its ID
        // ========================================
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteSubmission(int id)
        {
            try
            {
                var result = await _submissionService.DeleteSubmission(id);

                if (!result)
                    return NotFound($"Submission with ID {id} not found");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
