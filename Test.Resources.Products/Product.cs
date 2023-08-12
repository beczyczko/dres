using System.Collections.ObjectModel;
using Dres.Core;
using Test.Resources.Common;

namespace Test.Resources.Products;

[Resource(ResourceIdentifier.Product)]
public class Product : IAggregateRoot
{
    private List<Return> returns = new List<Return>();

    public Guid Id { get; protected set; }
    public string Name { get; protected set; }
    public DateTime Created { get; protected set; }
    public DateTime Modified { get; protected set; }
    public bool Active { get; protected set; }
    public int Quantity { get; protected set; }
    public decimal Cost { get; protected set; }

    [ResourceRelation(ResourceIdentifier.ProductCode)]
    public ProductCode Code { get; protected set; }

    public ReadOnlyCollection<Return> Returns
    {
        get { return returns.AsReadOnly(); }
    }
}