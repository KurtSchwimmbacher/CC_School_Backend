using System;
using Code_CloudSchool.Data;
using Code_CloudSchool.DTOs;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.Models;
using Microsoft.EntityFrameworkCore;

namespace Code_CloudSchool.Services;

public class StudentReEnrollService : IStudentReEnroll
{

    private readonly AppDBContext _context;

    public StudentReEnrollService(AppDBContext context)
    {
        _context = context;
    }

    public async Task<bool> UpdateStudentYearLevel(string studentNumber, StudentReEnrollDTO studentReEnrollDTO)
    {
        Student? student = await _context.Students.FirstOrDefaultAsync(s => s.StudentNumber == studentNumber);

         if(student == null)
            {
                return false;
            }

            // if student is found
            student.YearLevel = studentReEnrollDTO.YearLevel;


            await _context.SaveChangesAsync();
            return true;
    }
}
