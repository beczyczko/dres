using Dres.Core;
using Test.Resources.Common;

namespace Test.Resources.Countries;

[Resource(ResourceIdentifier.Country)]
public class Country : IAggregateRoot
{
    public Guid Id { get; protected set; }
    public string Name { get; protected set; }
}