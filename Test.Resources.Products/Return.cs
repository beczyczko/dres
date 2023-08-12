using Dres.Core;
using Test.Resources.Common;

namespace Test.Resources.Products;

[Resource(ResourceIdentifier.Return)]
public class Return
{
    [ResourceRelation(ResourceIdentifier.Product)]
    public Product Product { get; protected set; }

    [ResourceRelation(ResourceIdentifier.Customer)]
    public Guid CustomerId { get; protected set; }

    public ReturnReason Reason { get; protected set; }
    public DateTime Created { get; protected set; }
    public string Note { get; protected set; }
}