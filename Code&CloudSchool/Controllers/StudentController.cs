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
        private readonly AppDbContext _context;
        private readonly IStudentAuth _StudentAuth;

        private readonly IStudentStatus _StudentStatus;

        private readonly IStudentReEnroll _StudentReEnroll;

        private readonly IUpdateStudentPassword _StudentPassword;

        public StudentController(AppDbContext context, IStudentAuth studentAuth, IStudentStatus studentStatus, IStudentReEnroll studentReEnroll, IUpdateStudentPassword studentPassword)
        {
            _context = context;
            _StudentAuth = studentAuth;
            _StudentStatus = studentStatus;
            _StudentReEnroll = studentReEnroll;
            _StudentPassword = studentPassword;
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
        public async Task<ActionResult<string>> LoginStudent(LoginDTO student)
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
        [HttpPut("{studentNumber}/  enroll")]
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
