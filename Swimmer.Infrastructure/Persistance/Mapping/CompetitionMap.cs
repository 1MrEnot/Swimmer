namespace Swimmer.Infrastructure.Persistance.Mapping;

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CompetitionMap : IEntityTypeConfiguration<Competition>
{
    public void Configure(EntityTypeBuilder<Competition> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name);
        builder.Property(x => x.TrackCount);
        builder.HasMany(x => x.Athletes);
        builder.HasMany(x => x.Swims);

        builder.Navigation(x => x.Athletes).AutoInclude();
        builder.Navigation(x => x.Swims).AutoInclude();
    }
}