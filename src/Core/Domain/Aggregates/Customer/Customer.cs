using Domain.ValueObjects;

namespace Domain.Aggregates.Customer;

public class Customer : AggRoot
{
    public string CustomerId { get; private set; }
    public string PartnerId { get; private set; }
    public string Name { get; private set; }
    public Guid? ParentId { get; private set; }
    public Address Address { get; private set; }
    public CustomerType CustomerType { get; private set; }
    public CustomerClass CustomerClass { get; private set; }

    public Customer(
        Guid id,
        Guid? parentId = null,
        string customerId = "",
        string partnerId = "",
        string name = "",
        Address address = null,
        int customerType = 0,
        int customerClass = 0
    )
    {
        ID = id;
        CustomerId = customerId;
        PartnerId = partnerId;
        Name = name;
        ParentId = parentId;
        Address = address;
        CustomerType = CustomerTypeParse.GetCustomerTypeValue(customerType);
        CustomerClass = CustomerClassParse.GetCustomerClassValue(customerClass);
    }

    public Customer() { }

    public virtual void Add()
    {
        Dp.Pipeline(Execute: () =>
        {
            Dp.Attach(Address);
            // CustomerType = CustomerTypeParse.GetCustomerTypeValue(customerTypeValue);
            // CustomerClass = CustomerClassParse.GetCustomerClassValue(customerClassValue);
            ValidFields();

            //if(Group)
            // Regra para grupo

            //if(Network)
            // Regra para network

            ICustomerRule rule = GetSpecificRule();
            rule.ValidateAddCustomer(this, null);

            ID = Guid.NewGuid();

            IsNew = true;
            var success = Dp.ProcessEvent<bool>(new CreateCustomer());
        });
    }

    public virtual void Update()
    {
        Dp.Pipeline(Execute: () =>
        {
            Dp.Attach(Address);
            // CustomerType = CustomerTypeParse.GetCustomerTypeValue(customerTypeValue);
            // CustomerClass = CustomerClassParse.GetCustomerClassValue(customerClassValue);
            if (ID.Equals(Guid.Empty))
                Dp.Notifications.Add("ID is required");
            ValidFields();
            var success = Dp.ProcessEvent<bool>(new UpdateCustomer());
        });
    }

    public virtual void Delete()
    {
        Dp.Pipeline(Execute: () =>
        {
            if (ID != Guid.Empty)
            {
                var success = Dp.ProcessEvent<bool>(new DeleteCustomer());
            }
        });
    }

    public virtual (List<Customer> Result, long Total) Get(
        int? limit,
        int? offset,
        string ordering,
        string sort,
        string filter
    )
    {
        return Dp.Pipeline(ExecuteResult: () =>
        {
            ValidateOrdering(limit, offset, ordering, sort);
            if (!string.IsNullOrWhiteSpace(filter))
            {
                bool filterIsValid = false;
                if (filter.Contains("="))
                {
                    if (filter.ToLower().StartsWith("id="))
                        filterIsValid = true;
                    if (filter.ToLower().StartsWith("customerid="))
                        filterIsValid = true;
                    if (filter.ToLower().StartsWith("partnerid="))
                        filterIsValid = true;
                    if (filter.ToLower().StartsWith("name="))
                        filterIsValid = true;
                    if (filter.ToLower().StartsWith("parentid="))
                        filterIsValid = true;
                }
                if (!filterIsValid)
                    throw new PublicException(
                        $"Invalid filter '{filter}' is invalid try: 'ID', 'CustomerId', 'PartnerId', 'Name', 'ParentId',"
                    );
            }
            var source = Dp.ProcessEvent(
                new CustomerGet()
                {
                    Limit = limit,
                    Offset = offset,
                    Ordering = ordering,
                    Sort = sort,
                    Filter = filter
                }
            );
            return source;
        });
    }

    public virtual (Customer Customer, List<Customer> Children) GetByID(bool includeChildren)
    {
        var result = Dp.Pipeline(ExecuteResult: () =>
        {
            var source = Dp.ProcessEvent(
                new CustomerGetByID() { IncludeChildren = includeChildren }
            );

            return source;
        });

        return result;
    }

    private void ValidFields()
    {
        if (String.IsNullOrWhiteSpace(CustomerId))
            Dp.Notifications.Add("CustomerId is required");
        if (String.IsNullOrWhiteSpace(PartnerId))
            Dp.Notifications.Add("PartnerId is required");
        if (String.IsNullOrWhiteSpace(Name))
            Dp.Notifications.Add("Name is required");
        if (CustomerType == CustomerType.Unkonw)
            Dp.Notifications.Add("CustomerType is required");
        if (CustomerClass == CustomerClass.Unkonw)
            Dp.Notifications.Add("CustomerClass is required");
        Dp.Notifications.ValidateAndThrow();
    }

    private string customerTypeValue { get; set; }
    private string customerClassValue { get; set; }

    private ICustomerRule GetSpecificRule()
    {
        if (CustomerType == CustomerType.Group)
            return new GroupRule();
        else if (CustomerType == CustomerType.Hotel)
            return new HotelRule();

        throw new Exception();
    }
}
