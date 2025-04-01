using System;
using Code_CloudSchool.DTOs;
using Code_CloudSchool.Models;

namespace Code_CloudSchool.Interfaces;

// Majors contract specifiying class requirements 
public interface IMajorServices
{
    //listing functionality that this service should be able to do

    public Task<Majors> GetMajorDetails(int majorId, MajorDetailsDTO majorDetailsDTO);

    // returns true if update correct
    public Task<bool> UpdateMajorDetails(int majorId, MajorDetailsDTO majorDetailsDTO);

    public Task<int> GetMajorCredits(int majorId, MajorCreditsDTO majorCredits);

    public Task<bool> UpdateMajorCredits(int majorId, MajorCreditsDTO majorCredits);

    public Task<Courses> GetCoursesByMajor(int majorId);

    public Task<Student> GetStudentsByMajor(int majorId);

    //TODO: 
    /*
        GET, POST, PUT, DELETE -> MAJOR 

        Get major Details ./
        Get major description 
        Get major credit 
        Get Major Courses 
        Get Major Students 
        Update Major Courses 
        Uppdate Major Details -> dto
        Update Major Description -> dto
        Update Major Credits -> dto 
        
        addCourse to Major
        remove course from major 
        
    */
}
