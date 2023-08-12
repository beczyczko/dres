using System.Reflection;

namespace Dres.Extensions.DependencyInjection;

public class DresOptions
{
    public Func<Assembly[]> AssembliesGetFunc { get; set; }
}