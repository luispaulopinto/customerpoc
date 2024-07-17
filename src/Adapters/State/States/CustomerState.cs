namespace DevPrime.State.States;
public class CustomerState : ICustomerState
{
    public ICustomerRepository Customer { get; set; }
    public CustomerState(ICustomerRepository customer)
    {
        Customer = customer;
    }
}