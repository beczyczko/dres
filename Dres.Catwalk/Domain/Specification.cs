using Dres.Catwalk.Controllers.ApiObjects;

namespace Dres.Catwalk.Domain;

public class Specification
{
    private Specification()
    {
        // EF needs it to generate migrations
    }

    public Specification(CreateSpecificationAo createSpec, DateTimeOffset moment)
    {
        Name = createSpec.Name;
        Tag = createSpec.Tag;
        CreatedOn = moment;
        Resources = createSpec.Specification.Resources.Select(r => new Resource(r, this)).ToList();
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Tag { get; private set; }
    public DateTimeOffset CreatedOn { get; private set; }
    public ICollection<Resource> Resources { get; private set; }
}