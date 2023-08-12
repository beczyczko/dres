using System.Collections.ObjectModel;
using Dres.Core;
using Test.Resources.Common;

namespace Test.Resources.Purchases;

[Resource(ResourceIdentifier.Purchase)]
public class Purchase : IAggregateRoot
{
    private List<PurchasedProduct> purchasedProducts = new List<PurchasedProduct>();

    public Guid Id { get; protected set; }

    public ReadOnlyCollection<PurchasedProduct> Products
    {
        get { return purchasedProducts.AsReadOnly(); }
    }

    public DateTime Created { get; protected set; }

    [ResourceRelation(ResourceIdentifier.Customer)]
    public Guid CustomerId { get; protected set; }

    public decimal TotalTax { get; protected set; }
    public decimal TotalCost { get; protected set; }
}