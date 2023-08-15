namespace Dres.Catwalk.Controllers.ApiObjects;

public class PropertyAo
{
    public PropertyAo(int id, string name, string type, string[] relatedResourcesIdentifiers)
    {
        Id = id;
        Name = name;
        Type = type;
        RelatedResourcesIdentifiers = relatedResourcesIdentifiers;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Type { get; private set; }
    public string[] RelatedResourcesIdentifiers { get; private set; }
}