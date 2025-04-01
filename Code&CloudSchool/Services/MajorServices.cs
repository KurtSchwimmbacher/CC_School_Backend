using System;
using Code_CloudSchool.Data;
using Code_CloudSchool.DTOs;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.Models;
using Microsoft.EntityFrameworkCore;

namespace Code_CloudSchool.Services;

public class MajorServices : IMajorServices
{
    private readonly AppDBContext _context;

    public MajorServices(AppDBContext context)
    {
        _context = context;
    }

    public async Task<MajorDetailsDTO> GetMajorDetails(int majorId)
    {

        return await _context.Majors
        .Where(m => m.Id == majorId)
        .Select(m => new MajorDetailsDTO  // Project directly to DTO
        {
            MajorId = m.Id,
            MajorName = m.MajorName,      // Direct property access
            MajorCode = m.MajorCode,      // No Include needed for simple fields
            MajorDescription = m.MajorDescription,
            CreditsRequired = m.CreditsRequired
        })
        .FirstOrDefaultAsync()
        ?? throw new KeyNotFoundException($"Major {majorId} not found");
    }

    public async Task<Majors> CreateMajorAsync(MajorDetailsDTO major)
    {
        var newMajor = _context.Majors.Add(new Majors
        {
            MajorName = major.MajorName,
            MajorCode = major.MajorCode,
            MajorDescription = major.MajorDescription,
            CreditsRequired = major.CreditsRequired
        });

        await _context.SaveChangesAsync();

        return newMajor.Entity;
    }

    public async Task<int> GetMajorCreditsAsync(int majorId)
    {
        var major = await _context.Majors
            .FirstOrDefaultAsync(m => m.Id == majorId);

        if (major == null)
        {
            throw new KeyNotFoundException($"Majorr with this ID: {majorId}, does not exist");
        }

        return major.CreditsRequired ?? throw new InvalidOperationException($"No credits defined for major {major.MajorName}");
    }

    public Task<bool> UpdateMajorCreditsAsync(int majorId, MajorCreditsDTO majorCredits)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateMajorDetailsAsync(int majorId, MajorDetailsDTO majorDetailsDTO)
    {
        throw new NotImplementedException();
    }

    async Task<List<Courses>> IMajorServices.GetCoursesByMajorAsync(int majorId)
    {
        if (majorId <= 0)
        {
            throw new ArgumentException("Invalid Id");
        }

        var majorExists = await _context.Majors.AnyAsync(m => m.Id == majorId);

        if (!majorExists)
        {
            throw new KeyNotFoundException($"Major with Id {majorId} does not exist");
        }

        return await _context.Majors
            .Where(m => m.Id == majorId)
            .SelectMany(m => m.Courses)
            .ToListAsync();
    }

    async Task<List<Student>> IMajorServices.GetStudentsByMajorAsync(int majorId)
    {
        if (majorId <= 0)
        {
            throw new ArgumentException("Invalid Id");
        }

        var majorExists = await _context.Majors.AnyAsync(m => m.Id == majorId);

        if (!majorExists)
        {
            throw new KeyNotFoundException($"Major with Id {majorId} does not exist");
        }

        return await _context.Majors
            .Where(m => m.Id == majorId)
            .SelectMany(m => m.Students)
            .ToListAsync();
    }

}
