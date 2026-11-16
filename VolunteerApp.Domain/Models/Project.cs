using static System.Net.Mime.MediaTypeNames;

namespace VolunteerApp.Domain.Models;

/// <summary>
/// Волонтёрский проект с ограниченным числом мест.
/// </summary>
public class Project : IComparable<Project>
{
    /// <summary>Уникальный идентификатор проекта.</summary>
    public int Id { get; set; }

    /// <summary>Название проекта.</summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>Максимальное число участников.</summary>
    public int MaxParticipants { get; set; }

    /// <summary>Идентификатор организатора.</summary>
    public int OrganizerId { get; set; }

    /// <summary>Навигационное свойство организатора.</summary>
    public Organizer? Organizer { get; set; }

    /// <summary>Заявки на участие в проекте.</summary>
    public ICollection<Application> Applications { get; set; } = new List<Application>();

    /// <summary>
    /// Сравнение проектов по названию — нужно для ограничения IComparable&lt;T&gt;.
    /// </summary>
    public int CompareTo(Project? other)
        => other is null ? 1 : string.Compare(Title, other.Title, StringComparison.Ordinal);
}