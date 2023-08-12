using Dres.Core;
using Test.Resources.Common;

namespace Test.Resources.Customers;

[Resource(ResourceIdentifier.Customer)]
public class Customer : IAggregateRoot
{
    private List<CreditCard> creditCards = new List<CreditCard>();

    public Guid Id { get; protected set; }
    public string FirstName { get; protected set; }
    public string LastName { get; protected set; }
    public string Email { get; protected set; }
    public string Password { get; protected set; }
    public decimal Balance { get; protected set; }

    [ResourceRelation(ResourceIdentifier.Country)]
    public Guid CountryId { get; protected set; }
}