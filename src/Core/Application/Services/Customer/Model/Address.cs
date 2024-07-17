namespace Application.Services.Customer.Model;
public class Address
{
    public Guid ID { get; set; }
    public string AddressId { get; set; }
    public string StreetName { get; set; }
    public string StreetNumber { get; set; }
    public virtual IList<Address> ToAddressList(IList<Domain.Aggregates.Customer.Address> addressList)
    {
        var _addressList = ToApplication(addressList);
        return _addressList;
    }
    public virtual Address ToAddress(Domain.Aggregates.Customer.Address address)
    {
        var _address = ToApplication(address);
        return _address;
    }
    public virtual Domain.Aggregates.Customer.Address ToDomain()
    {
        var _address = ToDomain(this);
        return _address;
    }
    public virtual Domain.Aggregates.Customer.Address ToDomain(Guid id)
    {
        var _address = new Domain.Aggregates.Customer.Address();
        _address.ID = id;
        return _address;
    }
    public Address()
    {
    }
    public Address(Guid id)
    {
        ID = id;
    }
    public static Application.Services.Customer.Model.Address ToApplication(Domain.Aggregates.Customer.Address address)
    {
        if (address is null)
            return new Application.Services.Customer.Model.Address();
        Application.Services.Customer.Model.Address _address = new Application.Services.Customer.Model.Address();
        _address.ID = address.ID;
        _address.AddressId = address.AddressId;
        _address.StreetName = address.StreetName;
        _address.StreetNumber = address.StreetNumber;
        return _address;
    }
    public static List<Application.Services.Customer.Model.Address> ToApplication(IList<Domain.Aggregates.Customer.Address> addressList)
    {
        List<Application.Services.Customer.Model.Address> _addressList = new List<Application.Services.Customer.Model.Address>();
        if (addressList != null)
        {
            foreach (var address in addressList)
            {
                Application.Services.Customer.Model.Address _address = new Application.Services.Customer.Model.Address();
                _address.ID = address.ID;
                _address.AddressId = address.AddressId;
                _address.StreetName = address.StreetName;
                _address.StreetNumber = address.StreetNumber;
                _addressList.Add(_address);
            }
        }
        return _addressList;
    }
    public static Domain.Aggregates.Customer.Address ToDomain(Application.Services.Customer.Model.Address address)
    {
        if (address is null)
            return new Domain.Aggregates.Customer.Address();
        Domain.Aggregates.Customer.Address _address = new Domain.Aggregates.Customer.Address(address.ID, address.AddressId, address.StreetName, address.StreetNumber);
        return _address;
    }
    public static List<Domain.Aggregates.Customer.Address> ToDomain(IList<Application.Services.Customer.Model.Address> addressList)
    {
        List<Domain.Aggregates.Customer.Address> _addressList = new List<Domain.Aggregates.Customer.Address>();
        if (addressList != null)
        {
            foreach (var address in addressList)
            {
                Domain.Aggregates.Customer.Address _address = new Domain.Aggregates.Customer.Address(address.ID, address.AddressId, address.StreetName, address.StreetNumber);
                _addressList.Add(_address);
            }
        }
        return _addressList;
    }
}