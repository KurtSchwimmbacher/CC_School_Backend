using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Code_CloudSchool.Data;
using Code_CloudSchool.Models;
using Code_CloudSchool.Services;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.DTOs;

namespace Code_CloudSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly IStudentAuth _StudentAuth;

        private readonly IStudentStatus _StudentStatus;

        private readonly IStudentReEnroll _StudentReEnroll;

        private readonly IUpdateStudentPassword _StudentPassword;

        private readonly IEmailVerificationService _emailVerificationService;


        public StudentController(AppDBContext context, IStudentAuth studentAuth, IStudentStatus studentStatus, IStudentReEnroll studentReEnroll, IUpdateStudentPassword studentPassword, IEmailVerificationService emailVerificationService)
        {
            _context = context;
            _StudentAuth = studentAuth;
            _StudentStatus = studentStatus;
            _StudentReEnroll = studentReEnroll;
            _StudentPassword = studentPassword;
            _emailVerificationService = emailVerificationService;
        }



        // register student
        [HttpPost("register")]
        public async Task<ActionResult<Student>> RegisterStudent(RegisterStudentDTO dto)
        {
            var newStudent = new Student
            {
                Name = dto.Name,
                LastName = dto.LastName,
                Password = dto.Password,
                privateEmail = dto.PrivateEmail,
                Gender = dto.Gender,
                Address = dto.Address,
                YearLevel = dto.YearLevel
            };

            var registeredStudent = await _StudentAuth.RegisterStudent(newStudent);

            if (registeredStudent == null)
            {
                return BadRequest("Student already exists");
            }

            // Trigger verification
            var token = await _emailVerificationService.GenerateAndStoreToken(registeredStudent);
            await _emailVerificationService.SendVerificationEmail(registeredStudent, token, registeredStudent.Email);

            return Ok("Verification email sent. Please check your inbox.");
        }


        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailDTO dto)
        {
            // Find the token record, include the User with it
            var tokenEntry = await _context.EmailVerificationTokens
                                .Include(t => t.User)
                                .FirstOrDefaultAsync(t => t.Token == dto.Token);

            if (tokenEntry == null)
            {
                return BadRequest("Invalid token.");
            }

            if (tokenEntry.ExpiryTime < DateTime.UtcNow)
            {
                return BadRequest("Token has expired.");
            }

            // Mark the user as verified
            tokenEntry.User.IsEmailVerified = true;

            // Remove or invalidate the token to prevent reuse
            _context.EmailVerificationTokens.Remove(tokenEntry);

            await _context.SaveChangesAsync();

            return Ok("Email successfully verified.");
        }





        [HttpPost("login")]
        public async Task<ActionResult<UserLoginReturnDTO>> LoginStudent(LoginDTO student)
        {
            var studentLogin = await _StudentAuth.LoginStudent(student.Password, student.Email);

            if (studentLogin == null)
            {
                return BadRequest("Invalid credentials or student not found");
            }

            var UserLoginReturnDTO = new UserLoginReturnDTO
            {
                UserID = studentLogin.UserId,
                Name = studentLogin.Name,
                Email = studentLogin.Email,
                Role = studentLogin.Role
            };

            return Ok(UserLoginReturnDTO);
        }



        // GET: api/Student
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _context.Students
            .Include(s => s.Courses)
            .ToListAsync();
        }

        [HttpGet("{studentNumber}")]
        public async Task<ActionResult<Student>> GetStudent(string studentNumber)
        {
            var student = await _context.Students
                .Include(s => s.Courses)
                .FirstOrDefaultAsync(s => s.StudentNumber == studentNumber);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }


        // PUT: api/Student/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{studentNumber}")]
        public async Task<IActionResult> PutStudent(string studentNumber, Student student)
        {
            if (studentNumber != student.StudentNumber)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(studentNumber))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        /// <summary>
        /// update the students status
        /// </summary>
        /// <param name="studentNumber"></param>
        /// <returns></returns>
        [HttpPut("{studentNumber}/status")]
        public async Task<IActionResult> UpdateStudentStatus(string studentNumber, UpdateStudentStatusDTO statusDTO)
        {
            bool updated = await _StudentStatus.UpdateStudentStatus(studentNumber, statusDTO);

            if (!updated)
            {
                return NotFound("Student update failed or student not found");
            }

            return Ok("Student Status updated successfully");
        }

        /// <summary>
        /// Update the year level of a student who has re-enrolled
        /// </summary>
        /// <param name="studentNumber"></param>
        /// <returns></returns>
        [HttpPut("{studentNumber}/enroll")]
        public async Task<ActionResult> StudentReEnroll(string studentNumber, StudentReEnrollDTO studentReEnrollDTO)
        {
            bool updated = await _StudentReEnroll.UpdateStudentYearLevel(studentNumber, studentReEnrollDTO);

            if (!updated)
            {
                return NotFound("Student update failed or student not found");
            }

            return Ok("Student Year Level updated Successfully");
        }



        [HttpPut("{studentNumber}/updatePassword")]
        public async Task<IActionResult> UpdateStudentPassword(string studentNumber, StudentPasswordDTO studentPasswordDTO)
        {
            bool updated = await _StudentPassword.UpdateStudentPassword(studentNumber, studentPasswordDTO);

            if (!updated)
            {
                return BadRequest("Password update failed. Incorrect old password or student not found.");
            }

            return Ok("Password updated successfully.");
        }



        // DELETE: api/Student/5
        [HttpDelete("{studentNumber}")]
        public async Task<IActionResult> DeleteStudent(string studentNumber)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.StudentNumber == studentNumber);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExists(string studentNumber)
        {
            return _context.Students.Any(e => e.StudentNumber == studentNumber);
        }
    }
}
