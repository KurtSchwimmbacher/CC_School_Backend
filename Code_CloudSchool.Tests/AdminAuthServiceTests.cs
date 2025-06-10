using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Code_CloudSchool.Data;
using Code_CloudSchool.Models;
using Code_CloudSchool.Services;

public class AdminAuthServiceTests
{
    private AppDBContext GetInMemoryDb()
    {
        var options = new DbContextOptionsBuilder<AppDBContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new AppDBContext(options);
    }

    private Admin GetTestAdmin()
    {
        return new Admin
        {
            UserId = 1,
            Name = "Kurt",
            LastName = "Schwimmbacher",
            AdminRole = "Moderator",
            Password = "Password123",
            AdminEmail = "KurtSchwimmbacher.Moderator@codecloudschool.com"
        };
    }
    [Fact]
    public async Task EmailExists_ReturnsAdmin_IfExists()
    {
        using var context = GetInMemoryDb();
        var admin = GetTestAdmin();
        context.Admins.Add(admin);
        context.SaveChanges();

        var service = new AdminAuthService(context);
        var result = await service.EmailExists(admin.AdminEmail);

        Assert.NotNull(result);
        Assert.Equal(admin.AdminEmail, result.AdminEmail);
    }

    [Fact]
    public async Task EmailExists_ReturnsNull_IfNotExists()
    {
        using var context = GetInMemoryDb();
        var service = new AdminAuthService(context);
        var result = await service.EmailExists("nonexistent@codecloudschool.com");

        Assert.Null(result);
    }

    [Fact]
    public async Task GenerateAdminEmail_ReturnsCorrectFormat()
    {
        var context = GetInMemoryDb();
        var service = new AdminAuthService(context);

        var result = await service.GenerateAdminEmail("Jane", "Doe", "Moderator");

        Assert.Equal("JaneDoe.Moderator@codecloudschool.com", result);
    }

    [Fact]
    public async Task HashPassword_CreatesValidHash()
    {
        var context = GetInMemoryDb();
        var service = new AdminAuthService(context);

        var password = "MySecurePassword!";
        var hash = await service.HashPassword(password);

        Assert.NotEqual(password, hash);
        Assert.True(hash.Length > 20); // check it's hashed
    }

    [Fact]
    public async Task ValidatePassword_ReturnsTrue_WhenPasswordMatches()
    {
        var context = GetInMemoryDb();
        var service = new AdminAuthService(context);

        var password = "Secret123";
        var hashed = await service.HashPassword(password);
        var admin = GetTestAdmin();
        admin.Password = hashed;

        var result = await service.ValidatePassword(admin, password);

        Assert.True(result);
    }

    [Fact]
    public async Task ValidatePassword_ReturnsFalse_WhenPasswordDoesNotMatch()
    {
        var context = GetInMemoryDb();
        var service = new AdminAuthService(context);

        var admin = GetTestAdmin();
        admin.Password = await service.HashPassword("ActualPassword");

        var result = await service.ValidatePassword(admin, "WrongPassword");

        Assert.False(result);
    }

    [Fact]
    public async Task LoginAdmin_ReturnsAdmin_WhenCredentialsAreValid()
    {
        var context = GetInMemoryDb();
        var service = new AdminAuthService(context);

        var password = "CorrectPassword";
        var admin = GetTestAdmin();
        admin.Password = await service.HashPassword(password);
        context.Admins.Add(admin);
        context.SaveChanges();

        var result = await service.LoginAdmin(password, admin.AdminEmail);

        Assert.NotNull(result);
        Assert.Equal(admin.AdminEmail, result.AdminEmail);
    }

    [Fact]
    public async Task LoginAdmin_ReturnsNull_WhenEmailNotFound()
    {
        var context = GetInMemoryDb();
        var service = new AdminAuthService(context);

        var result = await service.LoginAdmin("any", "not@found.com");

        Assert.Null(result);
    }

        [Fact]
    public async Task LoginAdmin_ReturnsNull_WhenPasswordWrong()
    {
        var context = GetInMemoryDb();
        var service = new AdminAuthService(context);

        var admin = GetTestAdmin();
        admin.Password = await service.HashPassword("correct");
        context.Admins.Add(admin);
        context.SaveChanges();

        var result = await service.LoginAdmin("wrong", admin.AdminEmail);

        Assert.Null(result);
    } 

    [Fact]
    public async Task RegisterAdmin_AddsAdmin_WhenNotExists()
    {
        var context = GetInMemoryDb();
        var service = new AdminAuthService(context);

        var newAdmin = GetTestAdmin();
        newAdmin.Password = "MyPassword"; // plain password
    }

    [Fact]
    public async Task RegisterAdmin_ReturnsNull_WhenAdminExists()
    {
        var context = GetInMemoryDb();
        var admin = GetTestAdmin();
        context.Admins.Add(admin);
        context.SaveChanges();

        var service = new AdminAuthService(context);
        var duplicate = GetTestAdmin();

        var result = await service.RegisterAdmin(duplicate);

        Assert.Null(result);
    }
}
