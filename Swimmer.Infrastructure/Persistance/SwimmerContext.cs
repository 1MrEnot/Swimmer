namespace Swimmer.Infrastructure.Persistance;

using Mapping;
using Microsoft.EntityFrameworkCore;

public class SwimmerContext : DbContext
{
    public SwimmerContext(DbContextOptions<SwimmerContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new SwimMap());
        modelBuilder.ApplyConfiguration(new AthleteMap());
        modelBuilder.ApplyConfiguration(new AthleteOnSwimMap());
        modelBuilder.ApplyConfiguration(new CompetitionMap());
    }
}