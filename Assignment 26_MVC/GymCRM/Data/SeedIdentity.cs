using System.Threading.Tasks;
using GymCRM.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace GymCRM.Data
{
    public static class SeedIdentity
    {
        public static async Task Run(IServiceProvider sp)
        {
            var roleMgr = sp.GetRequiredService<RoleManager<IdentityRole>>();
            var userMgr = sp.GetRequiredService<UserManager<ApplicationUser>>();

            const string adminRole = "Admin";
            if (!await roleMgr.RoleExistsAsync(adminRole))
                await roleMgr.CreateAsync(new IdentityRole(adminRole));

            var adminEmail = "admin@gymcrm.local";
            var admin = await userMgr.FindByEmailAsync(adminEmail);
            if (admin == null)
            {
                admin = new ApplicationUser { UserName = adminEmail, Email = adminEmail, DisplayName = "Administrator" };
                await userMgr.CreateAsync(admin, "Admin@12345"); // غيّرها بعد أول تشغيل
                await userMgr.AddToRoleAsync(admin, adminRole);
            }
        }
    }
}
