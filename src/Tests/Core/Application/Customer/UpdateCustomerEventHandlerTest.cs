namespace Core.Tests;
public class UpdateCustomerEventHandlerTest
{
    public UpdateCustomer Create_Customer_Object_OK(DpTest dpTest)
    {
        var customer = CustomerTest.Create_Customer_Required_Properties_OK(dpTest);
        var updateCustomer = new UpdateCustomer();
        dpTest.SetDomainEventObject(updateCustomer, customer);
        return updateCustomer;
    }
    [Fact]
    [Trait("EventHandler", "UpdateCustomerEventHandler")]
    [Trait("EventHandler", "Success")]
    public void Handle_CustomerObjectFilled_Success()
    {
        //Arrange
        var dpTest = new DpTest();
        object parameter = null;
        var updateCustomer = Create_Customer_Object_OK(dpTest);
        var customer = dpTest.GetDomainEventObject<Domain.Aggregates.Customer.Customer>(updateCustomer);
        var repositoryMock = new Mock<ICustomerRepository>();
        repositoryMock.Setup((o) => o.Update(customer)).Returns(true).Callback(() =>
        {
            parameter = customer;
        });
        var repository = repositoryMock.Object;
        var stateMock = new Mock<ICustomerState>();
        stateMock.SetupGet((o) => o.Customer).Returns(repository);
        var state = stateMock.Object;
        var updateCustomerEventHandler = new Application.EventHandlers.Customer.UpdateCustomerEventHandler(state, dpTest.MockDp<ICustomerState>(state));
        //Act
        var result = updateCustomerEventHandler.Handle(updateCustomer);
        //Assert
        Assert.Equal(parameter, customer);
        Assert.Equal(result, true);
    }
}