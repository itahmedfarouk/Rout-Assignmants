namespace GymCRM.Model
{
    public class MembershipPlan
    {
        public int Id { get; set; }
        public string TitleAr { get; set; } = null!;
        public string TitleEn { get; set; } = null!;
        public decimal PriceMonthly { get; set; }
        public decimal? PriceUpfront { get; set; }
        public bool IsInstallmentSupported { get; set; }

        public int DurationMonths { get; set; } = 1;
    }
}
