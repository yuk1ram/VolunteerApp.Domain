using static System.Net.Mime.MediaTypeNames;

namespace VolunteerApp.Domain.Models;

/// <summary>
/// Волонтёр — пользователь, который подаёт заявки на проекты.
/// </summary>
public class Volunteer
{
    /// <summary>Уникальный идентификатор волонтёра.</summary>
    public int Id { get; set; }

    /// <summary>Полное имя волонтёра.</summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>Электронная почта (используется для связи).</summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>Список заявок волонтёра.</summary>
    public ICollection<Application> Applications { get; set; } = new List<Application>();
}