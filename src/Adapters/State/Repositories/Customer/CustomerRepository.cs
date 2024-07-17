namespace DevPrime.State.Repositories.Customer;

public class CustomerRepository : RepositoryBase, ICustomerRepository
{
    public CustomerRepository(IDpState dp, ConnectionEF state)
        : base(dp)
    {
        ConnectionAlias = "State1";
        State = state;
    }

    #region Write
    public bool Add(Domain.Aggregates.Customer.Customer customer)
    {
        var result = Dp.Pipeline(
            ExecuteResult: (stateContext) =>
            {
                var state = (ConnectionEF)stateContext;
                var _customer = ToState(customer);
                state.Customer.Add(_customer);
                state.SaveChanges();
                return true;
            }
        );
        if (result is null)
            return false;
        return result;
    }

    public bool Delete(Guid customerID)
    {
        var result = Dp.Pipeline(
            ExecuteResult: (stateContext) =>
            {
                var state = (ConnectionEF)stateContext;
                var _customer = state
                    .Customer.Include(a => a.Address)
                    .FirstOrDefault(b => b.TenantId == customerID);
                state.Remove(_customer);
                state.SaveChanges();
                return true;
            }
        );
        if (result is null)
            return false;
        return result;
    }

    public bool Update(Domain.Aggregates.Customer.Customer customer)
    {
        var result = Dp.Pipeline(
            ExecuteResult: (stateContext) =>
            {
                var state = (ConnectionEF)stateContext;
                var _customer = ToState(customer);
                state.Update(_customer);
                state.SaveChanges();
                return true;
            }
        );
        if (result is null)
            return false;
        return result;
    }

    #endregion Write

    #region Read
    public Domain.Aggregates.Customer.Customer Get(Guid customerID)
    {
        return Dp.Pipeline(
            ExecuteResult: (stateContext) =>
            {
                var state = (ConnectionEF)stateContext;

                var customer = state
                    .Customer.Include(a => a.Address)
                    .FirstOrDefault(b => b.TenantId == customerID);

                var _customer = ToDomain(customer);

                return _customer;
            }
        );
    }

    public List<Domain.Aggregates.Customer.Customer> GetChildren(Guid tenantId)
    {
        return Dp.Pipeline(
            ExecuteResult: (stateContext) =>
            {
                var dbContext = (ConnectionEF)stateContext;

                List<Model.Customer> customer = null;

                var recursiveQuery = dbContext
                    .Customer.FromSql(
                        $@"
                        WITH RECURSIVE recursive_cte
                        AS (
                            -- anchor member
                            SELECT * FROM ""Customer"" 
                                WHERE ""ParentCustomerId"" = {tenantId}

                            UNION ALL

                            -- recursive term
                            SELECT c.* FROM ""Customer"" c 
                                INNER JOIN recursive_cte cte 
                                    ON 
                                        cte.""TenantId"" = c.""ParentCustomerId""
                        ) 
                        
                        SELECT * FROM recursive_cte
                    "
                    )
                    .ToList();

                customer = recursiveQuery.ToList();

                var _customer = ToDomain(customer);

                return _customer;
            }
        );
    }

    public List<Domain.Aggregates.Customer.Customer> GetAll(
        int? limit,
        int? offset,
        string ordering,
        string sort,
        string filter
    )
    {
        return Dp.Pipeline(
            ExecuteResult: (stateContext) =>
            {
                var state = (ConnectionEF)stateContext;
                List<Model.Customer> customer = null;
                if (sort?.ToLower() == "desc")
                {
                    var result = state
                        .Customer.Include(a => a.Address)
                        .Where(GetFilter(filter))
                        .OrderByDescending(GetOrdering(ordering));
                    if (limit != null && offset != null)
                        customer = result
                            .Skip(((int)offset - 1) * (int)limit)
                            .Take((int)limit)
                            .ToList();
                    else
                        customer = result.ToList();
                }
                else
                {
                    var result = state
                        .Customer.Include(a => a.Address)
                        .Where(GetFilter(filter))
                        .OrderBy(GetOrdering(ordering));
                    if (limit != null && offset != null)
                        customer = result
                            .Skip(((int)offset - 1) * (int)limit)
                            .Take((int)limit)
                            .ToList();
                    else
                        customer = result.ToList();
                }
                var _customer = ToDomain(customer);
                return _customer;
            }
        );
    }

    private Expression<Func<Model.Customer, object>> GetOrdering(string field)
    {
        Expression<Func<Model.Customer, object>> exp = p => p.TenantId;
        if (!string.IsNullOrWhiteSpace(field))
        {
            if (field.ToLower() == "customerid")
                exp = p => p.CustomerId;
            else if (field.ToLower() == "partnerid")
                exp = p => p.PartnerId;
            else if (field.ToLower() == "name")
                exp = p => p.Name;
            else if (field.ToLower() == "parentid")
                exp = p => p.ParentCustomerId;
            else
                exp = p => p.TenantId;
        }
        return exp;
    }

    private Expression<Func<Model.Customer, bool>> GetFilter(string filter)
    {
        Expression<Func<Model.Customer, bool>> exp = p => true;
        if (!string.IsNullOrWhiteSpace(filter))
        {
            var slice = filter?.Split("=");
            if (slice.Length > 1)
            {
                var field = slice[0];
                var value = slice[1];
                if (string.IsNullOrWhiteSpace(value))
                {
                    exp = p => true;
                }
                else
                {
                    if (field.ToLower() == "customerid")
                        exp = p => p.CustomerId.ToLower() == value.ToLower();
                    else if (field.ToLower() == "partnerid")
                        exp = p => p.PartnerId.ToLower() == value.ToLower();
                    else if (field.ToLower() == "name")
                        exp = p => p.Name.ToLower() == value.ToLower();
                    else if (field.ToLower() == "parentid")
                        exp = p => p.ParentCustomerId == new Guid(value);
                    else
                        exp = p => true;
                }
            }
        }
        return exp;
    }

    public bool Exists(Guid customerID)
    {
        var result = Dp.Pipeline(
            ExecuteResult: (stateContext) =>
            {
                var state = (ConnectionEF)stateContext;
                var customer = state.Customer.Where(x => x.TenantId == customerID).FirstOrDefault();
                return (customerID == customer?.TenantId);
            }
        );
        if (result is null)
            return false;
        return result;
    }

    public long Total(string filter)
    {
        return Dp.Pipeline(
            ExecuteResult: (stateContext) =>
            {
                var state = (ConnectionEF)stateContext;
                var total = state.Customer.Where(GetFilter(filter)).Count();
                return total;
            }
        );
    }

    #endregion Read

    #region mappers
    public static Model.Customer ToState(Domain.Aggregates.Customer.Customer customer)
    {
        if (customer is null)
            return new Model.Customer();
        Model.Customer _customer = new Model.Customer
        {
            TenantId = customer.ID,
            CustomerId = customer.CustomerId,
            PartnerId = customer.PartnerId,
            Name = customer.Name,
            ParentCustomerId = customer.ParentId,
            Address = ToState(customer.Address),
            CustomerType = (int)customer.CustomerType,
            CustomerClass = (int)customer.CustomerClass
        };
        return _customer;
    }

    public static Model.Address ToState(Domain.Aggregates.Customer.Address address)
    {
        if (address is null)
            return new Model.Address();
        Model.Address _address = new Model.Address
        {
            ID = address.ID,
            AddressId = address.AddressId,
            StreetName = address.StreetName,
            StreetNumber = address.StreetNumber
        };
        return _address;
    }

    public static List<Model.Address> ToState(IList<Domain.Aggregates.Customer.Address> addressList)
    {
        List<Model.Address> _addressList = new List<Model.Address>();
        if (addressList != null)
        {
            foreach (var address in addressList)
            {
                Model.Address _address = new Model.Address
                {
                    ID = address.ID,
                    AddressId = address.AddressId,
                    StreetName = address.StreetName,
                    StreetNumber = address.StreetNumber
                };
                _addressList.Add(_address);
            }
        }
        return _addressList;
    }

    public static Domain.Aggregates.Customer.Customer ToDomain(Model.Customer customer)
    {
        if (customer is null)
            return new Domain.Aggregates.Customer.Customer() { IsNew = true };
        Domain.Aggregates.Customer.Customer _customer = new Domain.Aggregates.Customer.Customer(
            customer.TenantId,
            customer.ParentCustomerId,
            customer.CustomerId,
            customer.PartnerId,
            customer.Name,
            ToDomain(customer.Address),
            customer.CustomerType,
            customer.CustomerClass
        );
        return _customer;
    }

    public static Domain.Aggregates.Customer.Address ToDomain(Model.Address address)
    {
        if (address is null)
            return new Domain.Aggregates.Customer.Address() { IsNew = true };
        Domain.Aggregates.Customer.Address _address = new Domain.Aggregates.Customer.Address(
            address.ID,
            address.AddressId,
            address.StreetName,
            address.StreetNumber
        );
        return _address;
    }

    public static List<Domain.Aggregates.Customer.Address> ToDomain(
        IList<Model.Address> addressList
    )
    {
        List<Domain.Aggregates.Customer.Address> _addressList =
            new List<Domain.Aggregates.Customer.Address>();
        if (addressList != null)
        {
            foreach (var address in addressList)
            {
                Domain.Aggregates.Customer.Address _address =
                    new Domain.Aggregates.Customer.Address(
                        address.ID,
                        address.AddressId,
                        address.StreetName,
                        address.StreetNumber
                    );
                _addressList.Add(_address);
            }
        }
        return _addressList;
    }

    public static List<Domain.Aggregates.Customer.Customer> ToDomain(
        IList<Model.Customer> customerList
    )
    {
        List<Domain.Aggregates.Customer.Customer> _customerList =
            new List<Domain.Aggregates.Customer.Customer>();
        if (customerList != null)
        {
            foreach (var customer in customerList)
            {
                Domain.Aggregates.Customer.Customer _customer =
                    new Domain.Aggregates.Customer.Customer(
                        customer.TenantId,
                        customer.ParentCustomerId,
                        customer.CustomerId,
                        customer.PartnerId,
                        customer.Name,
                        ToDomain(customer.Address),
                        customer.CustomerType,
                        customer.CustomerClass
                    );
                _customerList.Add(_customer);
            }
        }
        return _customerList;
    }

    #endregion mappers
}
