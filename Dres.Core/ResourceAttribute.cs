namespace Dres.Core;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Struct)]
public class ResourceAttribute : Attribute
{
    public ResourceAttribute(string identifier)
    {
        Identifier = identifier;
        var identifierParts = identifier.Split('.');
        Context = string.Join(separator: '.', identifierParts[..^1]);
        Name = identifierParts[^1];
    }

    public ResourceAttribute(string name, string context)
    {
        Name = name;
        Context = context;
        Identifier = $"{Context}.{Name}";
    }

    public string Name { get; }
    public string Identifier { get; }
    public string Context { get; }
}