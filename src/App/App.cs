var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerState, CustomerState>();
builder.Services.AddScoped<IExtensions, Extensions>();
builder.Services.AddScoped<IEventStream, EventStream>();
builder.Services.AddScoped<IEventHandler, Application.EventHandlers.EventHandler>();

builder.Services.AddDbContext<ConnectionEF>(options =>
    options
        .UseNpgsql(
            StateAdapter.GetConnection("State1").ConnectionString,
            b => b.MigrationsAssembly("App")
        )
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
);

builder.Services.AddMvc(o =>
{
    o.EnableEndpointRouting = false;
});

await new DpApp(builder).Run(
    "CustomerPOC",
    (app) =>
    {
        app.UseRouting();
        //Uncomment this line to enable Authentication
        app.UseAuthentication();
        DpApp.UseDevPrimeSwagger(app);
        //Uncomment this line to enable UseAuthorization
        app.UseAuthorization();
        app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
    },
    (builder) =>
    {
        DpApp.AddDevPrime(builder.Services);
        DpApp.AddDevPrimeSwagger(builder.Services);
        DpApp.AddDevPrimeSecurity(builder.Services);
    }
);
