using GymCRM.ViewModels;

public class Step3ReviewViewModel
{
    public Step1SelectViewModel Step1 { get; set; } = new();
    public Step2PersonalViewModel Step2 { get; set; } = new();
    public decimal Subtotal { get; set; }
    public decimal Vat { get; set; }
    public decimal Total { get; set; }

    // أسماء للعرض
    public string? CityName { get; set; }
    public string? BranchName { get; set; }
    public string? PlanTitle { get; set; }
}
