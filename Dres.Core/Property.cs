using System.Text.Json.Serialization;

namespace Dres.Core;

public class Property
{
    public Property(Resource resource, string name, string type, ResourceRelationAttribute? resourceAttribute)
    {
        Resource = resource;
        Name = name;
        Type = type;
        RelatedResourcesIdentifiers = resourceAttribute?.ResourceIdentifiers ?? Array.Empty<string>();
    }

    [JsonIgnore]
    public Resource Resource { get; }

    public string Name { get; }
    public string Type { get; }
    public string[] RelatedResourcesIdentifiers { get; }
}