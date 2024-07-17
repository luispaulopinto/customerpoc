namespace Core.Tests;
public class CustomerDeletedEventHandlerTest
{
    public Dictionary<string, string> CustomSettings()
    {
        var settings = new Dictionary<string, string>();
        settings.Add("stream.customerevents", "customerevents");
        return settings;
    }
    private CustomerDeletedEventDTO SetEventData(Domain.Aggregates.Customer.Customer customer)
    {
        return new CustomerDeletedEventDTO()
        {
            ID = customer.ID,
            CustomerId = customer.CustomerId,
            PartnerId = customer.PartnerId,
            Name = customer.Name,
            ParentId = customer.ParentId
        };
    }
    public CustomerDeleted Create_Customer_Object_OK(DpTest dpTest)
    {
        var customer = CustomerTest.Create_Customer_Required_Properties_OK(dpTest);
        var customerDeleted = new CustomerDeleted();
        dpTest.SetDomainEventObject(customerDeleted, customer);
        return customerDeleted;
    }
    [Fact]
    [Trait("EventHandler", "CustomerDeletedEventHandler")]
    [Trait("EventHandler", "Success")]
    public void Handle_CustomerObjectFilled_Success()
    {
        //Arrange
        var dpTest = new DpTest();
        var settings = CustomSettings();
        var customerDeleted = Create_Customer_Object_OK(dpTest);
        var customer = dpTest.GetDomainEventObject<Domain.Aggregates.Customer.Customer>(customerDeleted);
        var customerDeletedEventHandler = new Application.EventHandlers.Customer.CustomerDeletedEventHandler(null, dpTest.MockDp<ICustomerState>(null));
        dpTest.SetupSettings(customerDeletedEventHandler.Dp, settings);
        dpTest.SetupStream(customerDeletedEventHandler.Dp);
        //Act
        var result = customerDeletedEventHandler.Handle(customerDeleted);
        //Assert
        var sentEvents = dpTest.GetSentEvents(customerDeletedEventHandler.Dp);
        var customerDeletedEventDTO = SetEventData(customer);
        Assert.Equal(sentEvents[0].Destination, settings["stream.customerevents"]);
        Assert.Equal("CustomerDeleted", sentEvents[0].EventName);
        Assert.Equivalent(sentEvents[0].EventData, customerDeletedEventDTO);
        Assert.Equal(result, true);
    }
}