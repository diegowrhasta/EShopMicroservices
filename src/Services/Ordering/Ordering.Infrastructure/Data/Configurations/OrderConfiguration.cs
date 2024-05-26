using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .HasConversion(orderId => orderId.Value, dbId => OrderId.Of(dbId));

        builder
            .HasOne<Customer>()
            .WithMany()
            .HasForeignKey(x => x.CustomerId)
            .IsRequired();

        builder
            .HasMany(x => x.OrderItems)
            .WithOne()
            .HasForeignKey(x => x.OrderId);

        builder.ComplexProperty(
            o => o.OrderName,
            nameBuilder =>
            {
                nameBuilder
                    .Property(x => x.Value)
                    .HasColumnName(nameof(Order.OrderName))
                    .HasMaxLength(100)
                    .IsRequired();
            }
        );

        builder.ComplexProperty(
            o => o.ShippingAddress,
            addressBuilder =>
            {
                addressBuilder
                    .Property(x => x.FirstName)
                    .HasMaxLength(50)
                    .IsRequired();

                addressBuilder
                    .Property(x => x.LastName)
                    .HasMaxLength(50)
                    .IsRequired();
                addressBuilder
                    .Property(x => x.EmailAddress)
                    .HasMaxLength(50)
                    .IsRequired();
                addressBuilder.Property(x => x.AddressLine).HasMaxLength(180);

                addressBuilder
                    .Property(x => x.Country)
                    .HasMaxLength(50)
                    .IsRequired();

                addressBuilder.Property(x => x.State).HasMaxLength(50);

                addressBuilder
                    .Property(x => x.ZipCode)
                    .HasMaxLength(5)
                    .IsRequired();
            }
        );

        builder.ComplexProperty(
            o => o.BillingAddress,
            addressBuilder =>
            {
                addressBuilder
                    .Property(x => x.FirstName)
                    .HasMaxLength(50)
                    .IsRequired();

                addressBuilder
                    .Property(x => x.LastName)
                    .HasMaxLength(50)
                    .IsRequired();
                addressBuilder
                    .Property(x => x.EmailAddress)
                    .HasMaxLength(50)
                    .IsRequired();
                addressBuilder.Property(x => x.AddressLine).HasMaxLength(180);

                addressBuilder
                    .Property(x => x.Country)
                    .HasMaxLength(50)
                    .IsRequired();

                addressBuilder.Property(x => x.State).HasMaxLength(50);

                addressBuilder
                    .Property(x => x.ZipCode)
                    .HasMaxLength(5)
                    .IsRequired();
            }
        );

        builder.ComplexProperty(
            o => o.Payment,
            paymentBuilder =>
            {
                paymentBuilder.Property(x => x.CardName).HasMaxLength(50);
                paymentBuilder
                    .Property(x => x.CardNumber)
                    .HasMaxLength(24)
                    .IsRequired();
                paymentBuilder.Property(x => x.Expiration).HasMaxLength(10);
                paymentBuilder.Property(x => x.CVV).HasMaxLength(3);
                paymentBuilder.Property(x => x.PaymentMethod);
            }
        );

        builder
            .Property(o => o.Status)
            .HasDefaultValue(OrderStatus.Draft)
            .HasConversion(
                s => s.ToString(),
                dbStatus =>
                    (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus)
            );

        builder.Property(o => o.TotalPrice);
    }
}
