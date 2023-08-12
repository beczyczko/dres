using System.Reflection;

namespace Dres.Core;

public class AssembliesResolver : IAssembliesResolver
{
    private readonly Func<Assembly[]> _assembliesGetFunc;

    public AssembliesResolver(Func<Assembly[]> assembliesGetFunc)
    {
        _assembliesGetFunc = assembliesGetFunc;
    }

    public Assembly[] GetAvailable()
    {
        return _assembliesGetFunc();
    }
}