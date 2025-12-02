using GymCRM.Data;

namespace GymCRM.Services
{
    public class PricingService : IPricingService
    {
        private readonly GymCRMContext _db;
        const decimal VAT = 0.15m;
        public PricingService(GymCRMContext db) { _db = db; }

        public decimal Estimate(int? planId, string? coupon)
        {
            if (planId is null) return 0;
            var plan = _db.MembershipPlans.Find(planId);
            if (plan == null) return 0;

            var subtotal = plan.PriceMonthly; // كبداية: نحسب الشهري
            var discount = 0m;

            if (!string.IsNullOrWhiteSpace(coupon))
            {
                var c = _db.Coupons.FirstOrDefault(x => x.Code == coupon && x.IsActive &&
                    (x.ExpiresAt == null || x.ExpiresAt > DateTime.UtcNow));

                if (c != null)
                    discount = c.AmountOff ?? (subtotal * (c.PercentOff ?? 0) / 100m);
            }

            subtotal = Math.Max(0, subtotal - discount);
            var vat = subtotal * VAT;
            return Math.Round(subtotal + vat, 2);
        }
    }
}

