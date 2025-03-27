using System;
using Code_CloudSchool.Data;
using Code_CloudSchool.Interface;
using Code_CloudSchool.Models;

namespace Code_CloudSchool.Services;

// This class implements the IAuthAnnouncement interface and provides services related to announcements
public class AnnounceServices : IAuthAnnouncement
{
    // Private field to hold the database context
    private readonly AppDBContext _context;

    // Constructor that injects the database context
    public AnnounceServices(AppDBContext context)
    {
        _context = context;
    }

    // Method to create a new announcement asynchronously and save it to the database
    public async Task<Announcements> CreateAnnouncementAsync(Announcements announcements)
    {
        // Add the new announcement to the database
        var newAnnouncement = _context.Announcements.Add(announcements);
        
        // Save the changes asynchronously
        await _context.SaveChangesAsync();
        
        // Return the newly created announcement entity
        return newAnnouncement.Entity;
    }

    // Method to retrieve an announcement by its ID (not yet implemented)
    public Task<Announcements> GetAnnouncementsAsync(Guid id)
    {
        throw new NotImplementedException(); // Placeholder for future implementation
    }
}