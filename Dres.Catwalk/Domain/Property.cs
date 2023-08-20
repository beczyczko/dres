namespace Dres.Catwalk.Domain;

public class Property
{
    private Property()
    {
        // EF needs it to generate migrations
    }
    
    public Property(Core.Property property, Resource resource)
    {
        Resource = resource;
        Name = property.Name;
        Type = property.Type;
        RelatedResourcesIdentifiers = property.RelatedResourcesIdentifiers;
    }


    public int Id { get; private set; }
    public Resource Resource { get; private set; } = null!;
    public string Name { get; private set; } = null!;
    public string Type { get; private set; } = null!;
    public string[] RelatedResourcesIdentifiers { get; private set; } = null!;
}