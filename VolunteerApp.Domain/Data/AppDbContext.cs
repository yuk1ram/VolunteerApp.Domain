using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using VolunteerApp.Domain.Models;

namespace VolunteerApp.Data;

/// <summary>Контекст EF Core для волонтёрского приложения.</summary>
public class AppDbContext : DbContext
{
    /// <summary>Создаёт контекст с переданными опциями.</summary>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    /// <summary>Волонтёры.</summary>
    public DbSet<Volunteer> Volunteers => Set<Volunteer>();
    /// <summary>Организаторы.</summary>
    public DbSet<Organizer> Organizers => Set<Organizer>();
    /// <summary>Проекты.</summary>
    public DbSet<Project> Projects => Set<Project>();
    /// <summary>Заявки.</summary>
    public DbSet<Application> Applications => Set<Application>();

    /// <summary>Настраивает связи и ограничения модели.</summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>()
            .HasOne(p => p.Organizer)
            .WithMany(o => o.Projects)
            .HasForeignKey(p => p.OrganizerId);

        modelBuilder.Entity<Application>()
            .HasOne(a => a.Project)
            .WithMany(p => p.Applications)
            .HasForeignKey(a => a.ProjectId);

        modelBuilder.Entity<Application>()
            .HasOne(a => a.Volunteer)
            .WithMany(v => v.Applications)
            .HasForeignKey(a => a.VolunteerId);
    }
}