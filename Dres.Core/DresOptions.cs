using System.Reflection;

namespace Dres.Core;

public class DresOptions
{
    public const string DresApiVersion = "1.0";
    public Func<Assembly[]> AssembliesGetFunc { get; set; } = Array.Empty<Assembly>;
    public string SpecificationName { get; set; } = string.Empty;
    public string SpecificationTag { get; set; } = string.Empty;
}