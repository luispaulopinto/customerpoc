using Domain.ValueObjects;

namespace Domain.Aggregates.Customer;

public class Customer : AggRoot
{
    public string CustomerId { get; private set; }
    public string PartnerId { get; private set; }
    public string Name { get; private set; }
    public Guid ParentId { get; private set; }
    public Address Address { get; private set; }
    public CustomerType CustomerType { get; private set; }
    public CustomerClass CustomerClass { get; private set; }
}
