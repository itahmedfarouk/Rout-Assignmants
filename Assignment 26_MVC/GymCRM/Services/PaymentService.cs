using GymCRM.Services;

public class PaymentService : IPaymentService
{
    public (bool Success, string Ref) Charge(int subscriptionId)
        => (true, $"GYMCRM-{subscriptionId}-{DateTime.UtcNow:yyyyMMddHHmmss}");
}