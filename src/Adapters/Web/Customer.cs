using Application.Services.Customer.Model.Query;

namespace DevPrime.Web;

public class Customer : Routes
{
    public override void Endpoints(WebApplication app)
    {
        //Automatically returns 404 when no result
        app.MapGet(
            "/v1/customer",
            async (
                HttpContext http,
                ICustomerService Service,
                int? limit,
                int? offset,
                string ordering,
                string ascdesc,
                string filter
            ) =>
                await Dp(http)
                    .Pipeline(
                        () =>
                            Service.GetAll(
                                new Application.Services.Customer.Model.Customer(
                                    limit,
                                    offset,
                                    ordering,
                                    ascdesc,
                                    filter
                                )
                            ),
                        404
                    )
        );
        //Automatically returns 404 when no result
        app.MapGet(
            "/v1/customer/{id}",
            async (
                HttpContext http,
                ICustomerService Service,
                [FromRoute] Guid id,
                [FromQuery] bool includeChildren
            ) =>
                await Dp(http)
                    .Pipeline(() => Service.Get(new CustomerQuery(id, includeChildren)), 404)
        );
        app.MapPost(
            "/v1/customer",
            async (
                HttpContext http,
                ICustomerService Service,
                DevPrime.Web.Models.Customer.Customer command
            ) => await Dp(http).Pipeline(() => Service.Add(command.ToApplication()))
        );
        app.MapPut(
            "/v1/customer",
            async (
                HttpContext http,
                ICustomerService Service,
                Application.Services.Customer.Model.Customer command
            ) => await Dp(http).Pipeline(() => Service.Update(command))
        );
        app.MapDelete(
            "/v1/customer/{id}",
            async (HttpContext http, ICustomerService Service, Guid id) =>
                await Dp(http)
                    .Pipeline(
                        () => Service.Delete(new Application.Services.Customer.Model.Customer(id))
                    )
        );
    }
}
