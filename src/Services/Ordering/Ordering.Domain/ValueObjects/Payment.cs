namespace Ordering.Domain.ValueObjects;

public record Payment
{
    private const int CvvMaxLength = 3;

    protected Payment() { }

    private Payment(
        string cardName,
        string cardNumber,
        string expiration,
        string cvv,
        string paymentMethod
    )
    {
        CardName = cardName;
        CardNumber = cardNumber;
        Expiration = expiration;
        CVV = cvv;
        PaymentMethod = paymentMethod;
    }

    public string? CardName { get; } = default!;
    public string CardNumber { get; } = default!;
    public string Expiration { get; } = default!;
    public string CVV { get; } = default!;
    public string PaymentMethod { get; } = default!;

    public static Payment Of(
        string cardName,
        string cardNumber,
        string expiration,
        string cvv,
        string paymentMethod
    )
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(cardName);
        ArgumentException.ThrowIfNullOrWhiteSpace(cardNumber);
        ArgumentException.ThrowIfNullOrWhiteSpace(cvv);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(cvv.Length, CvvMaxLength);

        return new Payment(cardName, cardNumber, expiration, cvv, paymentMethod);
    }
}
