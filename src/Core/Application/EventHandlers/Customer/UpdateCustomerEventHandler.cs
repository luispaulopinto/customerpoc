namespace Application.EventHandlers.Customer;
public class UpdateCustomerEventHandler : EventHandler<UpdateCustomer, ICustomerState>
{
    public UpdateCustomerEventHandler(ICustomerState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(UpdateCustomer updateCustomer)
    {
        var customer = updateCustomer.Get<Domain.Aggregates.Customer.Customer>();
        var result = Dp.State.Customer.Update(customer);
        return result;
    }
}