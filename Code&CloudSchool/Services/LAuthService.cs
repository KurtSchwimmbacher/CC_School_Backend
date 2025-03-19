using System;
using Code_CloudSchool.Data;
using Code_CloudSchool.Models;

namespace Code_CloudSchool.Services;

public class LAuthService : ILAuthService
{
    private readonly AppDbContext _context;

    public LAuthService(AppDbContext context)
    {
        _context = context;
    }

    public Task<bool> RegisterLecturer(LecturerReg lecturer)
    {
        LecturerReg? doesLecturerExist = EmailExists(lecturer.LecEmail).Result; //checking if the email exists in our DB
        if (doesLecturerExist != null)
        {

            return Task.FromResult(false); //if the email exists, return false
        }

        Lecturer.Password = HashedPassword(user.Password).Result; //first updating my password 


        //Adding the user to our DB
        _context.Users.Add(user);
        _context.SaveChanges();

        return Task.FromResult(true); //returning true if the user was added successfully
        throw new NotImplementedException();
    }

    private Task<string> HashedPassword(string password)
    {
        // Hashing and salting the password with BCrypt Enhanced capabilities
        string hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(password, 13);
        return Task.FromResult(hashedPassword);
    }

    public Task<string> HashPassword(string password)
    {
        //Hashing and salting our password with BCrypt  Enhanced capabilities
        string HashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(password, 13);
        return Task.FromResult(HashedPassword);
        //task - a promise to return a value in the furure 
        //returning the hashed password 
    }

    public async Task<User?> EmailExists(string email)
    {

        //checking if the email exists in our DB
        //go find the fist user where their email matches the email we are looking for
       User? userFromDB = await  _context.Users.FirstOrDefaultAsync(userInDB => userInDB.Email == email);
       return userFromDB; // if null, email not in use, if User, means User already exists 
    
        throw new NotImplementedException();
    }

    public Task<bool> LoginUser(string email, string password)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ValidatePassword(User user, string password)
    {
        throw new NotImplementedException();
    }

    
}

public class ILAuthService
{
}