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

    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Tag { get; private set; }
    public DateTimeOffset CreatedOn { get; private set; }
    public ICollection<ResourceAo> Resources { get; private set; }
}