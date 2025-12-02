namespace GymCRM.Model
{
    public class Coupon
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public decimal? PercentOff { get; set; }
        public decimal? AmountOff { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
