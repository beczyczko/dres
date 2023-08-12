using System.Reflection;

namespace Dres.Core;

public interface IAssembliesResolver
{
    Assembly[] GetAvailable();
}