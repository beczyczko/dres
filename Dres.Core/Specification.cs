namespace Dres.Core;

public class Specification
{
    public Specification(ICollection<Resource> resources)
    {
        Resources = resources.ToList();
    }
    
    public ICollection<Resource> Resources { get; private set; }
}