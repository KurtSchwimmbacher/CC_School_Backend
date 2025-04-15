using Code_CloudSchool.Data;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.Models;
using Microsoft.EntityFrameworkCore;

namespace Code_CloudSchool.Services;

public class LAuthService : ILecturerAuth
{
    private readonly AppDBContext _context;

    public LAuthService(AppDBContext context) => _context = context;

    public async Task<string> GenerateEmailAddress(string lectName)
    {
        var names = lectName.ToLower(); 
        return $"{names}.@cloudschool.edu";
    }

        public Task<string> HashPassword(string password) 
        => Task.FromResult(BCrypt.Net.BCrypt.EnhancedHashPassword(password, 13)); //why the work factor of 13 ? 
        //It balances security and performance. Higher values increase hash time but improve resistance to brute-force attacks

        //Securely compares password to hash.

    public Task<LecturerReg?> EmailExists(string email) 
        => _context.Lecturers.FirstOrDefaultAsync(l => l.LecEmail == email);

    public async Task<LecturerReg?> LoginLecturer(string email, string password)
    {
        var lecturer = await EmailExists(email);
        return lecturer != null && BCrypt.Net.BCrypt.EnhancedVerify(password, lecturer.Password) 
            ? lecturer : null; //Ternary operator for clean returns. One-line success/failure handling.
    }

    public Task<LecturerReg?> GetLecturerByEmail(string email) 
        => _context.Lecturers.Include(l => l.Courses).FirstOrDefaultAsync(l => l.LecEmail == email);


        //All methods are asynchronous to avoid blocking threads during database operations.
}