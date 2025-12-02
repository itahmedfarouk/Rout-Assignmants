using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymCRM.ViewModels
{
    public class AdminDashboardViewModel
    {
        public int? SelectedCountryId { get; set; }
        public int? SelectedCityId { get; set; }
        public int? SelectedBranchId { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; } = new List<SelectListItem>();

        public int TotalSubs { get; set; }
        public int ActiveSubs { get; set; }
        public decimal Revenue { get; set; }

        public List<PerPlanRow> PerPlan { get; set; } = new();
        public List<PerCityRow> PerCity { get; set; } = new();

        // تفاصيل الفرع المختار
        public List<BranchSubRow> BranchSubscriptions { get; set; } = new();

        public class PerPlanRow
        {
            public string Plan { get; set; } = "";
            public int Count { get; set; }
            public decimal Revenue { get; set; }
        }

        public class PerCityRow
        {
            public string City { get; set; } = "";
            public int Count { get; set; }
        }

        public class BranchSubRow
        {
            public int SubscriptionId { get; set; }
            public string Customer { get; set; } = "";
            public string Plan { get; set; } = "";
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public string Status { get; set; } = "";
        }
    }
}
