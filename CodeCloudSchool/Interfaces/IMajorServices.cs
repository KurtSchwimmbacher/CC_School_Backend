using System;
using System.Collections;
using Code_CloudSchool.DTOs;
using Code_CloudSchool.Models;

namespace Code_CloudSchool.Interfaces;

// Majors contract specifiying class requirements 
public interface IMajorServices
{
    //listing functionality that this service should be able to do

    //Major Details 
    public Task<MajorDetailsDTO> GetMajorDetails(int majorId);

    // returns true if update correct
    public Task<bool> UpdateMajorDetailsAsync(int majorId, MajorDetailsDTO majorDetailsDTO);

    // major credits
    public Task<int> GetMajorCreditsAsync(int majorId);

    public Task<bool> UpdateMajorCreditsAsync(int majorId, MajorCreditsDTO majorCredits);

    //courses 
    public Task<List<Courses>> GetCoursesByMajorAsync(int majorId);

    public Task<List<Student>> GetStudentsByMajorAsync(int majorId);

    public Task<Majors> CreateMajorAsync(MajorDetailsDTO majorCreateDTO);
    
    public Task<bool> AddStudentToMajorAsync(int majorId, int studentId);

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
