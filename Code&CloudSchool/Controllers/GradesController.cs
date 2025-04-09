using Microsoft.AspNetCore.Http; // Provides types for handling HTTP-specific features like requests and responses
using Microsoft.AspNetCore.Mvc; // Provides attributes and types for building Web APIs
using Code_CloudSchool.Interfaces; // Needed for accessing the IGradeService interface
using Code_CloudSchool.Models; // Imports the Grade model
using Code_CloudSchool.DTOs; // Allows the use of CreateGradeDto to handle input data from the client
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; // Used for querying related data with Include()
using Code_CloudSchool.Data; // Provides access to the application’s DbContext

namespace Code_CloudSchool.Controllers
{
    [Route("api/[controller]")] // Sets the base route for this controller, e.g. "/api/grades"
    [ApiController] // Enables automatic request validation and model binding features for APIs
    public class GradesController : ControllerBase
    {
        private readonly IGradeService _gradeService; // Service layer abstraction to handle business logic for grading
        private readonly AppDBContext _context; // Direct access to database when needed for lower-level queries

        // Constructor to inject the required services into the controller
        public GradesController(IGradeService gradeService, AppDBContext context)
        {
            _gradeService = gradeService; // Assign the injected GradeService to the private field
            _context = context; // Assign the injected DbContext for future use
        }

        [HttpPost] // Handles HTTP POST requests to create a new grade
        public async Task<ActionResult<Grade>> GradeSubmission(CreateGradeDto gradeDto)
        {
            try
            {
                // Passes the incoming grade data (in DTO format) to the service for processing and saving
                var result = await _gradeService.GradeSubmission(gradeDto);

                // Returns a 201 response with a reference to the GetGradeBySubmissionId route and the created grade
                return CreatedAtAction(
                    nameof(GetGradeBySubmissionId), // Target action name for location header
                    new { submissionId = result.Submission_ID }, // Route values to generate the correct URL
                    result); // Include the created Grade object in the response body
            }
            catch (ArgumentException ex)
            {
                // If the submission ID is invalid or not found, return 404 with a message
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Fallback error handling for any unexpected exceptions
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("submission/{submissionId}")] // Handles HTTP GET requests to fetch a grade by submission ID
        public async Task<ActionResult<Grade>> GetGradeBySubmissionId(int submissionId)
        {
            try
            {
                // Calls the service to find a grade using the provided submission ID
                var grade = await _gradeService.GetGradeBySubmissionId(submissionId);

                // If the grade doesn't exist, return a 404
                if (grade == null) return NotFound();

                // Otherwise return the found grade with a 200 OK
                return Ok(grade);
            }
            catch (Exception ex)
            {
                // Handle any unexpected errors
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut] // Handles HTTP PUT requests to update an existing grade
        public async Task<ActionResult<Grade>> UpdateGrade(Grade grade)
        {
            try
            {
                // Pass the grade object to the service for updating in the database
                var result = await _gradeService.UpdateGrade(grade);

                // Return the updated grade object
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Return a 500 status code if something goes wrong
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")] // Handles HTTP DELETE requests to remove a grade by its ID
        public async Task<ActionResult<bool>> DeleteGrade(int id)
        {
            try
            {
                // Call the service to delete the grade and capture the result
                var result = await _gradeService.DeleteGrade(id);

                // If the grade was not found or deletion failed, return 404
                if (!result) return NotFound();

                // Return true to confirm deletion
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Catch all unexpected issues
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("assignment/{assignmentId}")] // Handles HTTP GET requests to fetch all grades for a specific assignment
        public async Task<ActionResult<List<Grade>>> GetGradesForAssignment(int assignmentId)
        {
            try
            {
                // Use the service to fetch all grades linked to the assignment ID
                var grades = await _gradeService.GetGradesForAssignment(assignmentId);

                // Return the list of grades with 200 OK
                return Ok(grades);
            }
            catch (Exception ex)
            {
                // Standard error handling
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("student/{studentNumber}")] // Handles HTTP GET requests to retrieve grades by student number
        public async Task<ActionResult<List<Grade>>> GetGradesByStudent(string studentNumber)
        {
            try
            {
                // Fetch all grades associated with the specified student
                var grades = await _gradeService.GetGradesByStudent(studentNumber);

                // Return the grades list
                return Ok(grades);
            }
            catch (Exception ex)
            {
                // Handle unexpected failures
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("has-graded/{submissionId}")] // Handles HTTP GET requests to check if a submission has been graded
        public async Task<ActionResult<bool>> HasSubmissionBeenGraded(int submissionId)
        {
            try
            {
                // Use the service to check whether the submission has a grade
                var result = await _gradeService.HasSubmissionBeenGraded(submissionId);

                // Return true if graded, false if not
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Provide detailed server error in case of failure
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
