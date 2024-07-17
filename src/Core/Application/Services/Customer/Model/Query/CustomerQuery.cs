namespace Application.Services.Customer.Model.Query;

public class CustomerQuery
{
    internal int? Limit { get; set; }
    internal int? Offset { get; set; }
    internal string Ordering { get; set; }
    internal string Filter { get; set; }
    internal string Sort { get; set; }
    public Guid ID { get; set; }
    public bool IncludeChildren { get; set; }

    public int CustomerType { get; set; }
    public int CustomerClass { get; set; }

    public CustomerQuery() { }

    public CustomerQuery(Guid id, bool includeChildren)
    {
        ID = id;
        IncludeChildren = includeChildren;
    }

    public CustomerQuery(int? limit, int? offset, string ordering, string sort, string filter)
    {
        Limit = limit;
        Offset = offset;
        Ordering = ordering;
        Filter = filter;
        Sort = sort;
    }

    public virtual PagingResult<IList<Customer>> ToCustomerList(
        IList<Domain.Aggregates.Customer.Customer> customerList,
        long? total,
        int? offSet,
        int? limit
    )
    {
        var _customerList = ToApplication(customerList);
        return new PagingResult<IList<Customer>>(_customerList, total, offSet, limit);
    }

    public virtual Customer ToCustomer(Domain.Aggregates.Customer.Customer customer)
    {
        var _customer = ToApplication(customer);
        return _customer;
    }

    public virtual Domain.Aggregates.Customer.Customer ToDomain()
    {
        var _customer = ToDomain(this);
        return _customer;
    }

    public virtual Domain.Aggregates.Customer.Customer ToDomain(Guid id)
    {
        var _customer = new Domain.Aggregates.Customer.Customer { ID = id };
        return _customer;
    }

    public static Customer ToApplication(Domain.Aggregates.Customer.Customer customer)
    {
        if (customer is null)
            return new Customer();
        Customer _customer = new Customer
        {
            ID = customer.ID,
            CustomerId = customer.CustomerId,
            PartnerId = customer.PartnerId,
            Name = customer.Name,
            ParentId = customer.ParentId,
            Address = Address.ToApplication(customer.Address),
            CustomerType = (int)customer.CustomerType,
            CustomerClass = (int)customer.CustomerClass
        };
        return _customer;
    }

    public static List<Customer> ToApplication(
        IList<Domain.Aggregates.Customer.Customer> customerList
    )
    {
        List<Customer> _customerList = new List<Customer>();
        if (customerList != null)
        {
            foreach (var customer in customerList)
            {
                Customer _customer = new Customer
                {
                    ID = customer.ID,
                    CustomerId = customer.CustomerId,
                    PartnerId = customer.PartnerId,
                    Name = customer.Name,
                    ParentId = customer.ParentId,
                    Address = Address.ToApplication(customer.Address),
                    CustomerType = (int)customer.CustomerType,
                    CustomerClass = (int)customer.CustomerClass
                };
                _customerList.Add(_customer);
            }
        }
        return _customerList;
    }

    public static Domain.Aggregates.Customer.Customer ToDomain(CustomerQuery customer)
    {
        if (customer is null)
            return new Domain.Aggregates.Customer.Customer();

        Domain.Aggregates.Customer.Customer _customer = new Domain.Aggregates.Customer.Customer(
            id: customer.ID
        );

        return _customer;
    }

    public static List<Domain.Aggregates.Customer.Customer> ToDomain(
        IList<CustomerQuery> customerList
    )
    {
        List<Domain.Aggregates.Customer.Customer> _customerList =
            new List<Domain.Aggregates.Customer.Customer>();
        if (customerList != null)
        {
            foreach (var customer in customerList)
            {
                Domain.Aggregates.Customer.Customer _customer =
                    new Domain.Aggregates.Customer.Customer(id: customer.ID);

                _customerList.Add(_customer);
            }
        }
        return _customerList;
    }
}
