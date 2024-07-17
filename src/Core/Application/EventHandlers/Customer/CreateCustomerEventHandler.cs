namespace Application.EventHandlers.Customer;
public class CreateCustomerEventHandler : EventHandler<CreateCustomer, ICustomerState>
{
    public CreateCustomerEventHandler(ICustomerState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(CreateCustomer createCustomer)
    {
        var customer = createCustomer.Get<Domain.Aggregates.Customer.Customer>();
        var result = Dp.State.Customer.Add(customer);
        return result;
    }
}