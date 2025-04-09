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
        //go find the first user where their email matches the email we are looking for
        LecturerReg? lecturerFromDB = await _context.Lecturers.FirstOrDefaultAsync(lectInDB => lectInDB.LecEmail == email);
        return lecturerFromDB; // if null, email not in use, if User, means User already exists 

    }


    // TODO
    public Task<string> GenerateEmailAddress(string LectName)
{
    // Brilu don't forget to write the code to generate the lecturers email address 
    //tutorial from https://www.codeproject.com/Articles/22777/Email-Address-Validation-Using-Regular-Expression 
    

    string cleanName = LectName.Replace(" ", "").ToLower();
    string email = $"{cleanName}@cloudschool.edu";
    
    // You might want to add checks for uniqueness
    return Task.FromResult(email);
}

    public Task<string> GenerateEmailAdress(string LectName)
    {
        throw new NotImplementedException();
    }



    // TODO
    public async Task<LecturerReg?> GetLecturerByEmail(string email)
{
    // Get lecturer with all their related data (if needed)
    return await _context.Lecturers
        .FirstOrDefaultAsync(l => l.LecEmail == email);
}

    public Task<string> HashPassword(string password)
    {
        // Hashing and salting the password with BCrypt Enhanced capabilities
        string hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(password, 13);
        return Task.FromResult(hashedPassword);
    }


    // TODO
    public async Task<LecturerReg?> LoginLecturer(string email, string password)
{
    // 1. Find lecturer by email 
    var lecturer = await _context.Lecturers
        .FirstOrDefaultAsync(l => l.LecEmail == email);

    // 2. If lecturer not found, return null
    if (lecturer == null)
    {
        return null;
    }

    // 3. Verify the password matches the hashed password in database
    bool isPasswordValid = BCrypt.Net.BCrypt.EnhancedVerify(password, lecturer.Password);
    
    // 4. Return lecturer if password is correct, otherwise return null
    return isPasswordValid ? lecturer : null;
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
