using Microsoft.EntityFrameworkCore;
using VolunteerApp.Data;
using VolunteerApp.Data.Repositories;
using VolunteerApp.Domain.Generics;
using VolunteerApp.Domain.Models;
using VolunteerApp.Domain.Services;
using VolunteerApp.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("VolunteerDb"));

builder.Services.AddScoped<IRepository<Project>, ProjectRepository>();
builder.Services.AddScoped<IRepository<Application>, Repository<Application>>();

builder.Services.AddScoped<ApplicationService>(sp =>
{
    var db = sp.GetRequiredService<AppDbContext>();
    var repo = sp.GetRequiredService<ProjectRepository>();
    return new ApplicationService(
        projectLoader: id => db.Projects.Include(p => p.Applications).FirstOrDefault(p => p.Id == id),
        hasDuplicate: (projectId, volunteerId) =>
            db.Applications.Any(a => a.ProjectId == projectId && a.VolunteerId == volunteerId),
        saver: app => { db.Applications.Add(app); db.SaveChanges(); },
        notifyOrganizer: projectId =>
        {
            var org = db.Projects.Include(p => p.Organizer)
                                 .First(p => p.Id == projectId).Organizer;
            Console.WriteLine($"[EMAIL] Уведомление организатору {org?.Email}: новая заявка на проект {projectId}");
        });
});

builder.Services.AddScoped<DashboardService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    SeedData(db);
}

app.UseStaticFiles();
app.UseRouting();
app.MapDefaultControllerRoute();

app.Run();

/// <summary>Заполняет БД тестовыми данными.</summary>
static void SeedData(AppDbContext db)
{
    if (db.Projects.Any()) return;

    var org = new Organizer { Name = "Эко-центр", Email = "org@eco.ru" };
    var v1 = new Volunteer { FullName = "Иван Петров", Email = "ivan@mail.ru" };
    var v2 = new Volunteer { FullName = "Мария Сидорова", Email = "maria@mail.ru" };

    db.Organizers.Add(org);
    db.Volunteers.AddRange(v1, v2);
    db.Projects.AddRange(
        new Project { Title = "Уборка парка", MaxParticipants = 2, Organizer = org },
        new Project { Title = "Помощь приюту", MaxParticipants = 1, Organizer = org });

    db.SaveChanges();
}