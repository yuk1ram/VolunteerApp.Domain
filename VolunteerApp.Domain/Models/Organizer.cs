namespace VolunteerApp.Domain.Models;

/// <summary>
/// Организатор — пользователь, который создаёт проекты и получает уведомления.
/// </summary>
public class Organizer
{
    /// <summary>Уникальный идентификатор организатора.</summary>
    public int Id { get; set; }

    /// <summary>Название организации или ФИО.</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Контактная почта для уведомлений.</summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>Проекты, созданные организатором.</summary>
    public ICollection<Project> Projects { get; set; } = new List<Project>();
}