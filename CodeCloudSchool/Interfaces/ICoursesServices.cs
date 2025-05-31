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
    public Task<bool> AddStudentToCourseAsync(int courseId, int studentId);


    //for descriptive details
    // Fetch full course details (JSON stored in courseDescription)
    Task<CourseDescriptDetailsDTO?> GetDescriptiveDetails(int courseId);

    // Overwrite the course details completely
    Task<bool> UpdateDescriptiveCourseDetails(int courseId, CourseDescriptDetailsDTO updatedDetails);

    // Add new course details (when courseDescription is currently null or empty)
    Task<bool> AddDescriptiveDetails(int courseId, CourseDescriptDetailsDTO descriptiveDetails);

    //Handle PATCH-style partial updates if needed
    Task<bool> PatchDescriptiveDetails(int courseId, CourseDescriptDetailsDTO partialDetails);

}
