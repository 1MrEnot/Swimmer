namespace Swimmer.Infrastructure.Persistance.Mapping;

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AthleteMap : IEntityTypeConfiguration<Athlete>
{
    public void Configure(EntityTypeBuilder<Athlete> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Gender);
        builder.OwnsOne(x => x.Name)
            .Property(n => n.NameValue);
        builder.OwnsOne(x => x.BirthYear)
            .Property(y => y.YearValue);
    }
}