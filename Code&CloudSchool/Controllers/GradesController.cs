using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.Models;
using Code_CloudSchool.DTOs; // Added for DTO support
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; // Added for Include() method
using Code_CloudSchool.Data; // Added for AppDbContext

namespace Code_CloudSchool.Controllers
{
    [Route("api/[controller]")] // Base route for this controller (e.g., "/api/grades").
    [ApiController] // Indicates this is an API controller.
    public class GradesController : ControllerBase
    {
        private readonly IGradeService _gradeService; // Dependency injection for GradeService.
        private readonly AppDbContext _context; // Added for direct database access when needed

        // Constructor to inject the GradeService and AppDbContext.
        public GradesController(IGradeService gradeService, AppDbContext context)
        {
            _gradeService = gradeService;
            _context = context; // Initialize the DbContext
        }

[HttpPost] // HTTP POST method to grade a submission.
public async Task<ActionResult<Grade>> GradeSubmission(CreateGradeDto gradeDto)
{
    try
    {
        // Pass the DTO directly to the service which handles validation and grade creation
        var result = await _gradeService.GradeSubmission(gradeDto);
        
        // Return 201 Created status with location header pointing to the new resource
        return CreatedAtAction(
            nameof(GetGradeBySubmissionId), 
            new { submissionId = result.Submission_ID }, 
            result);
    }
    catch (ArgumentException ex)
    {
        // Handle case where submission wasn't found (thrown by service layer)
        return NotFound(ex.Message);
    }
    catch (Exception ex)
    {
        // Handle all other unexpected errors
        return StatusCode(500, $"Internal server error: {ex.Message}");
    }
}
        /* The rest of your controller methods remain exactly the same */
        [HttpGet("submission/{submissionId}")] // HTTP GET method to get a grade by submission ID.
        public async Task<ActionResult<Grade>> GetGradeBySubmissionId(int submissionId)
        {
            try
            {
                var grade = await _gradeService.GetGradeBySubmissionId(submissionId); // Call the service method.
                if (grade == null) return NotFound(); // If no grade is found, return a 404 Not Found status.
                return Ok(grade); // Return the grade with a 200 OK status.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}"); // Handle exceptions.
            }
        }

        [HttpPut] // HTTP PUT method to update a grade.
        public async Task<ActionResult<Grade>> UpdateGrade(Grade grade)
        {
            try
            {
                var result = await _gradeService.UpdateGrade(grade); // Call the service method.
                return Ok(result); // Return the updated grade with a 200 OK status.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}"); // Handle exceptions.
            }
        }

        [HttpDelete("{id}")] // HTTP DELETE method to delete a grade by its ID.
        public async Task<ActionResult<bool>> DeleteGrade(int id)
        {
            try
            {
                var result = await _gradeService.DeleteGrade(id); // Call the service method.
                if (!result) return NotFound(); // If the grade is not found, return a 404 Not Found status.
                return Ok(result); // Return true with a 200 OK status.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}"); // Handle exceptions.
            }
        }

        [HttpGet("assignment/{assignmentId}")] // HTTP GET method to get all grades for a specific assignment.
        public async Task<ActionResult<List<Grade>>> GetGradesForAssignment(int assignmentId)
        {
            try
            {
                var grades = await _gradeService.GetGradesForAssignment(assignmentId); // Call the service method.
                return Ok(grades); // Return the list of grades with a 200 OK status.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}"); // Handle exceptions.
            }
        }

        [HttpGet("student/{studentId}")] // HTTP GET method to get all grades for a specific student.
        public async Task<ActionResult<List<Grade>>> GetGradesByStudent(int studentId)
        {
            try
            {
                var grades = await _gradeService.GetGradesByStudent(studentId); // Call the service method.
                return Ok(grades); // Return the list of grades with a 200 OK status.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}"); // Handle exceptions.
            }
        }

        [HttpGet("has-graded/{submissionId}")] // HTTP GET method to check if a submission has been graded.
        public async Task<ActionResult<bool>> HasSubmissionBeenGraded(int submissionId)
        {
            try
            {
                var result = await _gradeService.HasSubmissionBeenGraded(submissionId); // Call the service method.
                return Ok(result); // Return true or false with a 200 OK status.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}"); // Handle exceptions.
            }
        }
    }
}