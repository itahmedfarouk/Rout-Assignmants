namespace GymCRM.ViewModels
{
    public class UserDashboardViewModel
    {
        public int CustomerId { get; set; }
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";

        public int? SubscriptionId { get; set; }
        public string? Status { get; set; }
        public string? PaymentRef { get; set; }
        public string? AppliedCoupon { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? Vat { get; set; }
        public decimal? Total { get; set; }

        public string? PlanTitle { get; set; }
        public string? BranchName { get; set; }
        public string? CityName { get; set; }

        // 👇 جديد
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? DaysRemaining { get; set; }
        public bool CanStartNew { get; set; }

        public bool JustPaid { get; set; }
        public List<BookingItemViewModel> Bookings { get; set; } = new();

    }
}
