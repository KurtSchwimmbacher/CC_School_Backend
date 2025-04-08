using System;
using Code_CloudSchool.Data;
using Code_CloudSchool.DTOs;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.Models;
using Microsoft.EntityFrameworkCore;

namespace Code_CloudSchool.Services;

public class ClassesServices : IClassesServices
{
    private readonly AppDBContext _context;

    public ClassesServices(AppDBContext context)
    {
        _context = context;
    }

    public async Task<ClassDetailsDTO> GetClassDetailsAsync(int classId)
    {
        return await _context.Classes
            .Where(c => c.classID == classId)
            .Select(c => new ClassDetailsDTO
            {
                ClassId = c.classID,
                ClassName = c.className,
                classDescription = c.classDescription,
                CourseName = c.Courses != null ? c.Courses.courseName : null
            })
            .FirstOrDefaultAsync() ?? throw new KeyNotFoundException($"Class with ID {classId} not found");
    }

    public async Task<Classes> GetClassLecturersAsync(int classId)
    {
        if (classId <= 0)
        {
            throw new ArgumentException("Invalid Id");
        }
        var classExists = _context.Classes.Any(c => c.classID == classId);

        if (!classExists)
        {
            throw new KeyNotFoundException($"Class with Id {classId} does not exist");
        }

        return await _context.Classes
            .Include(c => c.Lecturers)
            .FirstOrDefaultAsync(c => c.classID == classId) ?? throw new KeyNotFoundException($"Class with ID {classId} does not exist or was not found");
    }

    public Task<Classes?> GetClassStudentsAsync(int classId)
    {
        if (classId <= 0)
        {
            throw new ArgumentException("Invalid Id");
        }
        var classExists = _context.Classes.Any(c => c.classID == classId);

        if (!classExists)
        {
            throw new KeyNotFoundException($"Class with Id {classId} does not exist");
        }

        return _context.Classes
            .Include(c => c.Student)
            .FirstOrDefaultAsync(c => c.classID == classId) ?? throw new KeyNotFoundException($"Class with ID {classId} does not exist or was not found");
    }

    public Task<Classes> GetClassTimeAsync(int classId)
    {
        if (classId <= 0)
        {
            throw new ArgumentException("Invalid Id");
        }
        var classExists = _context.Classes.Any(c => c.classID == classId);

        if (!classExists)
        {
            throw new KeyNotFoundException($"Class with Id {classId} does not exist");
        }

        return _context.Classes
            .Include(c => c.classTime)
            .FirstOrDefaultAsync(c => c.classID == classId) ?? throw new KeyNotFoundException($"Class with ID {classId} does not exist or was not found");
    }

    public Task<bool> RemoveLecturerFromClassAsync(int classId, int lecturerId)
    {
        if (classId <= 0 || lecturerId <= 0)
        {
            throw new ArgumentException("Invalid Id");
        }

        var classExists = _context.Classes.Any(c => c.classID == classId);

        if (!classExists)
        {
            throw new KeyNotFoundException($"Class with Id {classId} does not exist");
        }

        var lecturerExists = _context.Lecturers.Any(l => l.LecturerId == lecturerId);
        if (!lecturerExists)
        {
            throw new KeyNotFoundException($"Lecturer with Id {lecturerId} does not exist");
        }

        var lecturer = _context.Lecturers.Find(lecturerId);
        if (lecturer == null)
        {
            throw new KeyNotFoundException($"Lecturer with Id {lecturerId} does not exist");
        }
        _context.Lecturers.Remove(lecturer);
        _context.SaveChanges();

        return Task.FromResult(true); // Assuming the operation is successful save changes from the context and return true
    }

    public Task<bool> RemoveStudentFromClassAsync(int classId, string studentId)
    {
        if (classId <= 0 || studentId == null)
        {
            throw new ArgumentException("Invalid Id");
        }

        var classExists = _context.Classes.Any(c => c.classID == classId);

        if (!classExists)
        {
            throw new KeyNotFoundException($"Class with Id {classId} does not exist");
        }

        var studentExists = _context.Students.Any(s => s.StudentNumber == studentId);
        if (!studentExists)
        {
            throw new KeyNotFoundException($"Student with Id {studentId} does not exist");
        }

        var student = _context.Students.Find(studentId);
        if (student == null)
        {
            throw new KeyNotFoundException($"Student with Id {studentId} does not exist");
        }
        _context.Students.Remove(student);
        _context.SaveChanges();

        return Task.FromResult(true); // Assuming the operation is successful save changes from the context and return true
    }


    public Task<Classes> UpdateClassDetailsAsync(int classId, Classes classes)
    {
        if (classId <= 0 || classes == null)
        {
            throw new ArgumentException("Invalid Id");
        }

        var classExists = _context.Classes.Any(c => c.classID == classId);

        if (!classExists)
        {
            throw new KeyNotFoundException($"Class with Id {classId} does not exist");
        }

        _context.Classes.Update(classes);
        _context.SaveChanges();

        return Task.FromResult(classes); // Assuming the operation is successful save changes from the context and return the updated class
    }

    public Task<Classes> UpdateClassTimeAsync(int classId, Classes classes)
    {
        if (classId <= 0 || classes == null)
        {
            throw new ArgumentException("Invalid Id");
        }

        var classExists = _context.Classes.Any(c => c.classID == classId);

        if (!classExists)
        {
            throw new KeyNotFoundException($"Class with Id {classId} does not exist");
        }

        _context.Classes.Update(classes);
        _context.SaveChanges();

        return Task.FromResult(classes); // Assuming the operation is successful save changes from the context and return the updated class
    }

    async Task<LecturerDTO> IClassesServices.GetClassLecturersAsync(int classId)
    {
        throw new NotImplementedException();
    }

    Task<Student> IClassesServices.GetClassStudentsAsync(int classId)
    {
        throw new NotImplementedException();
    }

    public async Task<CourseDetailsDTO> GetClassCourseAsync(int classId)
    {
        if (classId <= 0)
        {
            throw new ArgumentException("Invalid Id");
        }
        var classExists = _context.Classes.Any(c => c.classID == classId);
        if (!classExists)
        {
            throw new KeyNotFoundException($"Class with Id {classId} does not exist");
        }
        return await _context.Classes
            .Where(c => c.classID == classId)
            .Select(c => new CourseDetailsDTO
            {
                CourseId = c.Courses != null ? c.Courses.Id : 0,
                CourseName = c.Courses != null ? c.Courses.courseName : null,
                CourseDescription = c.Courses != null ? c.Courses.courseDescription : null
            })
            .FirstOrDefaultAsync() ?? throw new KeyNotFoundException($"Class with ID {classId} not found");
    }
}
