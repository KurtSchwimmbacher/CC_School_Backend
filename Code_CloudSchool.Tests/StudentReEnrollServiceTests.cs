using System.Threading.Tasks;
using Code_CloudSchool.Data;
using Code_CloudSchool.DTOs;
using Code_CloudSchool.Models;
using Code_CloudSchool.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class StudentReEnrollServiceTests
{
    private AppDBContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDBContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        var context = new AppDBContext(options);

        return context;
    }

    [Fact]
    public async Task UpdateStudentYearLevel_ShouldReturnFalse_WhenStudentNotFound()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var service = new StudentReEnrollService(context);

        var studentNumber = "nonexistent";
        var dto = new StudentReEnrollDTO { YearLevel = "First Year" };

        // Act
        var result = await service.UpdateStudentYearLevel(studentNumber, dto);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task UpdateStudentYearLevel_ShouldUpdateYearLevelAndReturnTrue_WhenStudentExists()
    {
        // Arrange
        var context = GetInMemoryDbContext();

        var existingStudent = new Student
        {
            StudentNumber = "S12345",
            YearLevel = "First Year",
            Gender = "Male"
        };

        context.Students.Add(existingStudent);
        await context.SaveChangesAsync();

        var service = new StudentReEnrollService(context);
        var dto = new StudentReEnrollDTO { YearLevel = "First Year" };

        // Act
        var result = await service.UpdateStudentYearLevel("S12345", dto);

        // Assert
        Assert.True(result);

        var updatedStudent = await context.Students.FirstOrDefaultAsync(s => s.StudentNumber == "S12345");
        Assert.NotNull(updatedStudent);
        Assert.Equal("First Year", updatedStudent.YearLevel);
    }
}
