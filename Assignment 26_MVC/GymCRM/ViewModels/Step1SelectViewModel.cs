using Microsoft.AspNetCore.Mvc.Rendering;
using GymCRM.Model;

namespace GymCRM.ViewModels
{
    public class Step1SelectViewModel
    {
        public Gender? Gender { get; set; }
        public int? CountryId { get; set; }   // 👈 جديد
        public int? CityId { get; set; }
        public int? BranchId { get; set; }
        public int? PlanId { get; set; }
        public string? CouponCode { get; set; }
        public decimal? EstimatedTotal { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; } = Enumerable.Empty<SelectListItem>(); // جديد
        public IEnumerable<SelectListItem> Cities { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Branches { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Plans { get; set; } = Enumerable.Empty<SelectListItem>();
    }
}
