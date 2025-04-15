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
    public Task<bool> UpdateMajorForCourseAsync(int courseId, MajorDetailsDTO majorDetails);
    public Task<bool> RemoveMajorForCourseAsync(int courseId, MajorDetailsDTO majorDetails);

    //students 
    public Task<List<Student>> GetStudentsInCourseAsync(int courseId);

    public Task<bool> RemoveStudentInCourseAsync(int courseId, int studentId);

    //classes 
    public Task<List<Classes>> GetClassesForCourseAsync(int courseId);
    public Task<bool> UpdateClassForCourseAsync(int courseId, ClassDetailsDTO classDTO);
    public Task<bool> RemoveClassFromCourseAsync(int courseId, ClassDetailsDTO classDTO);

    //Add

    public Task<bool> AddMajorToCourseAsync(int courseId, MajorDetailsDTO majorDetails);
    public Task<bool> AddStudentToCourseAsync(int courseId, string studentNo);


}
