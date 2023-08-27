using System.ComponentModel.DataAnnotations;

namespace Dres.Catwalk.Controllers.ApiObjects;

public class ResourceAo
{
    public ResourceAo(
        string name,
        string domainContext,
        string identifier,
        string type,
        IEnumerable<PropertyAo> properties)
    {
        Name = name;
        DomainContext = domainContext;
        Identifier = identifier;
        Type = type;
        Properties = properties.ToList();
    }

    [Required] public string Name { get; private set; }
    [Required] public string DomainContext { get; private set; }
    [Required] public string Identifier { get; private set; }
    [Required] public string Type { get; private set; }
    [Required] public ICollection<PropertyAo> Properties { get; private set; }
}