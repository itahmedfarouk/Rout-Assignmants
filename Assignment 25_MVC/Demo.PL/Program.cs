using Demo.DAL.Data.DBContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Demo.DAL.Repositories.Interfaces;
using Demo.DAL.Repositories.Classes;
using Demo.BL2.Services.Interfaces;
using Demo.BL2.Services.Classes;
using Demo.BL2.Mapping_Profiles;
using Microsoft.AspNetCore.Mvc;
using Demo.BL2.Services.AttachmentService;
using Microsoft.AspNetCore.Identity;
using Demo.DAL.Models.IdentityModels;

namespace Demo.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDBContext>(options =>
            {
                // get connection string from appsettings.json
                var ConString = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(ConString).UseLazyLoadingProxies();
            });

            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IAttachmentService, AttachmentService>();

            builder.Services.AddAutoMapper(m => m.AddProfile(new MappingProfiles()));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                //options.User.RequireUniqueEmail = true;
                //options.Password.RequireLowercase = true;
                //options.Password.RequireUppercase = true;
            })
            .AddEntityFrameworkStores<ApplicationDBContext>()
            .AddDefaultTokenProviders(); 

            builder.Services.AddControllersWithViews(opt =>
            {
                // Global CSRF protection for all POST methods
                opt.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Register}/{id?}");

            app.Run();
        }
    }
}
