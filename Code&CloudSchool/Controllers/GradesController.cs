using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Code_CloudSchool.Controllers
{
    [Route("api/[controller]")]// Base route for this controller (e.g., "/api/grades").
    [ApiController]// Indicates this is an API controller.
    public class GradesController : ControllerBase
    {
        private readonly IGradeService _gradeService; // Dependency injection for GradeService.

        // Constructor to inject the GradeService.
        public GradesController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }

        [HttpPost] // HTTP POST method to grade a submission.
        public async Task<ActionResult<Grade>> GradeSubmission(Grade grade)
        {
            try
            {
                var result = await _gradeService.GradeSubmission(grade); // Call the service method.
                return Ok(result); // Return the graded submission with a 200 OK status.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}"); // Handle exceptions.
            }
        }

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
