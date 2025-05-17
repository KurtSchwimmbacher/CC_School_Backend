using System;
using Code_CloudSchool.Data;
using Code_CloudSchool.DTOs;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.Models;
using Microsoft.EntityFrameworkCore;

namespace Code_CloudSchool.Services;

public class CoursesServices : ICourseServices
{
    private readonly AppDBContext _context;

    public CoursesServices(AppDBContext context)
    {
        _context = context;
    }

    public async Task<bool> AddMajorToCourseAsync(int courseId, MajorDetailsDTO majorDetails)
    {
        var course = _context.Courses
            .Include(c => c.Majors)
            .FirstOrDefault(c => c.Id == courseId);
        if (course == null)
        {
            throw new KeyNotFoundException($"Course with Id {courseId} does not exist");
        }
        var newMajor = new Majors
        {
            MajorName = majorDetails.MajorName,
            MajorDescription = majorDetails.MajorDescription ?? string.Empty
        };


        course.Majors.Add(newMajor);
        await _context.SaveChangesAsync();

        return true;


    }

    public async Task<bool> AddStudentToCourseAsync(int courseId, int studentId)
    {
        var course = await _context.Courses
            .Include(c => c.Student)
            .FirstOrDefaultAsync(c => c.Id == courseId);

        if (course == null)
        {
            throw new KeyNotFoundException($"Course with Id {courseId} does not exist");
        }

        var student = await _context.Students
            .FirstOrDefaultAsync(s => s.UserId == studentId);

        if (student == null)
        {
            throw new KeyNotFoundException($"Student with Id {studentId} does not exist");
        }

        // Ensure the Student collection is initialized
        if (course.Student == null)
        {
            course.Student = new List<Student>();
        }

        // Check if the student is already in the course
        if (course.Student.Any(s => s.UserId == studentId))
        {
            throw new InvalidOperationException($"Student with Id {studentId} is already enrolled in the course");
        }

        course.Student.Add(student);
        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<Courses> CreateNewCourseAsync(CourseDetailsDTO courseDTO)
    {
        var newCourse = _context.Courses.Add(new Courses
        {
            courseName = courseDTO.CourseName ?? string.Empty,
            courseCode = courseDTO.CourseCode,
            courseDescription = courseDTO.CourseDescription

        });
        await _context.SaveChangesAsync();

        return newCourse.Entity;
    }

    public async Task<List<Classes>> GetClassesForCourseAsync(int courseId)
    {
        if (courseId <= 0)
        {
            throw new ArgumentException("Invalid Id");
        }

        var courseExists = await _context.Courses.AnyAsync(c => c.Id == courseId);

        if (!courseExists)
        {
            throw new KeyNotFoundException($"Course with Id {courseId} does not exist");
        }

        return await _context.Courses
            .Where(c => c.Id == courseId)
            .SelectMany(c => c.Classes)
            .ToListAsync();
    }

    public async Task<CourseDetailsDTO> GetCourseDetailsAsync(int courseId)
    {
        return await _context.Courses
            .Where(c => c.Id == courseId)
            .Select(c => new CourseDetailsDTO
            {
                CourseId = c.Id,
                CourseName = c.courseName,
                CourseCode = c.courseCode,
                CourseDescription = c.courseDescription
            })
            .FirstOrDefaultAsync() ?? throw new KeyNotFoundException($"Course with Id {courseId} does not exist");
    }

    public async Task<List<Majors>> GetMajorsForCourseAsync(int courseId)
    {
        if (courseId <= 0)
        {
            throw new ArgumentException("Invalid Id");
        }

        var courseExists = await _context.Courses.AnyAsync(c => c.Id == courseId);

        if (!courseExists)
        {
            throw new KeyNotFoundException($"Course with Id {courseId} does not exist");
        }

        return await _context.Courses
            .Where(c => c.Id == courseId)
            .SelectMany(c => c.Majors)
            .ToListAsync();
    }

    public async Task<List<Student>> GetStudentsInCourseAsync(int courseId)
    {
        if (courseId <= 0)
        {
            throw new ArgumentException("Invalid course ID", nameof(courseId));
        }

        // Retrieve the course and its students in a single query
        var students = await _context.Courses
            .Where(c => c.Id == courseId)
            .SelectMany(c => c.Student)
            .ToListAsync();

        if (!students.Any())
        {
            throw new KeyNotFoundException($"Course with Id {courseId} does not exist or has no students");
        }

        return students;
    }


    public async Task<bool> RemoveClassFromCourseAsync(int courseId, ClassDetailsDTO classDTO)
    {
        if (courseId <= 0 || classDTO == null)
        {
            throw new ArgumentException("Invalid input parameters");
        }

        var course = await _context.Courses
            .Include(c => c.Classes)
            .FirstOrDefaultAsync(c => c.Id == courseId);

        if (course == null)
        {
            throw new KeyNotFoundException($"Course with Id {courseId} does not exist");
        }

        var existingClass = course.Classes.FirstOrDefault(c => c.classID == classDTO.ClassId);

        if (existingClass == null)
        {
            throw new KeyNotFoundException($"Class with Id {classDTO.ClassId} does not exist in the course");
        }

        _context.Classes.Remove(existingClass);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveMajorForCourseAsync(int courseId, MajorDetailsDTO majorDetails)
    {
        if (courseId <= 0 || majorDetails == null)
        {
            throw new ArgumentException("Invalid input parameters");
        }

        var course = await _context.Courses
            .Include(c => c.Majors)
            .FirstOrDefaultAsync(c => c.Id == courseId);

        if (course == null)
        {
            throw new KeyNotFoundException($"Course with Id {courseId} does not exist");
        }

        var existingMajor = course.Majors.FirstOrDefault(c => c.Id == majorDetails.MajorId);

        if (existingMajor == null)
        {
            throw new KeyNotFoundException($"Class with Id {majorDetails.MajorId} does not exist in the course");
        }

        existingMajor.MajorName = majorDetails.MajorName;

        _context.Majors.Remove(existingMajor);
        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> UpdateClassForCourseAsync(int courseId, ClassDetailsDTO classDTO)
    {
        if (courseId <= 0 || classDTO == null)
        {
            throw new ArgumentException("Invalid input parameters");
        }

        var course = await _context.Courses
            .Include(c => c.Classes)
            .FirstOrDefaultAsync(c => c.Id == courseId);

        if (course == null)
        {
            throw new KeyNotFoundException($"Course with Id {courseId} does not exist");
        }

        var existingClass = course.Classes.FirstOrDefault(c => c.classID == classDTO.ClassId);

        if (existingClass == null)
        {
            throw new KeyNotFoundException($"Class with Id {classDTO.ClassId} does not exist in the course");
        }

        existingClass.className = classDTO.ClassName ?? string.Empty;
        existingClass.classDescription = classDTO.classDescription ?? string.Empty;

        _context.Classes.Update(existingClass);
        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> UpdateCourseDetailsAsync(int courseId, CourseDetailsDTO courseDetailsDTO)
    {
        if (courseDetailsDTO == null)
        {
            throw new ArgumentNullException(nameof(courseDetailsDTO));
        }

        var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId) ?? throw new KeyNotFoundException($"Course with the following Id: {courseId} was not found");

        if (course.courseName != courseDetailsDTO.CourseName)
        {
            course.courseName = courseDetailsDTO.CourseName ?? string.Empty;
        }
        if (course.courseCode != courseDetailsDTO.CourseCode)
        {
            course.courseCode = courseDetailsDTO.CourseCode;
        }
        if (course.courseDescription != courseDetailsDTO.CourseDescription)
        {
            course.courseDescription = courseDetailsDTO.CourseDescription;
        }

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateMajorForCourseAsync(int courseId, MajorDetailsDTO majorDetails)
    {
        if (courseId <= 0 || majorDetails == null)
        {
            throw new ArgumentException("Invalid input parameters");
        }

        var course = await _context.Courses
            .Include(c => c.Majors)
            .FirstOrDefaultAsync(c => c.Id == courseId);

        if (course == null)
        {
            throw new KeyNotFoundException($"Course with Id {courseId} does not exist");
        }

        var existingMajor = course.Majors.FirstOrDefault(c => c.Id == majorDetails.MajorId);

        if (existingMajor == null)
        {
            throw new KeyNotFoundException($"Class with Id {majorDetails.MajorId} does not exist in the course");
        }

        existingMajor.MajorName = majorDetails.MajorName;
        existingMajor.MajorDescription = existingMajor.MajorDescription;


        _context.Majors.Update(existingMajor);
        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> RemoveStudentInCourseAsync(int courseId, int studentId)
    {
        if (courseId <= 0 || studentId <= 0)
        {
            throw new ArgumentException("Invalid input parameters");
        }

        var course = _context.Courses
            .Include(c => c.Student)
            .FirstOrDefault(c => c.Id == courseId);

        if (course == null)
        {
            throw new KeyNotFoundException($"Course with Id {courseId} does not exist");
        }

        var student = course.Student.FirstOrDefault(s => s.UserId == studentId);

        if (student == null)
        {
            throw new KeyNotFoundException($"Student with Id {studentId} does not exist in the course");
        }

        course.Student.Remove(student);
        await _context.SaveChangesAsync();

        return true;
    }

}
