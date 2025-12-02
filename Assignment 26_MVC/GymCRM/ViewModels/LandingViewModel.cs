using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using GymCRM.Model;

namespace GymCRM.ViewModels
{
    public class LandingViewModel
    {
        public IEnumerable<SelectListItem> Countries { get; set; } = new List<SelectListItem>();
        public IEnumerable<MembershipPlan> Plans { get; set; } = new List<MembershipPlan>();
    }
}
