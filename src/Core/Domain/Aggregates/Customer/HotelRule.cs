namespace Domain.Aggregates.Customer;

public class HotelRule : ValueObject, ICustomerRule
{
    public void ValidateAddCustomer(Customer customer, Customer parentCustomer)
    {
        if (parentCustomer.CustomerType == CustomerType.Hotel)
            throw new Exception();
    }
}
