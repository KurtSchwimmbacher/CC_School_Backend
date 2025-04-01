using System;
using Code_CloudSchool.Models;

namespace Code_CloudSchool.Interfaces;

public interface ICourseServices
{
    public Task<Courses> RegisterCourse(Courses course);
    public Task<Courses> GetCourseById(int courseId);

    public Task<Courses?> CourseCodeCheck(string courseName);
    public Task<List<Courses>> GetCourses();
    public Task<Courses> UpdateCourse(Courses course);
    public Task<bool> DeleteCourse(int courseId);

    //TODO: 
    /*
        GET, POST, PUT, DELETE -> Courses 

        Get Course Details
        Get Course description 
        Get Course code 
        Get Course Major 
        Get Course Classes 
        Get Course Students 
        Update Courses classes 
        Uppdate Curse Details
        Update Course Description 
        Update Course Code 
        
        addClass to course
        remove class from course 
        
    */

}
