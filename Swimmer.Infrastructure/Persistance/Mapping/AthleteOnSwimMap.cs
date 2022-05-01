namespace Swimmer.Infrastructure.Persistance.Mapping;

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AthleteOnSwimMap : IEntityTypeConfiguration<AthleteOnSwim>
{
    public void Configure(EntityTypeBuilder<AthleteOnSwim> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Athlete);
        // builder.HasOne(x => x.Swim);

        builder.Property(x => x.Row);
        builder.Property(x => x.PreliminaryTime);
        builder.Property(x => x.SwimState);
        builder.Property(x => x.SwimTime);

        builder.Navigation(x => x.Athlete).AutoInclude();
        // builder.Navigation(x => x.Swim).AutoInclude();
    }
}