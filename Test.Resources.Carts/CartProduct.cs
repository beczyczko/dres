using Dres.Core;
using Test.Resources.Common;

namespace Test.Resources.Carts;

[Resource(ResourceIdentifier.CartProduct)]
public class CartProduct
{
    [ResourceRelation(ResourceIdentifier.Cart)]
    public Guid CartId { get; protected set; }

    [ResourceRelation(ResourceIdentifier.Customer)]
    public Guid CustomerId { get; protected set; }

    public int Quantity { get; protected set; }

    [ResourceRelation(ResourceIdentifier.Product)]
    public Guid ProductId { get; protected set; }

    public DateTime Created { get; protected set; }
    public decimal Cost { get; protected set; }
    public decimal Tax { get; set; }
}