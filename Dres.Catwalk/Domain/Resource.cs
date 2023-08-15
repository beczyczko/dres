namespace Dres.Catwalk.Domain;

public class Resource
{
    private Resource()
    {
        // EF needs it to generate migrations
    }

    public Resource(Dres.Core.Resource resource, Specification specification)
    {
        Specification = specification;
        Name = resource.Name;
        DomainContext = resource.DomainContext;
        Identifier = resource.Identifier;
        Type = resource.Type;
        Properties = resource.Properties
            .Select(p => new Property(p, this))
            .ToList();
    }

    public int Id { get; private set; }
    public Specification Specification { get; private set; }
    public string Name { get; private set; }
    public string DomainContext { get; private set; }
    public string Identifier { get; private set; }
    public string Type { get; private set; }
    public ICollection<Property> Properties { get; private set; }
}