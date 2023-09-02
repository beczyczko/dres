using Dres.Catwalk.Domain;
using Dres.Catwalk.Specifications.Core.Dtos;
using Property = Dres.Core.Property;

namespace Dres.Catwalk.Specifications.Core.Extensions;

public static class SpecificationExtensions
{
    public static SpecificationDto ToDto(this Specification specification)
    {
        return new SpecificationDto(
            specification.SpecificationId,
            specification.DresApiVersion,
            specification.CreatedOn,
            specification.Resources.Select(r => r.ToDto()).ToList());
    }

    private static ResourceDto ToDto(this Resource resource)
    {
        return new ResourceDto(
            resource.Name,
            resource.DomainContext,
            resource.Identifier,
            resource.Type,
            resource.Properties.Select(p => p.ToCoreProperty()).ToList());
    }

    private static Property ToCoreProperty(this Domain.Property prop)
    {
        return new Property(
            prop.Resource.Identifier,
            prop.Name,
            prop.Type,
            prop.RelatedResourcesIdentifiers);
    }
}