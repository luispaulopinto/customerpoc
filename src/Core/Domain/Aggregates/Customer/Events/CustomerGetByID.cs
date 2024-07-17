namespace Domain.Aggregates.Customer.Events;

public class CustomerGetByID : DomainEvent
{
    public CustomerGetByID()
        : base() { }

    public bool IncludeChildren { get; set; }
}
