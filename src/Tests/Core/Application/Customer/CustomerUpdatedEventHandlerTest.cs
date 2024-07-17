namespace Core.Tests;
public class CustomerUpdatedEventHandlerTest
{
    public Dictionary<string, string> CustomSettings()
    {
        var settings = new Dictionary<string, string>();
        settings.Add("stream.customerevents", "customerevents");
        return settings;
    }
    private CustomerUpdatedEventDTO SetEventData(Domain.Aggregates.Customer.Customer customer)
    {
        return new CustomerUpdatedEventDTO()
        {
            ID = customer.ID,
            CustomerId = customer.CustomerId,
            PartnerId = customer.PartnerId,
            Name = customer.Name,
            ParentId = customer.ParentId
        };
    }
    public CustomerUpdated Create_Customer_Object_OK(DpTest dpTest)
    {
        var customer = CustomerTest.Create_Customer_Required_Properties_OK(dpTest);
        var customerUpdated = new CustomerUpdated();
        dpTest.SetDomainEventObject(customerUpdated, customer);
        return customerUpdated;
    }
    [Fact]
    [Trait("EventHandler", "CustomerUpdatedEventHandler")]
    [Trait("EventHandler", "Success")]
    public void Handle_CustomerObjectFilled_Success()
    {
        //Arrange
        var dpTest = new DpTest();
        var settings = CustomSettings();
        var customerUpdated = Create_Customer_Object_OK(dpTest);
        var customer = dpTest.GetDomainEventObject<Domain.Aggregates.Customer.Customer>(customerUpdated);
        var customerUpdatedEventHandler = new Application.EventHandlers.Customer.CustomerUpdatedEventHandler(null, dpTest.MockDp<ICustomerState>(null));
        dpTest.SetupSettings(customerUpdatedEventHandler.Dp, settings);
        dpTest.SetupStream(customerUpdatedEventHandler.Dp);
        //Act
        var result = customerUpdatedEventHandler.Handle(customerUpdated);
        //Assert
        var sentEvents = dpTest.GetSentEvents(customerUpdatedEventHandler.Dp);
        var customerUpdatedEventDTO = SetEventData(customer);
        Assert.Equal(sentEvents[0].Destination, settings["stream.customerevents"]);
        Assert.Equal("CustomerUpdated", sentEvents[0].EventName);
        Assert.Equivalent(sentEvents[0].EventData, customerUpdatedEventDTO);
        Assert.Equal(result, true);
    }
}