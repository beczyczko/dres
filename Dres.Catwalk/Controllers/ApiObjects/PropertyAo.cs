using System.ComponentModel.DataAnnotations;

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

    [Required] public int Id { get; private set; }
    [Required] public string Name { get; private set; }
    [Required] public string Type { get; private set; }
    [Required] public string[] RelatedResourcesIdentifiers { get; private set; }
}