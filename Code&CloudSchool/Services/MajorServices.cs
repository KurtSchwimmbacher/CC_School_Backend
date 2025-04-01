using System;
using Code_CloudSchool.Data;
using Code_CloudSchool.DTOs;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.Models;

namespace Code_CloudSchool.Services;

public class MajorServices : IMajorServices
{
    private readonly AppDBContext _context;

    public MajorServices(AppDBContext context)
    {
        _context = context;
    }


    public Task<Courses> GetCoursesByMajor(int majorId)
    {

        // 1st. Fetch major by ID
        // 2nd. get courses for corresponding major
        // 3rd. return courses
    }

    public Task<int> GetMajorCredits(int majorId, MajorCreditsDTO majorCredits)
    {
        throw new NotImplementedException();
    }


    public Task<Majors> GetMajorDetails(int majorId, MajorDetailsDTO majorDetailsDTO)
    {
        throw new NotImplementedException();
    }

    public Task<Student> GetStudentsByMajor(int majorId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateMajorCredits(int majorId, MajorCreditsDTO majorCredits)
    {
        throw new NotImplementedException();
    }


    public Task<bool> UpdateMajorDetails(int majorId, MajorDetailsDTO majorDetailsDTO)
    {
        throw new NotImplementedException();
    }
}
