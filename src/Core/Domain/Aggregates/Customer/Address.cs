namespace Domain.Aggregates.Customer;

public class Address : Entity
{
    public string AddressId { get; private set; }
    public string StreetName { get; private set; }
    public string StreetNumber { get; private set; }

    public Address(Guid id, string addressId, string streetName, string streetNumber)
    {
        ID = (id == Guid.Empty ? Guid.NewGuid() : id);
        AddressId = addressId;
        StreetName = streetName;
        StreetNumber = streetNumber;
    }

    public Address() { }
}
