using Dres.Core;
using Test.Resources.Common;

namespace Test.Resources.Customers;

[Resource(ResourceIdentifier.CreditCard)]
public class CreditCard
{
    public Guid Id { get; protected set; }
    public string NameOnCard { get; protected set; }
    public string CardNumber { get; protected set; }
    public bool Active { get; protected set; }
    public DateTime Created { get; protected set; }
    public DateTime Expiry { get; protected set; }

    [ResourceRelation(ResourceIdentifier.Customer)]
    public Customer Customer { get; protected set; }
}