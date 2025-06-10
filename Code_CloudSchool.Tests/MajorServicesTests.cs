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
}

