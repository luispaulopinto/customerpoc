namespace Domain.Aggregates.Customer;

public class GroupRule : ValueObject, ICustomerRule
{
    public void ValidateAddCustomer(Customer customer, Customer parentCustomer)
    {
        if (customer.ParentId != null)
            throw new Exception();
    }
}
