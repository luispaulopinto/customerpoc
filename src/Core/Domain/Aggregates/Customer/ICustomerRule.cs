namespace Domain.Aggregates.Customer;

public interface ICustomerRule // : ValueObject
{
    void ValidateAddCustomer(Customer customer, Customer parentCustomer);
}
