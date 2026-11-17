using VolunteerApp.Domain.Generics;
using VolunteerApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace VolunteerApp.Data.Repositories;

/// <summary>
/// Репозиторий проектов: обобщённое хранилище + загрузка из БД.
/// Реализует и <see cref="IRepository{T}"/>, и конкретные операции EF.
/// </summary>
public class ProjectRepository : IRepository<Project>
{
    private readonly AppDbContext _db;
    private readonly Repository<Project> _cache = new();

    /// <summary>Создаёт репозиторий над указанным контекстом.</summary>
    public ProjectRepository(AppDbContext db) => _db = db;

    /// <summary>Добавляет проект в кэш (для демонстрации обобщённого репозитория).</summary>
    public void Add(Project item) => _cache.Add(item);

    /// <summary>Возвращает отсортированный список проектов (по названию).</summary>
    public IReadOnlyList<Project> GetSorted() => _cache.GetSorted();

    /// <summary>Возвращает все проекты без сортировки.</summary>
    public IReadOnlyList<Project> GetAll() => _cache.GetAll();

    /// <summary>Загружает проект с участниками из БД.</summary>
    public async Task<Project?> GetWithApplicationsAsync(int id)
        => await _db.Projects
            .Include(p => p.Applications)
            .Include(p => p.Organizer)
            .FirstOrDefaultAsync(p => p.Id == id);

    /// <summary>Возвращает все проекты из БД.</summary>
    public async Task<List<Project>> GetAllFromDbAsync()
        => await _db.Projects.Include(p => p.Organizer).ToListAsync();
}