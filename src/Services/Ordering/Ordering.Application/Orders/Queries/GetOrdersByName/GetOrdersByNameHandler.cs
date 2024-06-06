using Microsoft.EntityFrameworkCore;

namespace Ordering.Application.Orders.Queries.GetOrdersByName;

public class GetOrdersByNameHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
{
    public async Task<GetOrdersByNameResult> Handle(
        GetOrdersByNameQuery query,
        CancellationToken cancellationToken
    )
    {
        var orders = await dbContext
            .Orders.Include(o => o.OrderItems)
            .AsNoTracking()
            .Where(o => o.OrderName.Value.Contains(query.Name))
            .OrderBy(o => o.OrderName)
            .Select(o => new OrderDto(
                o.Id.Value,
                o.CustomerId.Value,
                o.OrderName.Value,
                new AddressDto(
                    o.ShippingAddress.FirstName,
                    o.ShippingAddress.LastName,
                    o.ShippingAddress.EmailAddress!,
                    o.ShippingAddress.AddressLine,
                    o.ShippingAddress.Country,
                    o.ShippingAddress.State,
                    o.ShippingAddress.ZipCode
                ),
                new AddressDto(
                    o.BillingAddress.FirstName,
                    o.BillingAddress.LastName,
                    o.BillingAddress.EmailAddress!,
                    o.BillingAddress.AddressLine,
                    o.BillingAddress.Country,
                    o.BillingAddress.State,
                    o.BillingAddress.ZipCode
                ),
                new PaymentDto(
                    o.Payment.CardName!,
                    o.Payment.CardNumber,
                    o.Payment.Expiration,
                    o.Payment.CVV,
                    o.Payment.PaymentMethod
                ),
                o.Status,
                o.OrderItems
                    .Select(oi => new OrderItemDto(
                        oi.OrderId.Value,
                        oi.ProductId.Value,
                        oi.Quantity,
                        oi.Price))
                    .ToList()
            ))
            .ToListAsync(cancellationToken);

        return new GetOrdersByNameResult(orders);
    }
}
