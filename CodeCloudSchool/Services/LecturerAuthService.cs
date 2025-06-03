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
        return Task.FromResult(LectName + "@codecloudschool.com");
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


    public Task<bool> ValidatePassword(LecturerReg lecturer, string password)
    {
        bool isPasswordValid = BCrypt.Net.BCrypt.EnhancedVerify(password, lecturer.Password);
        return Task.FromResult(isPasswordValid);
    }

    // TODO
    public async Task<LecturerReg?> LoginLecturer(string email, string password)
    {
        LecturerReg? lecturerFromDB = await EmailExists(email);

        // user doesnt exist means cant login
        if (lecturerFromDB == null)
        {
            return null;
        }

        bool isPasswordValid = await ValidatePassword(lecturerFromDB, password);
        if (!isPasswordValid)
        {
            return null;
        }

        // login successful
        return lecturerFromDB;
    }

    public Task<bool> RegisterLecturer(LecturerReg lecturer)
    {
        LecturerReg? doesLecturerExist = EmailExists(lecturer.LecEmail).Result; //checking if the email exists in our DB
        if (doesLecturerExist != null)
        {
            return Task.FromResult(false); //if the email exists, return false
        }

        lecturer.Password = HashPassword(lecturer.Password).Result; //hash passwords    
        lecturer.LecEmail = GenerateEmailAdress(lecturer.LectName).Result; //generate email address based on lecturer name

        //Adding the lecturer to our DB
        _context.Lecturers.Add(lecturer);
        _context.SaveChanges();

        return Task.FromResult(true); //returning true if the user was added successfully
    }
}
