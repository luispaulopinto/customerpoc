namespace DevPrime.State.Repositories.Customer.Model;

public class Customer
{
    public Guid TenantId { get; set; }
    public string CustomerId { get; set; }
    public string PartnerId { get; set; }
    public string Name { get; set; }

    public Address Address { get; set; }
    public int CustomerType { get; set; }
    public int CustomerClass { get; set; }

    public Guid? ParentCustomerId { get; set; }
    public Customer ParentCustomer { get; set; }
    public ICollection<Customer> Associates { get; set; }
}
