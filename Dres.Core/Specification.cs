using System.ComponentModel.DataAnnotations;

namespace Dres.Core;

public class Specification
{
    public Specification(SpecificationId specificationId, string dresApiVersion, ICollection<Resource> resources)
    {
        SpecificationId = specificationId;
        DresApiVersion = dresApiVersion;
        Resources = resources;
    }

    [Required] public SpecificationId SpecificationId { get; }
    [Required] public string DresApiVersion { get; }
    [Required] public ICollection<Resource> Resources { get; }
}