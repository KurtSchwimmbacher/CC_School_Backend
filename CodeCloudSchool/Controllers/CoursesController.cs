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

/// <summary>
/// Courses Controller
/// </summary>
namespace Code_CloudSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly ICourseServices _courseServices;

        public CoursesController(AppDBContext context, ICourseServices courseServices)
        {
            _context = context;

            _courseServices = courseServices;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Courses>>> GetCourses()
        {
            return await _context.Courses.ToListAsync();
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Courses>> GetCourses(int id)
        {
            var courses = await _context.Courses
                .Include(c => c.Assignments) // Eagerly load related assignments
                .FirstOrDefaultAsync(c => c.Id == id);

            if (courses == null)
            {
                return NotFound();
            }

            return courses;
        }

        [HttpGet("courseDetails/{id}")]
        public async Task<ActionResult<CourseDetailsDTO>> GetCourseDetails(int id)
        {
            var courseDetails = await _courseServices.GetCourseDetailsAsync(id);

            if (courseDetails == null)
            {
                return NotFound();
            }

            return Ok(courseDetails);
        }

        [HttpGet("courses/classes/{id}")]
        public async Task<ActionResult> GetClassesForCourseAsync(int id)
        {
            var classesInCourse = await _courseServices.GetCourseDetailsAsync(id);

            if (classesInCourse == null)
            {
                return NotFound();
            }

            return Ok(classesInCourse);
        }

        [HttpGet("courses/majors/{id}")]
        public async Task<ActionResult>
        GetMajorsForCourseAsync(int id)
        {
            var majorsInCourse = await _courseServices.GetMajorsForCourseAsync(id);

            if (majorsInCourse == null)
            {
                return NotFound();
            }

            return Ok(majorsInCourse);
        }

        [HttpGet("studentsInCourse/{CourseId}")]
        public async Task<ActionResult> GetStudentsInCourseAsync(int CourseId)
        {
            var studentsInCourse = await _courseServices.GetStudentsInCourseAsync(CourseId);

            if (studentsInCourse == null)
            {
                return NotFound();
            }

            return Ok(studentsInCourse);
        }


        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourses(int id, Courses courses)
        {
            if (id != courses.Id)
            {
                return BadRequest();
            }

            _context.Entry(courses).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoursesExists(id))
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

        [HttpPut("courseDetails/{id}")]
        public async Task<ActionResult> UpdateCourseDetails(int id, [FromBody] CourseDetailsDTO courseDetails)
        {
            if (id <= 0 || courseDetails == null)
            {
                return BadRequest("invalid input");
            }

            var courseToUpdate = await _context.Courses.FindAsync(id);

            if (courseToUpdate == null)
            {
                return NotFound($"The Course with ID: {id} does not exist");
            }

            bool hasChanged = false;

            if (courseToUpdate.courseName != courseDetails.CourseName)
            {
                if (courseDetails.CourseName != null)
                {
                    courseToUpdate.courseName = courseDetails.CourseName;
                }

                hasChanged = true;
            }

            if (courseToUpdate.courseCode != courseDetails.CourseCode)
            {
                courseToUpdate.courseCode = courseDetails.CourseCode;

                hasChanged = true;
            }

            if (courseToUpdate.courseDescription != courseDetails.CourseDescription)
            {
                courseToUpdate.courseDescription = courseDetails.CourseDescription;

                hasChanged = true;
            }

            if (hasChanged)
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    return StatusCode(500, "An error occured saving these changes");
                }
            }

            return NoContent();
        }

        [HttpPut("courses/{courseId}/classes/{classId}")]
        public async Task<ActionResult> UpdateClassInCourse(int classId, int courseId, [FromBody] ClassDetailsDTO classDetails) //fetches the Id's from the http request 
        {

            if (courseId <= 0 || classDetails == null)
            {
                return BadRequest("Invalid input parameters");
            }

            //fetching the course/ searching for the course
            var course = await _context.Courses
                .Include(c => c.Classes)
                .FirstOrDefaultAsync(c => c.Id == courseId);

            if (course == null)
            {
                return NotFound($"Course with the ID: {courseId} does not exist or was not found");
            }

            //searching for the class 
            var classToUpdate = course.Classes.FirstOrDefault(c => c.classID == classDetails.ClassId);

            if (classToUpdate == null)
            {
                return NotFound($"Class with the ID:{classDetails.ClassId} does not exist in the course");
            }

            bool hasChanged = false;

            if (classToUpdate.className != classDetails.ClassName)
            {
                if (classDetails.ClassName != null)
                {
                    classToUpdate.className = classDetails.ClassName;
                }
                hasChanged = true;
            }
            if (classToUpdate.classDescription != classDetails.classDescription)
            {
                if (classDetails.classDescription != null)
                {
                    classToUpdate.classDescription = classDetails.classDescription;
                }
                hasChanged = true;
            }

            if (hasChanged)
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    return StatusCode(500, "An error occured whilst attempting to save the changes made.");
                }
            }
            return NoContent();
        }

        [HttpPut("courses/{courseId}/major/{majorId}")]
        public async Task<ActionResult> UpdateMajorInCourse(int majorId, int courseId, [FromBody] MajorDetailsDTO majorDetails)
        {
            if (courseId <= 0 || majorDetails == null)
            {
                return BadRequest("Invalid input parameters");
            }

            //fetching the course/ searching for the course
            var course = await _context.Courses
                .Include(c => c.Majors)
                .FirstOrDefaultAsync(c => c.Id == courseId);

            if (course == null)
            {
                return NotFound($"Course with the ID: {courseId} does not exist or was not found");
            }

            //searching for the class 
            var majorToUpdate = course.Majors.FirstOrDefault(m => m.Id == majorDetails.MajorId);

            if (majorToUpdate == null)
            {
                return NotFound($"Major with the ID:{majorDetails.MajorId} does not exist in the course");
            }

            bool hasChanged = false;

            if (majorToUpdate.MajorName != majorDetails.MajorName)
            {
                majorToUpdate.MajorName = majorDetails.MajorName;

                hasChanged = true;
            }
            if (majorToUpdate.MajorCode != majorDetails.MajorCode)
            {
                majorToUpdate.MajorCode = majorDetails.MajorCode;

                hasChanged = true;
            }
            if (majorToUpdate.MajorDescription != majorDetails.MajorDescription)
            {
                if (majorDetails.MajorDescription != null)
                {
                    majorToUpdate.MajorDescription = majorDetails.MajorDescription;
                }

                hasChanged = true;
            }
            if (majorToUpdate.CreditsRequired != majorDetails.CreditsRequired)
            {
                majorToUpdate.CreditsRequired = majorDetails.CreditsRequired;

                hasChanged = true;
            }

            if (hasChanged)
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    return StatusCode(500, "An error occured whilst attempting to save the changes made.");
                }
            }
            return NoContent();
        }




        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create-course")]
        public async Task<ActionResult<Courses>> PostCourses(Courses courses)
        {
            _context.Courses.Add(courses);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourses", new { id = courses.Id }, courses);
        }

        [HttpPost("addMajorToCourse/{courseId}/major/{majorId}")]
        public async Task<ActionResult> AddMajorToCourse(int courseId, int majorId)
        {
            if (courseId <= 0 || majorId <= 0)
            {
                return BadRequest("Invalid input parameters");
            }

            var course = await _context.Courses
                .Include(c => c.Majors)
                .FirstOrDefaultAsync(c => c.Id == courseId);

            if (course == null)
            {
                return NotFound($"Course with the ID: {courseId} does not exist or was not found");
            }

            var majorToAdd = await _context.Majors.FindAsync(majorId);

            if (majorToAdd == null)
            {
                return NotFound($"Major with the ID:{majorId} does not exist");
            }

            course.Majors.Add(majorToAdd);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPost("addClassToCourse/{courseId}/class/{classId}")]
        public async Task<ActionResult> AddClassToCourse(int courseId, int classId)
        {
            if (courseId <= 0 || classId <= 0)
            {
                return BadRequest("Invalid input parameters");
            }

            var course = await _context.Courses
                .Include(c => c.Classes)
                .FirstOrDefaultAsync(c => c.Id == courseId);

            if (course == null)
            {
                return NotFound($"Course with the ID: {courseId} does not exist or was not found");
            }

            var classToAdd = await _context.Classes.FindAsync(classId);

            if (classToAdd == null)
            {
                return NotFound($"Class with the ID:{classId} does not exist");
            }

            course.Classes.Add(classToAdd);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Post Students into Course
        [HttpPost("addStudentToCourse/{courseId}/student/{studentId}")]
        public async Task<ActionResult> AddStudentToCourse(int courseId, int studentId)
        {
            if (courseId <= 0 || studentId <= 0)
            {
                return BadRequest("Invalid input parameters");
            }

            try
            {
                var result = await _courseServices.AddStudentToCourseAsync(courseId, studentId);

                if (result)
                {
                    return Ok($"Student with ID: {studentId} has successfully been added to the Course with ID: {courseId}");
                }

                return StatusCode(500, "An unexpected error occurred while adding the student to the course");
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


        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourses(int id)
        {
            var courses = await _context.Courses.FindAsync(id);
            if (courses == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(courses);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CoursesExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }

        [HttpDelete("courses/{courseId}/major/{majorId}")]
        public async Task<ActionResult> RemoveMajorForCourse(int courseId, int majorId)
        {
            if (courseId <= 0 || majorId <= 0)
            {
                return BadRequest("Invalid input parameters");
            }

            var course = await _context.Courses
                .Include(c => c.Majors)
                .FirstOrDefaultAsync(c => c.Id == courseId);

            if (course == null)
            {
                return NotFound($"Course with the ID: {courseId} does not exist or was not found");
            }

            var majorToRemove = course.Majors.FirstOrDefault(m => m.Id == majorId);

            if (majorToRemove == null)
            {
                return NotFound($"Major with the ID:{majorId} does not exist in the course");
            }

            _context.Majors.Remove(majorToRemove);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("courses/{courseId}/classes/{classesId}")]
        public async Task<ActionResult> RemoveClassFromCourse(int courseId, int classesId)
        {
            if (courseId <= 0 || classesId <= 0)
            {
                return BadRequest("Invalid input parameters");
            }

            var course = await _context.Courses
                .Include(c => c.Classes)
                .FirstOrDefaultAsync(c => c.Id == courseId);

            if (course == null)
            {
                return NotFound($"Course with the ID: {courseId} does not exist or was not found");
            }

            var classToRemove = course.Classes.FirstOrDefault(c => c.classID == classesId);

            if (classToRemove == null)
            {
                return NotFound($"Class with the ID:{classesId} does not exist in the course");
            }

            _context.Classes.Remove(classToRemove);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("deleteStudentFromCourse/{courseId}/student/{studentId}")]
        public async Task<ActionResult> RemoveStudentFromCourse(int courseId, int studentId)
        {
            if (courseId <= 0 || studentId <= 0)
            {
                return BadRequest("Invalid input parameters");
            }

            try
            {
                var result = await _courseServices.RemoveStudentInCourseAsync(courseId, studentId);

                if (result)
                {
                    return Ok($"Student with ID: {studentId} has successfully been removed from the Course with ID: {courseId}");
                }

                return StatusCode(500, "An unexpected error occurred while removing the student from the course");
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

    }
}
