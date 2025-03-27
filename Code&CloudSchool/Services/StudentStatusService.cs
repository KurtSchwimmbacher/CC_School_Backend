using System;
using Code_CloudSchool.Data;
using Code_CloudSchool.DTOs;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Code_CloudSchool.Services;

public class StudentStatusService : IStudentStatus
{

     private readonly AppDBContext _context;

        public StudentStatusService(AppDBContext context)
        {
            _context = context;
        }


        public async Task<bool> UpdateStudentStatus(string studentNumber, UpdateStudentStatusDTO statusDTO)
        {
            Student? student = await _context.Students.FirstOrDefaultAsync(s => s.StudentNumber == studentNumber);

            if(student == null)
            {
                return false;
            }

            // if student is fine
            student.Status = statusDTO.Status;
            await _context.SaveChangesAsync();
            return true;
        }
}
