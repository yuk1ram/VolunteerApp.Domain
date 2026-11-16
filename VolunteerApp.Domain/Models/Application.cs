namespace VolunteerApp.Domain.Models;

/// <summary>Статус заявки волонтёра.</summary>
public enum ApplicationStatus
{
    /// <summary>Заявка подана и ожидает рассмотрения.</summary>
    Submitted,
    /// <summary>Заявка принята.</summary>
    Accepted,
    /// <summary>Заявка отклонена (нет мест или дубликат).</summary>
    Rejected
}

/// <summary>
/// Заявка волонтёра на участие в проекте.
/// </summary>
public class Application : IComparable<Application>
{
    /// <summary>Уникальный идентификатор заявки.</summary>
    public int Id { get; set; }

    /// <summary>Идентификатор проекта.</summary>
    public int ProjectId { get; set; }

    /// <summary>Навигационное свойство проекта.</summary>
    public Project? Project { get; set; }

    /// <summary>Идентификатор волонтёра.</summary>
    public int VolunteerId { get; set; }

    /// <summary>Навигационное свойство волонтёра.</summary>
    public Volunteer? Volunteer { get; set; }

    /// <summary>Текущий статус заявки.</summary>
    public ApplicationStatus Status { get; set; } = ApplicationStatus.Submitted;

    /// <summary>Причина отказа (заполняется при Status = Rejected).</summary>
    public string? RejectReason { get; set; }

    /// <summary>Дата подачи заявки (UTC).</summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Сравнение заявок по дате подачи — нужно для ограничения IComparable&lt;T&gt;.
    /// </summary>
    public int CompareTo(Application? other)
        => other is null ? 1 : CreatedAt.CompareTo(other.CreatedAt);
}