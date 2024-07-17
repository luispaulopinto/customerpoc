namespace Application.EventHandlers.Customer;
public class CustomerGetEventHandler : EventHandler<CustomerGet, ICustomerState>
{
    public CustomerGetEventHandler(ICustomerState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(CustomerGet domainEvent)
    {
        var source = Dp.State.Customer.GetAll(domainEvent.Limit, domainEvent.Offset, domainEvent.Ordering, domainEvent.Sort, domainEvent.Filter);
        var total = Dp.State.Customer.Total(domainEvent.Filter);
        return (source, total);
    }
}