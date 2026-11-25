namespace VolunteerApp.Domain.Models;

public class Certificate
{
    public int Id { get; set; }
    public int VolunteerId { get; set; }
    public Volunteer? Volunteer { get; set; }

    public int ProjectId { get; set; }
    public Project? Project { get; set; }

    public DateTime IssuedDate { get; set; } = DateTime.UtcNow;
    public string PdfFilePath { get; set; } = string.Empty;
}