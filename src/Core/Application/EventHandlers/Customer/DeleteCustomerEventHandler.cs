namespace Application.EventHandlers.Customer;
public class DeleteCustomerEventHandler : EventHandler<DeleteCustomer, ICustomerState>
{
    public DeleteCustomerEventHandler(ICustomerState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(DeleteCustomer deleteCustomer)
    {
        var customer = deleteCustomer.Get<Domain.Aggregates.Customer.Customer>();
        var result = Dp.State.Customer.Delete(customer.ID);
        return result;
    }
}