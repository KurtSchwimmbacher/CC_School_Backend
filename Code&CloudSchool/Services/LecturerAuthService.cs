using System;
using Code_CloudSchool.Data;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.Models;
using Microsoft.EntityFrameworkCore;

namespace Code_CloudSchool.Services;

public class LecturerAuthService : ILecturerAuth
{

    private readonly AppDBContext _context;

    public LecturerAuthService(AppDBContext context)
    {
        _context = context;
    }

    public async Task<LecturerReg?> EmailExists(string email)
    {
        //checking if the email exists in our DB
        //go find the fist user where their email matches the email we are looking for
        LecturerReg? lecturerFromDB = await _context.Lecturers.FirstOrDefaultAsync(lectInDB => lectInDB.LecEmail == email);
        return lecturerFromDB; // if null, email not in use, if User, means User already exists 

    }


    // TODO
    public Task<string> GenerateEmailAdress(string LectName)
    {
        throw new NotImplementedException();
    }

    // TODO
    public Task<LecturerReg?> GetLecturerByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public Task<string> HashPassword(string password)
    {
        // Hashing and salting the password with BCrypt Enhanced capabilities
        string hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(password, 13);
        return Task.FromResult(hashedPassword);
    }


    // TODO
    public Task<LecturerReg?> LoginLecturer(string email, string password)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RegisterLecturer(LecturerReg lecturer)
    {
        LecturerReg? doesLecturerExist = EmailExists(lecturer.LecEmail).Result; //checking if the email exists in our DB
        if (doesLecturerExist != null)
        {

            return Task.FromResult(false); //if the email exists, return false
        }

        lecturer.Password = HashPassword(lecturer.Password).Result; //first updating my password 


        //Adding the lecturer to our DB
        _context.Lecturers.Add(lecturer);
        _context.SaveChanges();

        return Task.FromResult(true); //returning true if the user was added successfully
    }
}
