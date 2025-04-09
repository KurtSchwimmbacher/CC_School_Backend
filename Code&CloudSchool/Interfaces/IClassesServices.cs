using System;
using Code_CloudSchool.DTOs;
using Code_CloudSchool.Models;

namespace Code_CloudSchool.Interfaces;

/// <summary>
/// Specifying functinality requirements 
/// </summary>
public interface IClassesServices
{
    public Task<ClassDetailsDTO> GetClassDetailsAsync(int classId);
    public Task<Classes> GetClassTimeAsync(int classId);
    public Task<LecturerDTO> GetClassLecturersAsync(int classId);
    public Task<Student> GetClassStudentsAsync(int classId);
    public Task<CourseDetailsDTO> GetClassCourseAsync(int classId);


    public Task<Classes> UpdateClassDetailsAsync(int classId, Classes classes);
    public Task<Classes> UpdateClassTimeAsync(int classId, Classes classes);

    public Task<bool> RemoveStudentFromClassAsync(int classId, string studentNo);
    public Task<bool> RemoveLecturerFromClassAsync(int classId, int lecturerId);

    //TODO: 
    /*
        GET, POST, PUT, DELETE -> Courses 
        
        Get Class Details
        Get Class Time 
        Get Class Lecturers 
        Get Course the class is under 
        Get Class Students 

        Update Class Details 
        Update Class Time 
        Update Class Students 
        Update Class Lecturers 
        Update Class Course 

        remove student from class
        remove Lectuer from class 
        remove class from course 
        
    */
}
