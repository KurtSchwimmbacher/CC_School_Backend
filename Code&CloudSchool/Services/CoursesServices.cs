using System;
using Code_CloudSchool.Data;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.Models;

namespace Code_CloudSchool.Services;

public class CoursesServices : ICourseServices
{
    private readonly AppDBContext _context;
    public CoursesServices(AppDBContext context)
    {
        _context = context;
    }
    public Task<Courses?> CourseCodeCheck(string courseName)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteCourse(int courseId)
    {
        throw new NotImplementedException();
    }

    public Task<Courses> GetCourseById(int courseId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Courses>> GetCourses()
    {
        throw new NotImplementedException();
    }

    public Task<Courses> RegisterCourse(Courses course)
    {
        throw new NotImplementedException();
    }

    public Task<Courses> UpdateCourse(Courses course)
    {
        throw new NotImplementedException();
    }
}
