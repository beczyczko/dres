using System.Collections.Immutable;
using System.Reflection;

namespace Dres.Core;

public interface IResourcesProvider
{
    IImmutableList<Resource> Get(params Assembly[] assemblies);
}