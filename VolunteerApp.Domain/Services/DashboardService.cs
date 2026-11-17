using VolunteerApp.Domain.Generics;
using VolunteerApp.Domain.Models;

namespace VolunteerApp.Web.Services;

/// <summary>
/// Собирает статистику по разным типам сущностей через коллекцию обобщённых репозиториев.
/// </summary>
public class DashboardService
{
    private readonly List<IRepository<Project>> _projectRepos = new();

    /// <summary>Возвращает отсортированные названия проектов из всех репозиториев.</summary>
    public IReadOnlyList<string> GetAllProjectTitles()
    {
        var result = new List<string>();
        foreach (var repo in _projectRepos)
            foreach (var p in repo.GetSorted())
                result.Add(p.Title);
        return result;
    }

    /// <summary>Регистрирует репозиторий для единообразной обработки.</summary>
    public void Register(IRepository<Project> repo) => _projectRepos.Add(repo);
}