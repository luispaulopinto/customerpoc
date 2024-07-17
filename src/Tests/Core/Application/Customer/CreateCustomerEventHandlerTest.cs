namespace Core.Tests;
public class CreateCustomerEventHandlerTest
{
    public CreateCustomer Create_Customer_Object_OK(DpTest dpTest)
    {
        var customer = CustomerTest.Create_Customer_Required_Properties_OK(dpTest);
        var createCustomer = new CreateCustomer();
        dpTest.SetDomainEventObject(createCustomer, customer);
        return createCustomer;
    }
    [Fact]
    [Trait("EventHandler", "CreateCustomerEventHandler")]
    [Trait("EventHandler", "Success")]
    public void Handle_CustomerObjectFilled_Success()
    {
        //Arrange
        var dpTest = new DpTest();
        object parameter = null;
        var createCustomer = Create_Customer_Object_OK(dpTest);
        var customer = dpTest.GetDomainEventObject<Domain.Aggregates.Customer.Customer>(createCustomer);
        var repositoryMock = new Mock<ICustomerRepository>();
        repositoryMock.Setup((o) => o.Add(customer)).Returns(true).Callback(() =>
        {
            parameter = customer;
        });
        var repository = repositoryMock.Object;
        var stateMock = new Mock<ICustomerState>();
        stateMock.SetupGet((o) => o.Customer).Returns(repository);
        var state = stateMock.Object;
        var createCustomerEventHandler = new Application.EventHandlers.Customer.CreateCustomerEventHandler(state, dpTest.MockDp<ICustomerState>(state));
        //Act
        var result = createCustomerEventHandler.Handle(createCustomer);
        //Assert
        Assert.Equal(parameter, customer);
        Assert.Equal(result, true);
    }
}