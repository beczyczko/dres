namespace Dres.Catwalk.Controllers.ApiObjects;

public class ResourceAo
{
    public ResourceAo(
        int id,
        string name,
        string domainContext,
        string identifier,
        string type,
        IEnumerable<PropertyAo> properties)
    {
        Id = id;
        Name = name;
        DomainContext = domainContext;
        Identifier = identifier;
        Type = type;
        Properties = properties.ToList();
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public string DomainContext { get; private set; }
    public string Identifier { get; private set; }
    public string Type { get; private set; }
    public ICollection<PropertyAo> Properties { get; private set; }
}