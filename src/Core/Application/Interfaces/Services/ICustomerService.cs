using Application.Services.Customer.Model.Query;
using Application.Services.Customer.Model.Result;

namespace Application.Interfaces.Services;

public interface ICustomerService
{
    void Add(Customer command);
    void Update(Customer command);
    void Delete(Customer command);

    CustomerGetResult Get(CustomerQuery query);

    PagingResult<IList<Customer>> GetAll(Customer query);
}
