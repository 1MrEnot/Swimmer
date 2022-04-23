namespace Swimmer.Infrastructure.Persistance;

using Microsoft.EntityFrameworkCore;

public class SwimmerContext : DbContext
{
    public SwimmerContext(DbContextOptions<SwimmerContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SwimmerContext).Assembly);
    }
}