namespace Core.Tests;
public class DeleteCustomerEventHandlerTest
{
    public DeleteCustomer Create_Customer_Object_OK(DpTest dpTest)
    {
        var customer = CustomerTest.Create_Customer_Required_Properties_OK(dpTest);
        var deleteCustomer = new DeleteCustomer();
        dpTest.SetDomainEventObject(deleteCustomer, customer);
        return deleteCustomer;
    }
    [Fact]
    [Trait("EventHandler", "DeleteCustomerEventHandler")]
    [Trait("EventHandler", "Success")]
    public void Handle_CustomerObjectFilled_Success()
    {
        //Arrange
        var dpTest = new DpTest();
        object parameter = null;
        var deleteCustomer = Create_Customer_Object_OK(dpTest);
        var customer = dpTest.GetDomainEventObject<Domain.Aggregates.Customer.Customer>(deleteCustomer);
        var repositoryMock = new Mock<ICustomerRepository>();
        repositoryMock.Setup((o) => o.Delete(customer.ID)).Returns(true).Callback(() =>
        {
            parameter = customer;
        });
        var repository = repositoryMock.Object;
        var stateMock = new Mock<ICustomerState>();
        stateMock.SetupGet((o) => o.Customer).Returns(repository);
        var state = stateMock.Object;
        var deleteCustomerEventHandler = new Application.EventHandlers.Customer.DeleteCustomerEventHandler(state, dpTest.MockDp<ICustomerState>(state));
        //Act
        var result = deleteCustomerEventHandler.Handle(deleteCustomer);
        //Assert
        Assert.Equal(parameter, customer);
        Assert.Equal(result, true);
    }
}