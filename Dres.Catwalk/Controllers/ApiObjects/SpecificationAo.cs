using System.ComponentModel.DataAnnotations;

namespace Dres.Catwalk.Controllers.ApiObjects;

public class SpecificationAo
{
    public SpecificationAo(
        int id,
        string name,
        string tag,
        DateTimeOffset createdOn,
        IEnumerable<ResourceAo> resources)
    {
        Id = id;
        Name = name;
        Tag = tag;
        CreatedOn = createdOn;
        Resources = resources.ToList();
    }

    [Required] public int Id { get; private set; }
    [Required] public string Name { get; private set; }
    [Required] public string Tag { get; private set; }
    [Required] public DateTimeOffset CreatedOn { get; private set; }
    [Required] public ICollection<ResourceAo> Resources { get; private set; }
}