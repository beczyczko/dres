using Dres.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Specification = Dres.Catwalk.Domain.Specification;

namespace Dres.Catwalk.Specifications.DataAccess.Sqlite.Configurations;

internal class SpecificationConfiguration : IEntityTypeConfiguration<Specification>
{
    public void Configure(EntityTypeBuilder<Specification> builder)
    {
        builder.OwnsOne<SpecificationId>(
            s => s.SpecificationId,
            navBuilder => navBuilder.HasIndex(id => id.Value).IsUnique());
    }
}