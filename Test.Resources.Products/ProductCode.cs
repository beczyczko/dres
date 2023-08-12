using Dres.Core;
using Test.Resources.Common;

namespace Test.Resources.Products;

[Resource(ResourceIdentifier.ProductCode)]
public class ProductCode : IAggregateRoot
{
    public Guid Id { get; protected set; }
    public string Name { get; protected set; }
}