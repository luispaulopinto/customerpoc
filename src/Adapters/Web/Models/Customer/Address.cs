namespace DevPrime.Web.Models.Customer;
public class Address
{
    public string AddressId { get; set; }
    public string StreetName { get; set; }
    public string StreetNumber { get; set; }
    public static Application.Services.Customer.Model.Address ToApplication(DevPrime.Web.Models.Customer.Address address)
    {
        if (address is null)
            return new Application.Services.Customer.Model.Address();
        Application.Services.Customer.Model.Address _address = new Application.Services.Customer.Model.Address();
        _address.AddressId = address.AddressId;
        _address.StreetName = address.StreetName;
        _address.StreetNumber = address.StreetNumber;
        return _address;
    }
    public static List<Application.Services.Customer.Model.Address> ToApplication(IList<DevPrime.Web.Models.Customer.Address> addressList)
    {
        List<Application.Services.Customer.Model.Address> _addressList = new List<Application.Services.Customer.Model.Address>();
        if (addressList != null)
        {
            foreach (var address in addressList)
            {
                Application.Services.Customer.Model.Address _address = new Application.Services.Customer.Model.Address();
                _address.AddressId = address.AddressId;
                _address.StreetName = address.StreetName;
                _address.StreetNumber = address.StreetNumber;
                _addressList.Add(_address);
            }
        }
        return _addressList;
    }
    public virtual Application.Services.Customer.Model.Address ToApplication()
    {
        var model = ToApplication(this);
        return model;
    }
}