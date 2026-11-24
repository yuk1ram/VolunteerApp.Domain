namespace VolunteerApp.Domain.Models;

/// <summary>Волонтёр — пользователь, который подаёт заявки.</summary>
public class Volunteer
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public ICollection<Application> Applications { get; set; } = new List<Application>();
}