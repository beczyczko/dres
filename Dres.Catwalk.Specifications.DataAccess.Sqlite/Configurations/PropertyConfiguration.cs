using Dres.Catwalk.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dres.Catwalk.Specifications.DataAccess.Sqlite.Configurations;

internal class PropertyConfiguration : IEntityTypeConfiguration<Property>
{
    public void Configure(EntityTypeBuilder<Property> builder)
    {
        builder.HasIndex(p => new { p.ResourceId, p.Name }).IsUnique();
    }
}