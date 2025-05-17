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

    public async Task<ClassLecturerDTO> GetClassLecturersAsync(int classId)
    {
        if (classId <= 0)
        {
            throw new ArgumentException("Invalid Id");
        }

        // Check if the class exists
        var classExists = await _context.Classes.AnyAsync(c => c.classID == classId);
        if (!classExists)
        {
            throw new KeyNotFoundException($"Class with Id {classId} does not exist");
        }

        // Fetch the class with its lecturers
        var classWithLecturers = await _context.Classes
            .Include(c => c.Lecturers)
            .FirstOrDefaultAsync(c => c.classID == classId);

        if (classWithLecturers == null || classWithLecturers.Lecturers == null || !classWithLecturers.Lecturers.Any())
        {
            throw new KeyNotFoundException($"No lecturers found for the class with ID {classId}");
        }

        // Map the lecturers to a LecturerDTO
        var classLecturerDTO = new ClassLecturerDTO
        {
            ClassId = classId,
            Lecturers = classWithLecturers.Lecturers.Select(l => new LecturerDTO
            {
                LecturerId = l.LecturerId,
                LecName = l.Name,
                LecEmail = l.LecEmail,
                Department = l.Department
            }).ToList()
        };

        return classLecturerDTO;
    }

    public async Task<bool> AddLecturerToClassAsync(int classId, int lecturerId)
    {
        if (classId <= 0 || lecturerId <= 0)
        {
            throw new ArgumentException("Invalid input parameters");
        }

        // Check if the class exists
        var classExists = await _context.Classes
            .Include(c => c.Lecturers) // Include the Lecturers navigation property
            .FirstOrDefaultAsync(c => c.classID == classId);

        if (classExists == null)
        {
            throw new KeyNotFoundException($"Class with ID {classId} does not exist");
        }

        // Check if the lecturer exists
        var lecturerExists = await _context.Lecturers.FindAsync(lecturerId);
        if (lecturerExists == null)
        {
            throw new KeyNotFoundException($"Lecturer with ID {lecturerId} does not exist");
        }

        // Check if the lecturer is already associated with the class
        if (classExists.Lecturers.Any(l => l.LecturerId == lecturerId))
        {
            throw new InvalidOperationException($"Lecturer with ID {lecturerId} is already assigned to the class");
        }

        // Add the lecturer to the class
        classExists.Lecturers.Add(lecturerExists);

        // Save changes to the database
        await _context.SaveChangesAsync();

        return true; 
    }


    public async Task<bool> AddStudentToClassAsync(int classId, int studentId)
    {
        if (classId <= 0 || studentId <= 0)
        {
            throw new ArgumentException("Invalid input parameters");
        }
        
        // check if the class exists
        var classExists = _context.Classes
            .Include(c => c.Student) // Include the Students navigation property
            .FirstOrDefault(c => c.classID == classId);

        if (classExists == null)
        {
            throw new KeyNotFoundException($"Class with ID {classId} does not exists");
        }

        // check if the student exists
        var studentExists = _context.Students.Find(studentId);
        if (studentExists == null)
        {
            throw new KeyNotFoundException($"Student with ID {studentId} does not exists");
        }

        // check if the student is already associated with the class
        if (classExists.Student.Any(s => s.UserId == studentId))
        {
            throw new InvalidOperationException($"Student with ID {studentId} is already assigned to the class");
        }

        // add student to the class
        classExists.Student.Add(studentExists);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<Student>> GetClassStudentsAsync(int classId)
    {
        if (classId <= 0)
        {
            throw new ArgumentException("Invalid Id");
        }

        // Check if the class exists
        var classExists = await _context.Classes.AnyAsync(c => c.classID == classId);
        if (!classExists)
        {
            throw new KeyNotFoundException($"Class with Id {classId} does not exist");
        }

        // Fetch the students associated with the class
        var students = await _context.Classes
            .Where(c => c.classID == classId)
            .Include(c => c.Student) // Ensure the Students navigation property is loaded
            .SelectMany(c => c.Student) // Flatten the collection of students
            .ToListAsync();

        if (students == null || !students.Any())
        {
            throw new KeyNotFoundException($"No students found for the class with ID {classId}");
        }

        return students;
        
    }

    public Task<Classes> GetClassTimeAsync(int classId)
    {
        throw new NotImplementedException();
        // if (classId <= 0)
        // {
        //     throw new ArgumentException("Invalid Id");
        // }
        // var classExists = _context.Classes.Any(c => c.classID == classId);

        // if (!classExists)
        // {
        //     throw new KeyNotFoundException($"Class with Id {classId} does not exist");
        // }

        // return _context.Classes
        //     .Include(c => c.classTime)
        //     .FirstOrDefaultAsync(c => c.classID == classId) ?? throw new KeyNotFoundException($"Class with ID {classId} does not exist or was not found");
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

    public Task<bool> RemoveStudentFromClassAsync(int classId, int studentId)
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

        var studentExists = _context.Students.Any(s => s.UserId == studentId);
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
