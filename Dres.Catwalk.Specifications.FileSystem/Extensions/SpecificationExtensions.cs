using Dres.Catwalk.Specifications.Core.Dtos;
using Dres.Core;

namespace Dres.Catwalk.Specifications.FileSystem.Extensions;

public static class SpecificationExtensions
{
    public static SpecificationDto ToDto(this Specification specification)
    {
        return new SpecificationDto(
            specification.SpecificationId,
            specification.DresApiVersion,
            null,
            specification.Resources.Select(r => r.ToDto()).ToList());
    }

    private static ResourceDto ToDto(this Resource resource)
    {
        return new ResourceDto(
            resource.Name,
            resource.DomainContext,
            resource.Identifier,
            resource.Type,
            resource.Properties);
    }
}