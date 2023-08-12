using System.Text;

namespace Dres.Core;

public class Relation
{
    public Relation(Property property, string relatedResourceIdentifier, Resource? resource)
    {
        Property = property;
        RelatedResourceIdentifier = relatedResourceIdentifier;
        Resource = resource;
    }

    public Property Property { get; }
    public string RelatedResourceIdentifier { get; }
    public Resource? Resource { get; }

    public string ToPumlNode(int baseIndent)
    {
        var relatedResourceIdentifier = Resource is not null ? Resource.Identifier : RelatedResourceIdentifier;

        return new StringBuilder()
            .AppendIndent(baseIndent)
            .AppendLine()
            .Append($"{Property.Resource.DomainContext}.{Property.Resource.Name} \"{Property.Name}\"")
            .Append(value: " ...> ")
            .Append($"\"Id\" {relatedResourceIdentifier}")
            .ToString();
    }
}