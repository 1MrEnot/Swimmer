namespace Swimmer.Infrastructure.Persistance.Mapping;

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SwimMap : IEntityTypeConfiguration<Swim>
{
    public void Configure(EntityTypeBuilder<Swim> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Gender);
        builder.Property(x => x.Index);
        builder.HasMany(x => x.Athletes);

        builder.Navigation(x => x.Athletes).AutoInclude();
    }
}