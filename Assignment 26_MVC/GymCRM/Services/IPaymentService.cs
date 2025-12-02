namespace GymCRM.Services
{
    public interface IPaymentService
    {
        (bool Success, string Ref) Charge(int subscriptionId);
    }
}