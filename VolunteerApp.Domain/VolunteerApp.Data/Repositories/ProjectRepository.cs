using Microsoft.EntityFrameworkCore;
using VolunteerApp.Domain.Generics;
using VolunteerApp.Domain.Models;

namespace VolunteerApp.Data.Repositories;

public class ProjectRepository : IRepository<Project>
{
    private readonly AppDbContext _db;
    private readonly Repository<Project> _cache = new();

    public ProjectRepository(AppDbContext db) => _db = db;

    public void Add(Project item) => _cache.Add(item);
    public IReadOnlyList<Project> GetSorted() => _cache.GetSorted();
    public IReadOnlyList<Project> GetAll() => _cache.GetAll();

    public async Task<Project?> GetWithApplicationsAsync(int id)
        => await _db.Projects.Include(p => p.Applications).Include(p => p.Organizer)
            .FirstOrDefaultAsync(p => p.Id == id);
}