using VolunteerApp.Domain.Models;

namespace VolunteerApp.Domain.Services;

/// <summary>Результат попытки подачи заявки.</summary>
public record ApplicationResult(bool Success, string Message);

/// <summary>
/// Бизнес-логика подачи заявки (шаги 3–8 диаграммы).
/// </summary>
public class ApplicationService
{
    private readonly Func<int, Project?> _projectLoader;
    private readonly Func<int, int, bool> _hasDuplicate;
    private readonly Action<Application> _saver;
    private readonly Action<int> _notifyOrganizer;

    public ApplicationService(
        Func<int, Project?> projectLoader,
        Func<int, int, bool> hasDuplicate,
        Action<Application> saver,
        Action<int> notifyOrganizer)
    {
        _projectLoader = projectLoader;
        _hasDuplicate = hasDuplicate;
        _saver = saver;
        _notifyOrganizer = notifyOrganizer;
    }

    /// <summary>
    /// Подаёт заявку волонтёра на проект.
    /// </summary>
    public ApplicationResult Submit(int projectId, int volunteerId)
    {
        var project = _projectLoader(projectId);
        if (project is null) return new ApplicationResult(false, "Проект не найден");

        int taken = project.Applications.Count(a => a.Status == ApplicationStatus.Accepted);
        bool hasFreeSeats = taken < project.MaxParticipants;
        bool alreadyApplied = _hasDuplicate(projectId, volunteerId);

        if (hasFreeSeats && !alreadyApplied)
        {
            var app = new Application
            {
                ProjectId = projectId,
                VolunteerId = volunteerId,
                Status = ApplicationStatus.Submitted
            };
            _saver(app);
            _notifyOrganizer(projectId);
            return new ApplicationResult(true, "Заявка принята");
        }

        string reason = alreadyApplied ? "Вы уже записаны" : "Свободных мест нет";
        return new ApplicationResult(false, reason);
    }
}