namespace Core.Tests;

public class CustomerCreatedEventHandlerTest
{
    public Dictionary<string, string> CustomSettings()
    {
        var settings = new Dictionary<string, string>();
        settings.Add("stream.customerevents", "customerevents");
        return settings;
    }

    private CustomerCreatedEventDTO SetEventData(Domain.Aggregates.Customer.Customer customer)
    {
        return new CustomerCreatedEventDTO()
        {
            ID = customer.ID,
            CustomerId = customer.CustomerId,
            PartnerId = customer.PartnerId,
            Name = customer.Name,
            ParentId = customer.ParentId
        };
    }

    public CustomerCreated Create_Customer_Object_OK(DpTest dpTest)
    {
        var customer = CustomerTest.Create_Customer_Required_Properties_OK(dpTest);
        var customerCreated = new CustomerCreated();
        dpTest.SetDomainEventObject(customerCreated, customer);
        return customerCreated;
    }

    [Fact]
    [Trait("EventHandler", "CustomerCreatedEventHandler")]
    [Trait("EventHandler", "Success")]
    public void Handle_CustomerObjectFilled_Success()
    {
        //Arrange
        var dpTest = new DpTest();
        var settings = CustomSettings();
        var customerCreated = Create_Customer_Object_OK(dpTest);
        var customer = dpTest.GetDomainEventObject<Domain.Aggregates.Customer.Customer>(
            customerCreated
        );
        var customerCreatedEventHandler =
            new Application.EventHandlers.Customer.CustomerCreatedEventHandler(
                null,
                dpTest.MockDp<ICustomerState>(null)
            );
        dpTest.SetupSettings(customerCreatedEventHandler.Dp, settings);
        dpTest.SetupStream(customerCreatedEventHandler.Dp);
        //Act
        var result = customerCreatedEventHandler.Handle(customerCreated);
        //Assert
        var sentEvents = dpTest.GetSentEvents(customerCreatedEventHandler.Dp);
        var customerCreatedEventDTO = SetEventData(customer);
        Assert.Equal(sentEvents[0].Destination, settings["stream.customerevents"]);
        Assert.Equal("CustomerCreated", sentEvents[0].EventName);
        Assert.Equivalent(sentEvents[0].EventData, customerCreatedEventDTO);
        Assert.Equal(result, true);
    }
}
