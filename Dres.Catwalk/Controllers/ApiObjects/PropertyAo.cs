using System.ComponentModel.DataAnnotations;

namespace Dres.Catwalk.Controllers.ApiObjects;

public class PropertyAo
{
    public PropertyAo(string name, string type, string[] relatedResourcesIdentifiers)
    {
        Name = name;
        Type = type;
        RelatedResourcesIdentifiers = relatedResourcesIdentifiers;
    }

    [Required] public string Name { get; private set; }
    [Required] public string Type { get; private set; }
    [Required] public string[] RelatedResourcesIdentifiers { get; private set; }
}