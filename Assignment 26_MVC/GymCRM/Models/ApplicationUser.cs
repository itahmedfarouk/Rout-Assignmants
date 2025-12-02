using Microsoft.AspNetCore.Identity;

namespace GymCRM.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string? DisplayName { get; set; }
    }
}
