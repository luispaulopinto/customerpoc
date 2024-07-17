namespace Core.Tests;
public class CustomerServiceTest
{
    public Application.Services.Customer.Model.Customer SetupCommand(Action add, Action update, Action delete, DpTest dpTest)
    {
        var domainCustomerMock = new Mock<Domain.Aggregates.Customer.Customer>();
        domainCustomerMock.Setup((o) => o.Add()).Callback(add);
        domainCustomerMock.Setup((o) => o.Update()).Callback(update);
        domainCustomerMock.Setup((o) => o.Delete()).Callback(delete);
        var customer = domainCustomerMock.Object;
        dpTest.MockDpDomain(customer);
        dpTest.Set<string>(customer, "CustomerId", Faker.Lorem.Sentence(1));
        dpTest.Set<string>(customer, "PartnerId", Faker.Lorem.Sentence(1));
        dpTest.Set<string>(customer, "Name", Faker.Lorem.Sentence(1));
        var applicationCustomerMock = new Mock<Application.Services.Customer.Model.Customer>();
        applicationCustomerMock.Setup((o) => o.ToDomain()).Returns(customer);
        var applicationCustomer = applicationCustomerMock.Object;
        return applicationCustomer;
    }
    public ICustomerService SetupApplicationService(DpTest dpTest)
    {
        var state = new Mock<ICustomerState>().Object;
        var customerService = new Application.Services.Customer.CustomerService(state, dpTest.MockDp());
        return customerService;
    }
    [Fact]
    [Trait("ApplicationService", "CustomerService")]
    [Trait("ApplicationService", "Success")]
    public void Add_CommandNotNull_Success()
    {
        //Arrange
        var dpTest = new DpTest();
        var addCalled = false;
        var add = () =>
        {
            addCalled = true;
        };
        var command = SetupCommand(add, () =>
        {
        }, () =>
        {
        }, dpTest);
        var customerService = SetupApplicationService(dpTest);
        //Act
        customerService.Add(command);
        //Assert
        Assert.True(addCalled);
    }
    [Fact]
    [Trait("ApplicationService", "CustomerService")]
    [Trait("ApplicationService", "Success")]
    public void Update_CommandFilled_Success()
    {
        //Arrange
        var dpTest = new DpTest();
        var updateCalled = false;
        var update = () =>
        {
            updateCalled = true;
        };
        var command = SetupCommand(() =>
        {
        }, update, () =>
        {
        }, dpTest);
        var customerService = SetupApplicationService(dpTest);
        //Act
        customerService.Update(command);
        //Assert
        Assert.True(updateCalled);
    }
    [Fact]
    [Trait("ApplicationService", "CustomerService")]
    [Trait("ApplicationService", "Success")]
    public void Delete_CommandFilled_Success()
    {
        //Arrange        
        var dpTest = new DpTest();
        var deleteCalled = false;
        var delete = () =>
        {
            deleteCalled = true;
        };
        var command = SetupCommand(() =>
        {
        }, () =>
        {
        }, delete, dpTest);
        var customerService = SetupApplicationService(dpTest);
        //Act
        customerService.Delete(command);
        //Assert
        Assert.True(deleteCalled);
    }
}