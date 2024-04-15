﻿namespace Catalog.API.Products.DeleteProduct;

// public record DeleteProductRequest(Guid id);
public record DeleteProductResponse(bool isSuccess);

public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete(
                "/api/products/{id}",
                async (Guid id, ISender sender) =>
                {
                    var result = await sender.Send(new DeleteProductCommand(id));

                    var response = result.Adapt<DeleteProductResponse>();

                    return Results.Ok(response);
                }
            )
            .WithName("DeleteProduct")
            .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Product")
            .WithDescription("Delete Product");
        ;
    }
}
