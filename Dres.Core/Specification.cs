using System.ComponentModel.DataAnnotations;

namespace Dres.Core;

public class Specification
{
    public Specification(string name, string tag, string dresApiVersion, ICollection<Resource> resources)
    {
        Name = name;
        Tag = tag;
        DresApiVersion = dresApiVersion;
        Resources = resources;
    }

    [Required] public string Name { get; }

    [Required] public string Tag { get; }

    [Required] public string DresApiVersion { get; }
    [Required] public ICollection<Resource> Resources { get; }
}