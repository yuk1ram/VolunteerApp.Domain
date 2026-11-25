namespace VolunteerApp.Domain.Models;

public class ActivityLog
{
    public int Id { get; set; }
    public int VolunteerId { get; set; }
    public Volunteer? Volunteer { get; set; }

    public int ProjectId { get; set; }
    public Project? Project { get; set; }

    public DateTime Date { get; set; } = DateTime.UtcNow;
    public double HoursWorked { get; set; }
    public string Description { get; set; } = string.Empty;
}