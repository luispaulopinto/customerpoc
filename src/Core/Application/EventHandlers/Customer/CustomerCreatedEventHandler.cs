namespace Application.EventHandlers.Customer;

public class CustomerCreatedEventHandler : EventHandler<CustomerCreated, ICustomerState>
{
    public CustomerCreatedEventHandler(ICustomerState state, IDp dp)
        : base(state, dp) { }

    public override dynamic Handle(CustomerCreated customerCreated)
    {
        var success = false;
        // var customer = customerCreated.Get<Domain.Aggregates.Customer.Customer>();
        // var destination = Dp.Settings.Default("stream.customerevents");
        // var eventName = "CustomerCreated";
        // var eventData = new CustomerCreatedEventDTO()
        // {
        //     ID = customer.ID,
        //     CustomerId = customer.CustomerId,
        //     PartnerId = customer.PartnerId,
        //     Name = customer.Name,
        //     ParentId = customer.ParentId,
        // };
        // Dp.Stream.Send(destination, eventName, eventData);
        // success = true;
        return success;
    }
}
