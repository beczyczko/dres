using System.ComponentModel.DataAnnotations;
using Dres.Core;

namespace Dres.Catwalk.Controllers.ApiObjects;

public class SpecificationAo
{
    public SpecificationAo(
        SpecificationId specificationId,
        DateTimeOffset? createdOn,
        IEnumerable<ResourceAo> resources)
    {
        SpecificationId = specificationId;
        CreatedOn = createdOn;
        Resources = resources.ToList();
    }

    [Required] public SpecificationId SpecificationId { get; private set; }
    [Required] public DateTimeOffset? CreatedOn { get; private set; }
    [Required] public ICollection<ResourceAo> Resources { get; private set; }
}