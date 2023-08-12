using System.Collections.ObjectModel;
using Dres.Core;
using Test.Resources.Common;

namespace Test.Resources.Carts;

[Resource(ResourceIdentifier.Cart)]
public class Cart : IAggregateRoot
{
    public Guid Id { get; protected set; }

    private List<CartProduct> cartProducts = new List<CartProduct>();

    public ReadOnlyCollection<CartProduct> Products
    {
        get { return cartProducts.AsReadOnly(); }
    }

    [ResourceRelation(ResourceIdentifier.Customer)]
    public Guid CustomerId { get; protected set; }

    public decimal TotalCost
    {
        get { return this.Products.Sum(cartProduct => cartProduct.Quantity * cartProduct.Cost); }
    }

    public decimal TotalTax
    {
        get { return this.Products.Sum(cartProducts => cartProducts.Tax); }
    }
}