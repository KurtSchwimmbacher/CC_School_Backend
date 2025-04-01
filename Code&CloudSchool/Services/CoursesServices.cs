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
    public async Task<Courses> CreateNewCourseAsync(CourseDetailsDTO courseDTO)
    {
        var newCourse = _context.Courses.Add(new Courses
        {
            courseName = courseDTO.CourseName,
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

    public async Task<List<Students>> GetStudentsInCourseAsync(int courseId)
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
            .SelectMany(c => c.Students)
            .ToListAsync();
    }

    public Task<bool> RemoveClassesCourseAsync(int courseId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveMajorsForCourseAsync(int courseId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveStudentsInCourseAsync(int courseId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateClassesForCourseAsync(int courseId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateCourseDetailsAsync(int courseId, CourseDetailsDTO courseDetailsDTO)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateMajorsForCourseAsync(int courseId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateStudentsInCourseAsync(int courseId)
    {
        throw new NotImplementedException();
    }

}
