using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Code_CloudSchool.Models;
using Code_CloudSchool.Data;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.DTOs;

namespace Code_CloudSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LecturerRegController : ControllerBase
    {
        private readonly AppDBContext _context;

        private readonly ILecturerAuth _lecturerAuth;

        private readonly IEmailVerificationService _emailVerificationService;

        public LecturerRegController(AppDBContext context, ILecturerAuth lecturerAuth, IEmailVerificationService emailVerificationService)
        {
            _lecturerAuth = lecturerAuth;
            _context = context;
            _emailVerificationService = emailVerificationService;
        }

        // GET: api/LecturerReg
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LecturerReg>>> GetLecturerReg()
        {
            return await _context.Lecturers.ToListAsync();
        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignCourseToLecturer([FromBody] AssignLecturerToCourse request)
        {
            try
            {
                var lecturer = await _context.Lecturers.FindAsync(request.LecturerId);
                if (lecturer == null)
                    return NotFound($"Lecturer with ID {request.LecturerId} not found.");

                var course = await _context.Courses.FindAsync(request.CourseId);
                if (course == null)
                    return NotFound($"Course with ID {request.CourseId} not found.");

                course.LecturerId = request.LecturerId;
                await _context.SaveChangesAsync();

                return Ok($"Lecturer {request.LecturerId} assigned to course {request.CourseId}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        // GET: api/LecturerReg/5
        [HttpGet("{id}")]
        public IActionResult GetLecturer(int id)
        {
            var lecturer = _context.Lecturers
                .Include(l => l.Courses)
                .FirstOrDefault(l => l.LecturerId == id);

            if (lecturer == null)
            {
                return NotFound();
            }

            return Ok(lecturer);
        }

        // PUT: api/LecturerReg/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLecturerReg(int id, LecturerReg lecturerReg)
        {
            if (id != lecturerReg.LecturerId)
            {
                return BadRequest();
            }

            _context.Entry(lecturerReg).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LecturerRegExists(id))
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


        // register student
        [HttpPost("register")]
        public async Task<ActionResult<LecturerReg>> RegisterLecturer(RegisterLecturerDTO dto)
        {
            var newLecturer = new LecturerReg
            {
                Name = dto.Name,
                LastName = dto.LastName,
                Password = dto.Password,
                privateEmail = dto.PrivateEmail,
                PhoneNumber = dto.PhoneNumber,
                Department = dto.Department,
                DateOfJoining = dto.DateOfJoining
            };

            var registeredLecturer = await _lecturerAuth.RegisterLecturer(newLecturer);

            if (registeredLecturer == null)
            {
                return BadRequest("Lecturer already exists");
            }

            // trigger verification 
            var token = await _emailVerificationService.GenerateAndStoreToken(registeredLecturer);
            await _emailVerificationService.SendVerificationEmail(registeredLecturer, token, registeredLecturer.LecEmail);

            return Ok("Verification email sent. Please check your inbox");
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
        public async Task<ActionResult<UserLoginReturnDTO>> LoginStudent(LoginDTO lecturer)
        {
            var LecturerLogin = await _lecturerAuth.LoginLecturer(lecturer.Email, lecturer.Password);

            if (LecturerLogin == null)
            {
                return BadRequest("Invalid credentials or student not found");
            }

            var UserLoginReturnDTO = new UserLoginReturnDTO
            {
                UserID = LecturerLogin.UserId,
                Name = LecturerLogin.Name,
                Email = LecturerLogin.LecEmail,
                Role = LecturerLogin.Role
            };

            return Ok(UserLoginReturnDTO);
        }


        // POST: api/LecturerReg
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LecturerReg>> PostLecturerReg(LecturerReg lecturerReg)
        {
            _context.Lecturers.Add(lecturerReg);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLecturerReg", new { id = lecturerReg.LecturerId }, lecturerReg);
        }

        // DELETE: api/LecturerReg/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLecturerReg(int id)
        {
            var lecturerReg = await _context.Lecturers.FindAsync(id);
            if (lecturerReg == null)
            {
                return NotFound();
            }

            _context.Lecturers.Remove(lecturerReg);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LecturerRegExists(int id)
        {
            return _context.Lecturers.Any(e => e.LecturerId == id);
        }
    }

    internal class LecturerDTO
    {
        public int Id { get; set; }
        public string? LectName { get; set; } = string.Empty;
        public string? LecLastName { get; set; } = string.Empty;
        public string? LecEmail { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
        public string? Department { get; set; } = string.Empty;
        public DateTime DateOfJoining { get; set; }
        public bool IsActive { get; set; }
    }
}

