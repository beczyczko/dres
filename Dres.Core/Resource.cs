using System.Text;

namespace Dres.Core;

public class Resource
{
    public Resource(string name, string domainContext, string type)
    {
        Name = name;
        DomainContext = domainContext;
        Type = type;
    }

    public string Name { get; }
    public string DomainContext { get; }
    public string Identifier => $"{DomainContext}.{Name}";
    public string Type { get; }
    public ICollection<Property> Properties { get; } = new List<Property>();

    public void AddProperties(IEnumerable<Property> properties)
    {
        foreach (var property in properties)
        {
            Properties.Add(property);
        }
    }

    public string ToPumlNode(int baseIndent)
    {
        return new StringBuilder()
            .AppendIndent(baseIndent)
            .AppendLine($"class {Name} {{")
            .AppendProperties(Properties, baseIndent)
            .AppendIndent(baseIndent)
            .AppendLine(value: "}")
            .ToString();
    }
}

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


public class DomainContext
{
    public DomainContext(string name, IEnumerable<Resource> resources)
    {
        Name = name;
        Resources = resources.ToList();
    }

    public string Name { get; }
    public ICollection<Resource> Resources { get; }

    public string ToPumlNode(int baseIndent)
    {
        return new StringBuilder()
            .AppendLine($"frame {Name} {{")
            .AppendResources(Resources, baseIndent + 1)
            .AppendLine(value: "}")
            .ToString();
    }
}