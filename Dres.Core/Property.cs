using System.Text.Json.Serialization;

namespace Dres.Core;

public class Property
{
    [JsonConstructor]
    public Property(string resourceIdentifier, string name, string type, string[] relatedResourcesIdentifiers)
    {
        ResourceIdentifier = resourceIdentifier;
        Name = name;
        Type = type;
        RelatedResourcesIdentifiers = relatedResourcesIdentifiers;
    }
    
    public Property(string resourceIdentifier, string name, string type, ResourceRelationAttribute? resourceAttribute)
    {
        ResourceIdentifier = resourceIdentifier;
        Name = name;
        Type = type;
        RelatedResourcesIdentifiers = resourceAttribute?.ResourceIdentifiers ?? Array.Empty<string>();
    }

    public string ResourceIdentifier { get; }
    public string Name { get; private set; }
    public string Type { get; private set; }
    public string[] RelatedResourcesIdentifiers { get; private set; }
}