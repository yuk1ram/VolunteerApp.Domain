namespace VolunteerApp.Domain.Models;

public enum ApplicationStatus { Submitted, Accepted, Rejected }

/// <summary>Заявка волонтёра.</summary>
public class Application : IComparable<Application>
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public Project? Project { get; set; }
    public int VolunteerId { get; set; }
    public Volunteer? Volunteer { get; set; }
    public ApplicationStatus Status { get; set; } = ApplicationStatus.Submitted;
    public string? RejectReason { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int CompareTo(Application? other)
        => other is null ? 1 : CreatedAt.CompareTo(other.CreatedAt);
}