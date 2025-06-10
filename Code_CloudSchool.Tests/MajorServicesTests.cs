using Xunit;
using Microsoft.EntityFrameworkCore;
using Code_CloudSchool.Data;
using Code_CloudSchool.Services;
using Code_CloudSchool.DTOs;
using Code_CloudSchool.Models;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;


public class MajorServicesTests : IDisposable
{
    private readonly AppDBContext _context;
    private readonly MajorServices _service;

    public MajorServicesTests()
    {
        // Setup InMemory DB
        var options = new DbContextOptionsBuilder<AppDBContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new AppDBContext(options);

        // Seed data for tests
        SeedData();

        _service = new MajorServices(_context);
    }

    private void SeedData()
    {
        var majors = new List<Majors>
        {
            new Majors { Id = 1, MajorName = "Computer Science", MajorCode = "CS", CreditsRequired = 120 },
            new Majors { Id = 2, MajorName = "Business", MajorCode = "BUS", CreditsRequired = 100 }
        };

        _context.Majors.AddRange(majors);
        _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }


    [Fact]
    public async Task GetMajorDetails_ReturnsCorrectMajor()
    {
        var majorDetails = await _service.GetMajorDetails(1);

        Assert.NotNull(majorDetails);
        Assert.Equal("Computer Science", majorDetails.MajorName);
        Assert.Equal("CS", majorDetails.MajorCode);
        Assert.Equal(120, majorDetails.CreditsRequired);
    }

    [Fact]
    public async Task GetMajorDetails_ThrowsIfNotFound()
    {
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetMajorDetails(999));
    }


    [Fact]
    public async Task CreateMajorAsync_AddsNewMajor()
    {
        var dto = new MajorDetailsDTO
        {
            MajorName = "Mathematics",
            MajorCode = "MATH",
            MajorDescription = "Math Major",
            CreditsRequired = 110
        };

        var created = await _service.CreateMajorAsync(dto);

        Assert.NotNull(created);
        Assert.Equal("Mathematics", created.MajorName);

        var majorInDb = await _context.Majors.FindAsync(created.Id);
        Assert.NotNull(majorInDb);
        Assert.Equal("MATH", majorInDb.MajorCode);
    }


    [Fact]
    public async Task GetMajorCreditsAsync_ReturnsCredits()
    {
        var credits = await _service.GetMajorCreditsAsync(1);

        Assert.Equal(120, credits);
    }

    [Fact]
    public async Task GetMajorCreditsAsync_ThrowsIfMajorNotFound()
    {
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetMajorCreditsAsync(999));
    }


    [Fact]
    public async Task UpdateMajorCreditsAsync_UpdatesCredits()
    {
        var dto = new MajorCreditsDTO { CreditsRequired = 130 };

        var result = await _service.UpdateMajorCreditsAsync(1, dto);

        Assert.True(result);

        var major = await _context.Majors.FindAsync(1);
        Assert.Equal(130, major.CreditsRequired);
    }

    [Fact]
    public async Task UpdateMajorCreditsAsync_ThrowsIfMajorNotFound()
    {
        var dto = new MajorCreditsDTO { CreditsRequired = 130 };

        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.UpdateMajorCreditsAsync(999, dto));
    }


    [Fact]
    public async Task UpdateMajorDetailsAsync_UpdatesAllFields()
    {
        var dto = new MajorDetailsDTO
        {
            MajorName = "Comp Sci Updated",
            MajorCode = "CSU",
            MajorDescription = "Updated desc",
            CreditsRequired = 140
        };

        var result = await _service.UpdateMajorDetailsAsync(1, dto);

        Assert.True(result);

        var major = await _context.Majors.FindAsync(1);
        Assert.Equal("Comp Sci Updated", major.MajorName);
        Assert.Equal("CSU", major.MajorCode);
        Assert.Equal("Updated desc", major.MajorDescription);
        Assert.Equal(140, major.CreditsRequired);
    }

    [Fact]
    public async Task UpdateMajorDetailsAsync_ThrowsIfDtoNull()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _service.UpdateMajorDetailsAsync(1, null));
    }

    [Fact]
    public async Task UpdateMajorDetailsAsync_ThrowsIfMajorNotFound()
    {
        var dto = new MajorDetailsDTO
        {
            MajorName = "X",
            MajorCode = "X",
            MajorDescription = "X",
            CreditsRequired = 10
        };

        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.UpdateMajorDetailsAsync(999, dto));
    }





    [Fact]
    public async Task AddStudentToMajorAsync_ThrowsIfStudentAlreadyInMajor()
    {
        var student = new Student { UserId = 2, Name = "Jane", LastName = "Doe", Gender = "Female" };
        _context.Students.Add(student);
        var major = await _context.Majors.Include(m => m.Students).FirstOrDefaultAsync(m => m.Id == 1);
        major.Students.Add(student);
        await _context.SaveChangesAsync();

        await Assert.ThrowsAsync<InvalidOperationException>(() => _service.AddStudentToMajorAsync(1, 2));
    }

    [Fact]
    public async Task AddStudentToMajorAsync_ThrowsIfMajorNotFound()
    {
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.AddStudentToMajorAsync(999, 1));
    }

    [Fact]
    public async Task AddStudentToMajorAsync_ThrowsIfStudentNotFound()
    {
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.AddStudentToMajorAsync(1, 999));
    }




}

