using Ordering.Application.Orders.Queries.GetOrdersByName;

namespace Ordering.API.Endpoints;

// public record GetOrdersByNameRequest(string Name);

public record GetOrdersByNameResponse(IEnumerable<OrderDto> Orders);

public class GetOrdersByName : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "/orders/{orderName}",
                async (string orderName, ISender sender) =>
                {
                    var result = await sender.Send(
                        new GetOrdersByNameQuery(orderName)
                    );

                    return Results.Ok(result);
                }
            )
            .WithName("GetOrdersByName")
            .Produces<UpdateOrderResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Orders By Name")
            .WithDescription("Get Orders By Name");
    }
}
