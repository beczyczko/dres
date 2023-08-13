using System.Collections.Immutable;
using System.Reflection;

namespace Dres.Core;

public class ResourcesProvider : IResourcesProvider
{
    public IImmutableList<Resource> Get(params Assembly[] assemblies)
    {
        // Filter the types that have custom ResourceAttribute
        var classesWithResourceAttribute = assemblies.SelectMany(
                assembly => assembly.GetTypes()
                    .Where(
                        type => type.GetCustomAttribute<ResourceAttribute>(inherit: true) is not null
                    )
            )
            .ToArray();

        var resources = classesWithResourceAttribute.Select(
                resourceType =>
                {
                    var propertyInfos = resourceType.GetProperties();

                    var resourceAttribute = resourceType.GetCustomAttribute<ResourceAttribute>()!;

                    var resource = new Resource(
                        resourceAttribute.Name,
                        resourceAttribute.Context,
                        resourceType.AssemblyQualifiedName!,
                        new List<Property>());

                    var propertiesAttributes = propertyInfos
                        .Select(
                            p =>
                            {
                                var propertyResourceAttributes = p.GetCustomAttribute<ResourceRelationAttribute>(inherit: true);

                                return new Property(
                                    resource.Identifier,
                                    p.Name,
                                    p.PropertyType.Name,
                                    propertyResourceAttributes);
                            })
                        .ToArray();

                    resource.AddProperties(propertiesAttributes);

                    return resource;
                })
            .ToImmutableList();
        return resources;
    }
}