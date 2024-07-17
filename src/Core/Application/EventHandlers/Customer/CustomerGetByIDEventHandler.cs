namespace Application.EventHandlers.Customer;

public class CustomerGetByIDEventHandler : EventHandler<CustomerGetByID, ICustomerState>
{
    public CustomerGetByIDEventHandler(ICustomerState state, IDp dp)
        : base(state, dp) { }

    public override dynamic Handle(CustomerGetByID domainEvent)
    {
        var customer = domainEvent.Get<Domain.Aggregates.Customer.Customer>();

        var result = Dp.State.Customer.Get(customer.ID);

        var children = new List<Domain.Aggregates.Customer.Customer>();

        if (domainEvent.IncludeChildren)
            children = Dp.State.Customer.GetChildren(customer.ID);

        return (result, children);
    }
}
