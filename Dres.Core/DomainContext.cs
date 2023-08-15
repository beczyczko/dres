using System.Text;

namespace Dres.Core;

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