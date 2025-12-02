using GymCRM.Data;

namespace GymCRM.Services
{
    public interface IPricingService
    {
        decimal Estimate(int? planId, string? coupon);
    }
}