using System;
using Code_CloudSchool.Models;
using Microsoft.EntityFrameworkCore;

namespace Code_CloudSchool.Services;

// Service class for handling lecturer authentication and registration
public class LAuthService : ILAuthService
{
    // Private field to hold the database context
    private readonly AppDbContext _context;

    // Constructor that injects the database context
    public LAuthService(AppDbContext context)
    {
        _context = context;
    }

    // Method to register a new lecturer
    public Task<bool> RegisterLecturer(LecturerReg lecturer)
    {
        // Check if the lecturer's email already exists in the database
        LecturerReg? doesLecturerExist = EmailExists(lecturer.LecEmail).Result;
        
        if (doesLecturerExist != null)
        {
            return Task.FromResult(false); // If the email exists, return false (registration failed)
        }

        // Hash and update the lecturer's password before storing it
        lecturer.Password = HashedPassword(lecturer.Password).Result;

        // Add the lecturer to the database
        _context.LecturerReg.Add(lecturer);
        _context.SaveChanges(); // Save the changes

        return Task.FromResult(true); // Return true if the lecturer was added successfully
        throw new NotImplementedException(); // Unreachable code (can be removed)
    }

    // Private method to hash the lecturer's password
    private Task<string> HashedPassword(string password)
    {
        // Hashing and salting the password using BCrypt with an enhanced security factor of 13
        string hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(password, 13);
        return Task.FromResult(hashedPassword); // Return the hashed password
    }

    // Public method to hash a password
    public Task<string> HashPassword(string password)
    {
        // Hashing and salting the password with BCrypt
        string hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(password, 13);
        return Task.FromResult(hashedPassword); // Return the hashed password
        
        // Task represents an asynchronous operation that will return the hashed password in the future
    }

    // Method to check if an email already exists in the database
    public async Task<LecturerReg?> EmailExists(string email)
    {
        // Search for the first lecturer where their email matches the given email
        LecturerReg? userFromDB = await _context.LecturerReg.FirstOrDefaultAsync(userInDB => userInDB.LecEmail == email);
        
        // Return the lecturer record if found, otherwise return null
        return userFromDB; // If null, email is not in use; if not null, the user already exists 

        throw new NotImplementedException(); // Unreachable code (can be removed)
    }

    // Method to handle user login (not yet implemented)
    public Task<bool> LoginUser(string email, string password)
    {
        throw new NotImplementedException();
    }

    // Method to validate a lecturer's password (not yet implemented)
    public Task<bool> ValidatePassword(LecturerReg lecturer, string password)
    {
        throw new NotImplementedException();
    }
}

// Incorrect interface definition (this should be an interface, not a class)
public class ILAuthService
{
}