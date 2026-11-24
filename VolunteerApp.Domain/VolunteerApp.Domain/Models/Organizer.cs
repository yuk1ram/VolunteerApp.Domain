namespace VolunteerApp.Domain.Models;

/// <summary>Организатор — создаёт проекты.</summary>
public class Organizer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public ICollection<Project> Projects { get; set; } = new List<Project>();
}