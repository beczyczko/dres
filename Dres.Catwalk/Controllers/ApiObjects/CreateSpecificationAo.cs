namespace Dres.Catwalk.Controllers.ApiObjects;

public class CreateSpecificationAo
{
    public string Name { get; set; } = null!;
    public string Tag { get; set; } = null!;
    public Core.Specification Specification { get; set; } = null!;
}