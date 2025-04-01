using System;
using Code_CloudSchool.DTOs;
using Code_CloudSchool.Models;

namespace Code_CloudSchool.Interfaces;

public interface ICourseServices
{

    public Task<CourseDetailsDTO> GetCourseDetailsAsync(int courseId);
    public Task<bool> UpdateCourseDetailsAsync(int courseId, CourseDetailsDTO courseDetailsDTO);
    public Task<Courses> CreateNewCourseAsync(CourseDetailsDTO courseDetailsDTO);

    //majors 
    public Task<List<Majors>> GetMajorsForCourseAsync(int courseId);
    public Task<bool> UpdateMajorsForCourseAsync(int courseId);
    public Task<bool> RemoveMajorsForCourseAsync(int courseId);

    //students 
    public Task<List<Students>> GetStudentsInCourseAsync(int courseId);
    public Task<bool> UpdateStudentsInCourseAsync(int courseId);
    public Task<bool> RemoveStudentsInCourseAsync(int courseId);

    //classes 
    public Task<List<Classes>> GetClassesForCourseAsync(int courseId);
    public Task<bool> UpdateClassesForCourseAsync(int courseId);
    public Task<bool> RemoveClassesCourseAsync(int courseId);

}
