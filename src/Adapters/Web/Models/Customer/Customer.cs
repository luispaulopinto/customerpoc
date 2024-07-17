namespace DevPrime.Web.Models.Customer;

public class Customer
{
    public string CustomerId { get; set; }
    public string PartnerId { get; set; }
    public string Name { get; set; }
    public Guid? ParentId { get; set; }
    public Address Address { get; set; }
    public int CustomerType { get; set; }
    public int CustomerClass { get; set; }

    public static Application.Services.Customer.Model.Customer ToApplication(Customer customer)
    {
        if (customer is null)
            return new Application.Services.Customer.Model.Customer();

        Application.Services.Customer.Model.Customer _customer =
            new Application.Services.Customer.Model.Customer
            {
                CustomerId = customer.CustomerId,
                PartnerId = customer.PartnerId,
                Name = customer.Name,
                ParentId = customer.ParentId,
                Address = Address.ToApplication(customer.Address),
                CustomerType = customer.CustomerType,
                CustomerClass = customer.CustomerClass
            };
        return _customer;
    }

    public static List<Application.Services.Customer.Model.Customer> ToApplication(
        IList<Customer> customerList
    )
    {
        List<Application.Services.Customer.Model.Customer> _customerList =
            new List<Application.Services.Customer.Model.Customer>();
        if (customerList != null)
        {
            foreach (var customer in customerList)
            {
                Application.Services.Customer.Model.Customer _customer =
                    new Application.Services.Customer.Model.Customer
                    {
                        CustomerId = customer.CustomerId,
                        PartnerId = customer.PartnerId,
                        Name = customer.Name,
                        ParentId = customer.ParentId,
                        Address = Address.ToApplication(customer.Address),
                        CustomerType = customer.CustomerType,
                        CustomerClass = customer.CustomerClass
                    };
                _customerList.Add(_customer);
            }
        }
        return _customerList;
    }

    public virtual Application.Services.Customer.Model.Customer ToApplication()
    {
        var model = ToApplication(this);
        return model;
    }
}
