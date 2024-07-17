using System.Text.Json.Serialization;
using Domain.ValueObjects;
using CustomerDomain = Domain.Aggregates.Customer.Customer;

namespace Application.Services.Customer.Model.Result;

public class CustomerGetResult
{
    public Guid ID { get; set; }
    public string CustomerId { get; set; }
    public string PartnerId { get; set; }
    public string Name { get; set; }
    public Guid? ParentId { get; set; }
    public Address Address { get; set; }
    public CustomerType CustomerType { get; set; }
    public CustomerClass CustomerClass { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<CustomerGetResult> Associates { get; set; }

    public CustomerGetResult() { }

    public CustomerGetResult(CustomerDomain customer)
    {
        if (customer is null)
            return;

        ID = customer.ID;
        CustomerId = customer.CustomerId;
        PartnerId = customer.PartnerId;
        Name = customer.Name;
        CustomerType = customer.CustomerType;
        CustomerClass = customer.CustomerClass;

        Associates = null;
    }

    public CustomerGetResult(CustomerDomain customer, List<CustomerDomain> children)
    {
        if (customer is null)
            return;

        ID = customer.ID;
        CustomerId = customer.CustomerId;
        PartnerId = customer.PartnerId;
        Name = customer.Name;
        CustomerType = customer.CustomerType;
        CustomerClass = customer.CustomerClass;

        if (customer.CustomerType == CustomerType.Hotel)
            Associates = null;
        else
            Associates = children
                .Where(c => c.ParentId == customer.ID) // Filtra os filhos diretos
                .Select(c => new CustomerGetResult(c, children)) // Converte cada filho em ObjetoC recursivamente
                .ToList();
    }
}
