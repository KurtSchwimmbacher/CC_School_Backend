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
            return await _context.Classes.ToListAsync();
        }

        // GET: api/Classes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Classes>> GetClasses(int id)
        {
            var classes = await _context.Classes.FindAsync(id);

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
                    CourseName = c.Courses.courseName
                })
                .FirstOrDefaultAsync();

            if (classDetails == null)
            {
                return NotFound();
            }

            return classDetails;
        }

        [HttpGet("lecturers/{id}")]
        public async Task<ActionResult<Classes>> GetClassLecturers(int id)
        {
            var classLecturers = await _classesServices.GetClassLecturersAsync(id);

            if (classLecturers == null)
            {
                return NotFound();
            }

            return classLecturers;
        }

        [HttpGet("students/{id}")]
        public async Task<ActionResult<Classes>> GetClassStudents(int id)
        {
            var classStudents = await _classesServices.GetClassStudentsAsync(id);

            if (classStudents == null)
            {
                return NotFound();
            }

            return classStudents;
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
        public async Task<ActionResult<Classes>> GetClassCourse(int id)
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

            classToUpdate.className = classDetailsDTO.ClassName;
            classToUpdate.classDescription = classDetailsDTO.classDescription;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("updateClassTime/{id}")]
        public async Task<IActionResult> UpdateClassTime(int id, ClassTimeDTO classTimeDTO)
        {
            if (id <= 0 || classTimeDTO == null)
            {
                return BadRequest("Invalid input parameters");
            }

            var classToUpdate = await _context.Classes.FindAsync(id);
            if (classToUpdate == null)
            {
                return NotFound();
            }

            classToUpdate.classTime = classTimeDTO.ClassTime;

            await _context.SaveChangesAsync();

            return NoContent();
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

/* 

[HttpPut("{id}")]
        public async Task<IActionResult> PutClasses(int id, Classes classes)
        {
            if (id != classes.classID)
            {
                return BadRequest();
            }

            _context.Entry(classes).State = EntityState.Modified;

            var classesExists = await _context.Classes.AnyAsync(c => c.classID == id);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!classesExists(id))
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
*/