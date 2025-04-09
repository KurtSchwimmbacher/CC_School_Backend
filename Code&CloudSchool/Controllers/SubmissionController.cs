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
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionsController : ControllerBase
    {
        private readonly ISubmissionService _submissionService;
        private readonly AppDBContext _context;

        public SubmissionsController(ISubmissionService submissionService, AppDBContext context)
        {
            _submissionService = submissionService;
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Submission>> SubmitAssignment(CreateSubmissionDTO submissionDto)
        {
            try
            {
                // Validate assignment exists
                var assignmentExists = await _context.Assignments
                    .AnyAsync(a => a.Assignment_ID == submissionDto.AssignmentId);
                if (!assignmentExists)
                    return BadRequest("Assignment does not exist");

                // Validate student exists
                var studentExists = await _context.Students
                    .AnyAsync(s => s.UserId == submissionDto.StudentId);
                if (!studentExists)
                    return BadRequest("Student does not exist");

                var result = await _submissionService.SubmitAssignment(submissionDto);
                return CreatedAtAction(nameof(GetSubmissionById),
                    new { id = result.Submission_ID },
                    result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

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

        [HttpGet("student/{studentId}")]
        public async Task<ActionResult<List<Submission>>> GetSubmissionsByStudent(string studentId)
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
