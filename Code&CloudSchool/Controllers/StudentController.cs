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

namespace Code_CloudSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IStudentAuth _StudentAuth;

        public StudentController(AppDbContext context, IStudentAuth studentAuth)
        {
            _context = context;
            _StudentAuth = studentAuth;
        }



        // register student
        [HttpPost("register")]
        public async Task<ActionResult<bool>> RegisterStudent(Student student)
        {
            bool isRegistered = await _StudentAuth.RegisterStudent(student);

            if (!isRegistered)
            {
                return BadRequest("Student already exists");
            }

            return Ok("Student registered successfully");
        }



        [HttpPost("login")]
        public async Task<ActionResult<string>> LoginStudent(Student student)
        {
            string message = await _StudentAuth.LoginStudent(student.Password, student.Email);
            if (message == "Login Successful")
            {
                return Ok(message);
            }
            else
            {
                return BadRequest(message);
            }
        }


        // GET: api/Student
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _context.Students.ToListAsync();
        }

        // GET: api/Student/5
        [HttpGet("{studentNumber}")]
        public async Task<ActionResult<Student>> GetStudent(string studentNumber)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.StudentNumber == studentNumber);

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
