using Dres.Catwalk.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dres.Catwalk.Specifications.DataAccess.Sqlite.Configurations;

internal class ResourceConfiguration : IEntityTypeConfiguration<Resource>
{
    public void Configure(EntityTypeBuilder<Resource> builder)
    {
        builder.HasIndex(r => new { r.SpecificationId, r.Identifier }).IsUnique();
    }
}