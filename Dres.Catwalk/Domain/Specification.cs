namespace Dres.Catwalk.Domain;

public class Specification
{
    private Specification()
    {
        // EF needs it to generate migrations
    }

    public Specification(Core.Specification createSpec, DateTimeOffset moment)
    {
        Name = createSpec.Name;
        Tag = createSpec.Tag;
        DresApiVersion = createSpec.DresApiVersion;
        CreatedOn = moment;
        Resources = createSpec.Resources.Select(r => new Resource(r, this)).ToList();
    }

    public int Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string Tag { get; private set; } = null!;
    public string DresApiVersion { get; private set; } = null!;
    public DateTimeOffset CreatedOn { get; private set; }
    public ICollection<Resource> Resources { get; private set; } = null!;
}