using System.Text;

namespace Dres.Core;

public class Resource
{
    public Resource(string name, string domainContext, string type, ICollection<Property> properties)
    {
        Name = name;
        DomainContext = domainContext;
        Type = type;
        Properties = properties;
    }

    public string Name { get; private set; }
    public string DomainContext { get; private set; }
    public string Identifier => $"{DomainContext}.{Name}";
    public string Type { get; private set; }
    public ICollection<Property> Properties { get; private set; }

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