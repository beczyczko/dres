using Dres.Core;
using Test.Resources.Common;

namespace Test.Resources.Purchases;

[Resource(ResourceIdentifier.PurchasedProduct)]
public class PurchasedProduct
{
    [ResourceRelation(ResourceIdentifier.Purchase)]
    public Guid PurchaseId { get; protected set; }

    [ResourceRelation(ResourceIdentifier.Product)]
    public Guid ProductId { get; protected set; }

    public int Quantity { get; protected set; }
}