namespace Application.Services.Customer.Model;
public class DeleteCustomerEventDTO
{
    public Guid ID { get; set; }
    public string CustomerId { get; set; }
    public string PartnerId { get; set; }
    public string Name { get; set; }
    public Guid ParentId { get; set; }
}