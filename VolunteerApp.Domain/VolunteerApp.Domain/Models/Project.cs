namespace VolunteerApp.Domain.Models;

/// <summary>Волонтёрский проект.</summary>
public class Project : IComparable<Project>
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int MaxParticipants { get; set; }
    public int OrganizerId { get; set; }
    public Organizer? Organizer { get; set; }
    public ICollection<Application> Applications { get; set; } = new List<Application>();

    public int CompareTo(Project? other)
        => other is null ? 1 : string.Compare(Title, other.Title, StringComparison.Ordinal);
}