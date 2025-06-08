using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Code_CloudSchool.Data;
using Code_CloudSchool.Models;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.DTOs;

namespace Code_CloudSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly AppDBContext _context;

        private readonly IClassesServices _classesServices;
        public ClassesController(AppDBContext context, IClassesServices classesServices)
        {
            _context = context;
            _classesServices = classesServices;
        }

        // GET: api/Classes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Classes>>> GetClasses()
        {
            var classes = await _context.Classes
                .Include(c => c.Courses)
                .Include(c => c.Lecturers)
                .Include(c => c.Student)
                .Include(c => c.TimeSlot)
                .ToListAsync();

            if (classes == null || !classes.Any())
            {
                return NotFound();
            }

            return classes;
        }

        // GET: api/Classes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Classes>> GetClasses(int id)
        {
            var classes = await _context.Classes
            .Include(c => c.Student)    // Include students
            .Include(c => c.Courses)    // Include course
            .Include(c => c.Lecturers)  // Include lecturers
            .FirstOrDefaultAsync(c => c.classID == id);

            if (classes == null)
            {
                return NotFound();
            }

            return classes;
        }

        [HttpGet("details/{id}")]
        public async Task<ActionResult<ClassDetailsDTO>> GetClassDetails(int id)
        {
            var classDetails = await _context.Classes
                .Where(c => c.classID == id)
                .Select(c => new ClassDetailsDTO
                {
                    ClassId = c.classID,
                    ClassName = c.className,
                    classDescription = c.classDescription,
                    CourseName = c.Courses != null ? c.Courses.courseName : null
                })
                .FirstOrDefaultAsync();

            if (classDetails == null)
            {
                return NotFound();
            }

            return classDetails;
        }

        [HttpGet("getClassLecturers/{classId}")]
        public async Task<ActionResult<LecturerReg>> GetClassLecturersAsync(int classId)
        {
            if (classId <= 0)
            {
                return BadRequest("Invalid class ID");
            }

            try
            {
                var classLecturers = await _classesServices.GetClassLecturersAsync(classId);

                if (classLecturers == null)
                {
                    return NotFound($"No Lecturers found for class with ID {classId}");
                }

                return Ok(classLecturers);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }

        [HttpGet("byStudent/{studentId}")]
        public async Task<ActionResult<List<object>>> GetClassesByStudentId(int studentId)
        {
            var classes = await _context.Classes
                .Where(c => c.Student.Any(s => s.UserId == studentId))
                .Select(c => new
                {
                    c.classID,
                    c.className,
                    c.Classroom,
                    c.classDescription,
                    TimeSlot = new
                    {
                        c.TimeSlot.TimeSlotId,
                        c.TimeSlot.Day,
                        c.TimeSlot.StartTime,
                        c.TimeSlot.EndTime
                    },
                    Courses = new
                    {
                        c.Courses.Id,
                        c.Courses.courseName,
                        c.Courses.courseCode
                    },
                    Lecturers = c.Lecturers.Select(l => new
                    {
                        l.UserId,
                        l.Name,
                        l.LastName,
                        l.LecEmail
                    }),
                    Students = c.Student.Select(s => new
                    {
                        s.UserId,
                        s.Name,
                        s.LastName,
                        s.StudentNumber,
                        s.Email
                    })
                })
                .AsNoTracking()
                .ToListAsync();

            if (classes == null || classes.Count == 0)
                return NotFound($"No classes found for student with ID {studentId}");

            return Ok(classes);
        }

        [HttpGet("byLecturer/{lecturerId}")]
        public async Task<ActionResult<List<object>>> GetClassesByLecturerId(int lecturerId)
        {
            if (lecturerId <= 0)
                return BadRequest("Invalid lecturer ID");

            var classes = await _context.Classes
                .Where(c => c.Lecturers.Any(l => l.UserId == lecturerId))
                .Select(c => new
                {
                    c.classID,
                    c.className,
                    c.Classroom,
                    c.classDescription,
                    TimeSlot = new
                    {
                        c.TimeSlot.TimeSlotId,
                        c.TimeSlot.Day,
                        c.TimeSlot.StartTime,
                        c.TimeSlot.EndTime
                    },
                    Courses = new
                    {
                        c.Courses.Id,
                        c.Courses.courseName,
                        c.Courses.courseCode
                    },
                    Lecturers = c.Lecturers.Select(l => new
                    {
                        l.UserId,
                        l.Name,
                        l.LastName,
                        l.LecEmail
                    }),
                    Students = c.Student.Select(s => new
                    {
                        s.UserId,
                        s.Name,
                        s.LastName,
                        s.StudentNumber,
                        s.Email
                    })
                })
                .AsNoTracking()
                .ToListAsync();

            if (classes == null || !classes.Any())
                return NotFound($"No classes found for lecturer with ID {lecturerId}");

            return Ok(classes);
        }



        [HttpGet("students/{classId}")]
        public async Task<ActionResult<List<Student>>> GetClassStudents(int classId)
        {
            if (classId <= 0)
            {
                return BadRequest("Invalid class ID");
            }

            try
            {
                var classStudents = await _classesServices.GetClassStudentsAsync(classId);

                if (classStudents == null || !classStudents.Any())
                {
                    return NotFound($"No students found for class with ID {classId}");
                }

                return Ok(classStudents);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }

        [HttpGet("time/{id}")]
        public async Task<ActionResult<Classes>> GetClassTime(int id)
        {
            var classTime = await _classesServices.GetClassTimeAsync(id);

            if (classTime == null)
            {
                return NotFound();
            }

            return classTime;
        }

        [HttpGet("classCourse/{id}")]
        public async Task<ActionResult<CourseDetailsDTO>> GetClassCourse(int id)
        {
            var classCourse = await _classesServices.GetClassCourseAsync(id);

            if (classCourse == null)
            {
                return NotFound();
            }

            return classCourse;
        }


        // PUT: api/Classes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpPut("updateClassDetails/{id}")]
        public async Task<IActionResult> UpdateClassDetails(int id, ClassDetailsDTO classDetailsDTO)
        {
            if (id <= 0 || classDetailsDTO == null)
            {
                return BadRequest("Invalid input parameters");
            }

            var classToUpdate = await _context.Classes.FindAsync(id);
            if (classToUpdate == null)
            {
                return NotFound();
            }

            classToUpdate.className = classDetailsDTO.ClassName ?? string.Empty;
            classToUpdate.classDescription = classDetailsDTO.classDescription ?? string.Empty;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("updateClassDetails/{id}")]
        public async Task<IActionResult> UpdateClassDetails(int id, [FromBody] Classes updatedClass)
        {
            if (id <= 0 || updatedClass == null)
            {
                return BadRequest("Invalid input parameters");
            }

            var classToUpdate = await _context.Classes.FindAsync(id);
            if (classToUpdate == null)
            {
                return NotFound($"Class with ID {id} not found.");
            }

            classToUpdate.className = updatedClass.className;
            classToUpdate.classDescription = updatedClass.classDescription;
            classToUpdate.Classroom = updatedClass.Classroom;

            await _context.SaveChangesAsync();

            return Ok($"Class with ID {id} updated successfully.");
        }

        [HttpPut("updateClassTime/{id}")]
        public async Task<IActionResult> UpdateClassTime(int id, ClassTimeDTO classTimeDTO)
        {

            throw new NotImplementedException();
            // if (id <= 0 || classTimeDTO == null)
            // {
            //     return BadRequest("Invalid input parameters");
            // }

            // var classToUpdate = await _context.Classes.FindAsync(id);
            // if (classToUpdate == null)
            // {
            //     return NotFound();
            // }

            // classToUpdate.classTime = classTimeDTO.ClassTime;

            // await _context.SaveChangesAsync();

            // return NoContent();
        }

        // post lecturer to class
        [HttpPost("addLecturerToClass/{classId}/{lecturerId}")]
        public async Task<IActionResult> AddLecturerToClass(int classId, int lecturerId)
        {
            if (classId <= 0 || lecturerId <= 0)
            {
                return BadRequest("Invalid input parameters");
            }

            try
            {
                var result = await _classesServices.AddLecturerToClassAsync(classId, lecturerId);

                if (result)
                {
                    return Ok($"Lecturer with ID: {lecturerId} added to class successfully.");
                }

                return StatusCode(500, "An unexpected error occurred while adding the lecturer to the class");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }


        [HttpPost("addStudentToClass/{classId}/{studentId}")]
        public async Task<IActionResult> AddStudentToClass(int classId, int studentId)
        {
            if (classId <= 0 || studentId <= 0)
            {
                return BadRequest("Invalid input parameters");
            }

            try
            {
                var result = await _classesServices.AddStudentToClassAsync(classId, studentId);

                if (result)
                {
                    return Ok($"Student with ID: {studentId} added to class successfully.");
                }

                return StatusCode(500, "An unexpected error occurred while adding the student to the class");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }

        // POST: api/Classes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Classes>> PostClasses(Classes classes)
        {
            _context.Classes.Add(classes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClasses", new { id = classes.classID }, classes);
        }

        // DELETE: api/Classes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClasses(int id)
        {
            var classes = await _context.Classes.FindAsync(id);
            if (classes == null)
            {
                return NotFound();
            }

            _context.Classes.Remove(classes);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("removeLecturer/{classId}/{lecturerId}")]
        public async Task<IActionResult> RemoveLecturerFromClass(int classId, int lecturerId)
        {
            var result = await _classesServices.RemoveLecturerFromClassAsync(classId, lecturerId);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("removeStudent/{classId}/{studentId}")]
        public async Task<IActionResult> RemoveStudentFromClass(int classId, int studentId)
        {
            var result = await _classesServices.RemoveStudentFromClassAsync(classId, studentId);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

