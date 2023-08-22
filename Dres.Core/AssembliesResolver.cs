using System.Reflection;

namespace Dres.Core;

public class AssembliesResolver : IAssembliesResolver
{
    private readonly Func<Assembly[]> _assembliesGetFunc;

    public AssembliesResolver(DresOptions dresOptions)
    {
        _assembliesGetFunc = dresOptions.AssembliesGetFunc;
    }

    public Assembly[] GetAvailable()
    {
        return _assembliesGetFunc();
    }
}