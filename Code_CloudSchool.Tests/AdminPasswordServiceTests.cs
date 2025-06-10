using Xunit;
using Microsoft.EntityFrameworkCore;
using Code_CloudSchool.Data;
using Code_CloudSchool.Services;
using Code_CloudSchool.DTOs;
using Code_CloudSchool.Models;
using System.Threading.Tasks;

public class AdminPasswordServiceTests
{
    private readonly AppDBContext _context;
    private readonly AdminPasswordService _service;

    public AdminPasswordServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDBContext>()
            .UseInMemoryDatabase(databaseName: "AdminPasswordTestDb")
            .Options;

        _context = new AppDBContext(options);

        // Seed an admin with a known password hash
        var passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword("OldPassword123", 13);

        _context.Admins.Add(new Admin
        {
            AdminEmail = "admin@example.com",
            Password = passwordHash
        });

        _context.SaveChanges();

        _service = new AdminPasswordService(_context);
    }

    [Fact]
    public async Task UpdateAdminPassword_ReturnsTrue_WhenPasswordUpdated()
    {
        var dto = new StudentPasswordDTO
        {
            OldPassword = "OldPassword123",
            NewPassword = "NewSecurePassword456"
        };

        var result = await _service.UpdateAdminPassword("admin@example.com", dto);

        Assert.True(result);

        var adminInDb = await _context.Admins.FirstOrDefaultAsync(a => a.AdminEmail == "admin@example.com");
        Assert.NotNull(adminInDb);
        Assert.NotEqual("OldPassword123", adminInDb.Password); // Password should be hashed and different

        // Verify the new password matches the stored hash
        bool verified = BCrypt.Net.BCrypt.EnhancedVerify(dto.NewPassword, adminInDb.Password);
        Assert.True(verified);
    }

    [Fact]
    public async Task UpdateAdminPassword_ReturnsFalse_WhenAdminNotFound()
    {
        var dto = new StudentPasswordDTO
        {
            OldPassword = "OldPassword123",
            NewPassword = "NewSecurePassword456"
        };

        var result = await _service.UpdateAdminPassword("nonexistent@example.com", dto);

        Assert.False(result);
    }

    [Fact]
    public async Task UpdateAdminPassword_ReturnsFalse_WhenOldPasswordIncorrect()
    {
        var dto = new StudentPasswordDTO
        {
            OldPassword = "WrongOldPassword",
            NewPassword = "NewSecurePassword456"
        };

        var result = await _service.UpdateAdminPassword("admin@example.com", dto);

        Assert.False(result);
    }

    [Fact]
    public async Task UpdateAdminPassword_ThrowsArgumentNullException_WhenDtoIsNull()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _service.UpdateAdminPassword("admin@example.com", null));
    }
}
