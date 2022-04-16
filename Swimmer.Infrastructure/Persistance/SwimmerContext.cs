namespace Swimmer.Infrastructure.Persistance;

using Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class SwimmerContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Athlete>();
    }
}