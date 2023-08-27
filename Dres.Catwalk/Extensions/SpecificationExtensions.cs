using Dres.Catwalk.Controllers.ApiObjects;
using Dres.Catwalk.Domain;

namespace Dres.Catwalk.Extensions;

public static class SpecificationExtensions
{
    public static SpecificationAo ToAo(this Dres.Core.Specification specification)
    {
        return new SpecificationAo(
            specification.SpecificationId,
            null,
            specification.Resources.Select(r => r.ToAo())
        );
    }
    
    public static SpecificationAo ToAo(this Specification specification)
    {
        return new SpecificationAo(
            specification.SpecificationId,
            specification.CreatedOn,
            specification.Resources.Select(r => r.ToAo())
        );
    }

    public static Dres.Core.Resource ToDresCoreResource(this Resource resource)
    {
        return new Dres.Core.Resource(
            resource.Name,
            resource.DomainContext,
            resource.Type,
            resource.Properties.Select(p => p.ToDresCoreProperty(resource.Identifier)).ToList()
        );
    }
    
    public static Dres.Core.Resource ToDresCoreResource(this Dres.Core.Resource resource)
    {
        return new Dres.Core.Resource(
            resource.Name,
            resource.DomainContext,
            resource.Type,
            resource.Properties.Select(p => p.ToDresCoreProperty(resource.Identifier)).ToList()
        );
    }

    private static Dres.Core.Property ToDresCoreProperty(this Property property, string resourceIdentifier)
    {
        return new Dres.Core.Property(
            resourceIdentifier,
            property.Name,
            property.Type,
            property.RelatedResourcesIdentifiers);
    }
    
    private static Dres.Core.Property ToDresCoreProperty(this Dres.Core.Property property, string resourceIdentifier)
    {
        return new Dres.Core.Property(
            resourceIdentifier,
            property.Name,
            property.Type,
            property.RelatedResourcesIdentifiers);
    }

    private static ResourceAo ToAo(this Resource resource)
    {
        return new ResourceAo(
            resource.Name,
            resource.DomainContext,
            resource.Identifier,
            resource.Type,
            resource.Properties.Select(p => p.ToAo())
        );
    }

    private static PropertyAo ToAo(this Property property)
    {
        return new PropertyAo(
            property.Name,
            property.Type,
            property.RelatedResourcesIdentifiers);
    }
    
    private static ResourceAo ToAo(this Dres.Core.Resource resource)
    {
        return new ResourceAo(
            resource.Name,
            resource.DomainContext,
            resource.Identifier,
            resource.Type,
            resource.Properties.Select(p => p.ToAo())
        );
    }

    private static PropertyAo ToAo(this Dres.Core.Property property)
    {
        return new PropertyAo(
            property.Name,
            property.Type,
            property.RelatedResourcesIdentifiers);
    }
}