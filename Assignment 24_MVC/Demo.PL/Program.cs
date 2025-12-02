
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


namespace Demo.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDBContext>(options => {
				// get connection string from appsettings.json
				//var ConString = builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"];
				var ConString = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(ConString).UseLazyLoadingProxies();
			});
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IAttachmentService, AttachmentService>();
			builder.Services.AddAutoMapper(m=>m.AddProfile(new MappingProfiles()));
			builder.Services.AddControllersWithViews(opt =>
            {
                opt.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()); // Global CSRF protection for all POST methods to prevent CSRF attacks 
			});

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
