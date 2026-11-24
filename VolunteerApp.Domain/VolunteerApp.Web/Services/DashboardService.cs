using VolunteerApp.Domain.Generics;
using VolunteerApp.Domain.Models;

namespace VolunteerApp.Web.Services;

public class DashboardService
{
    private readonly List<IRepository<Project>> _repos = new();

    public void Register(IRepository<Project> repo) => _repos.Add(repo);

    public IReadOnlyList<string> GetAllProjectTitles()
    {
        var result = new List<string>();
        foreach (var repo in _repos)
            foreach (var p in repo.GetSorted())
                result.Add(p.Title);
        return result;
    }
}