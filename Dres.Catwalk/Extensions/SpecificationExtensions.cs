using Dres.Catwalk.Controllers.ApiObjects;
using Dres.Catwalk.Specifications.Core.Dtos;

namespace Dres.Catwalk.Extensions;

public static class SpecificationExtensions
{
    public static SpecificationAo ToAo(this SpecificationDto specification)
    {
        return new SpecificationAo(
            specification.SpecificationId,
            specification.CreatedOn,
            specification.Resources.Select(r => r.ToAo())
        );
    }
    
    private static ResourceAo ToAo(this ResourceDto resource)
    {
        return new ResourceAo(
            resource.Name,
            resource.DomainContext,
            resource.Identifier,
            resource.Type,
            resource.Properties.Select(p => p.ToAo())
        );
    }

    private static PropertyAo ToAo(this Core.Property property)
    {
        return new PropertyAo(
            property.Name,
            property.Type,
            property.RelatedResourcesIdentifiers);
    }
    
    public static Core.Resource ToDresCoreResource(this ResourceDto resource)
    {
        return new Core.Resource(
            resource.Name,
            resource.DomainContext,
            resource.Type,
            resource.Properties
        );
    }
}