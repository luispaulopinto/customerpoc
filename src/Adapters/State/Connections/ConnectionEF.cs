using DevPrime.State.Repositories.Customer.Model;

namespace DevPrime.State.Connections;

public class ConnectionEF : EFBaseState
{
    public DbSet<DevPrime.State.Repositories.Customer.Model.Customer> Customer { get; set; }

    public ConnectionEF(DbContextOptions<ConnectionEF> options)
        : base(options) { }

    public ConnectionEF() { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(DbContextOptions<ConnectionEF>).Assembly
        );

        modelBuilder.Entity<Customer>().HasKey(c => c.TenantId);

        modelBuilder
            .Entity<Customer>()
            .HasOne(e => e.ParentCustomer)
            .WithMany(e => e.Associates)
            .HasForeignKey(e => e.ParentCustomerId);
    }
}
