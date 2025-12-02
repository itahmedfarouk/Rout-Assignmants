namespace GymCRM.Model
{
    public class Subscription
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public int BranchId { get; set; }
        public int MembershipPlanId { get; set; }

        public string? AppliedCoupon { get; set; }

        public decimal Subtotal { get; set; }
        public decimal VatAmount { get; set; }
        public decimal Total { get; set; }

        public string Status { get; set; } = "Pending";
        public string? PaymentRef { get; set; }


        public Customer Customer { get; set; } = null!;
        public Branch Branch { get; set; } = null!;
        public MembershipPlan MembershipPlan { get; set; } = null!;

        // 👇 تواريخ
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? PaidAt { get; set; }
        public DateTime? StartDate { get; set; }  
        public DateTime? EndDate { get; set; }    
    }
}
