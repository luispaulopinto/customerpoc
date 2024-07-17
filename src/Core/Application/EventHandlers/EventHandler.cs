namespace Application.EventHandlers;
public class EventHandler : IEventHandler
{
    public EventHandler(IHandler handler)
    {
        handler.Add<CreateCustomer, CreateCustomerEventHandler>();
        handler.Add<CustomerGetByID, CustomerGetByIDEventHandler>();
        handler.Add<CustomerCreated, CustomerCreatedEventHandler>();
        handler.Add<CustomerGet, CustomerGetEventHandler>();
        handler.Add<DeleteCustomer, DeleteCustomerEventHandler>();
        handler.Add<CustomerUpdated, CustomerUpdatedEventHandler>();
        handler.Add<CustomerDeleted, CustomerDeletedEventHandler>();
        handler.Add<UpdateCustomer, UpdateCustomerEventHandler>();
    }
}