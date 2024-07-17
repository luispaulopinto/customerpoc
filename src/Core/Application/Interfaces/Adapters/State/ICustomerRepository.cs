namespace Application.Interfaces.Adapters.State;

public interface ICustomerRepository
{
    bool Add(Domain.Aggregates.Customer.Customer source);
    bool Delete(Guid Id);
    bool Update(Domain.Aggregates.Customer.Customer source);
    Domain.Aggregates.Customer.Customer Get(Guid Id);

    List<Domain.Aggregates.Customer.Customer> GetChildren(Guid ParentId);
    List<Domain.Aggregates.Customer.Customer> GetAll(
        int? limit,
        int? offset,
        string ordering,
        string sort,
        string filter
    );
    bool Exists(Guid id);
    long Total(string filter);
}
