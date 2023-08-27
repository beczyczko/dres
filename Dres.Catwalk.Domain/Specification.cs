using Dres.Core;

namespace Dres.Catwalk.Domain;

public class Specification
{
    private Specification()
    {
        // EF needs it to generate migrations
    }

    public Specification(Core.Specification createSpec, DateTimeOffset moment)
    {
        SpecificationId = createSpec.SpecificationId;
        DresApiVersion = createSpec.DresApiVersion;
        CreatedOn = moment;
        Resources = createSpec.Resources.Select(r => new Resource(r, this)).ToList();
    }

    public int Id { get; private set; }
    public SpecificationId SpecificationId { get; private set; }
    public string DresApiVersion { get; private set; } = null!;
    public DateTimeOffset CreatedOn { get; private set; }
    public ICollection<Resource> Resources { get; private set; } = null!;
}