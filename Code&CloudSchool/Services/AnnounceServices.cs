using System;
using Code_CloudSchool.Interface;
using Code_CloudSchool.Models;

namespace Code_CloudSchool.Services;

public class AnnounceServices : IAuthAnnouncement
{

    private readonly AppDbContext _context;

    public AnnounceServices(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Announcements> CreateAnnouncementAsync(Announcements announcements)
    {
       var newAnnouncement = _context.Announcements.Add(announcements);
       await _context.SaveChangesAsync();
       
       return newAnnouncement.Entity;
    }

    public Task<Announcements> GetAnnouncementsAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
