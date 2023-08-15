using System.Text;

namespace Dres.Core;

public static class StringBuilderExtensions
{
    public static StringBuilder AppendDomainContexts(
        this StringBuilder stringBuilder,
        IEnumerable<DomainContext> contexts,
        int baseIndent)
    {
        foreach (var context in contexts)
        {
            stringBuilder
                .Append(context.ToPumlNode(baseIndent))
                .AppendLine();
        }

        return stringBuilder;
    }

    public static StringBuilder AppendProperties(
        this StringBuilder stringBuilder,
        IEnumerable<Property> properties,
        int baseIndent)
    {
        foreach (var property in properties)
        {
            stringBuilder.AppendIndent(baseIndent + 1).AppendLine($"+ {property.Name} : {property.Type}");
        }

        return stringBuilder;
    }

    public static StringBuilder AppendResources(
        this StringBuilder stringBuilder,
        IEnumerable<Resource> resources,
        int baseIndent)
    {
        foreach (var resource in resources)
        {
            stringBuilder.Append(resource.ToPumlNode(baseIndent));
        }

        return stringBuilder;
    }

    public static StringBuilder AppendRelations(
        this StringBuilder stringBuilder,
        IEnumerable<Relation> relations,
        int baseIndent)
    {
        foreach (var relation in relations)
        {
            stringBuilder.Append(relation.ToPumlNode(baseIndent));
        }

        stringBuilder.AppendLine();
        return stringBuilder;
    }

    public static StringBuilder AppendIndent(this StringBuilder stringBuilder, int indent)
    {
        stringBuilder.Append(new string(c: ' ', indent * 4));
        return stringBuilder;
    }
}