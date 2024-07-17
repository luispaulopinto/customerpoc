namespace Application.Interfaces.Adapters.State;
public interface ICustomerState
{
    ICustomerRepository Customer { get; set; }
}