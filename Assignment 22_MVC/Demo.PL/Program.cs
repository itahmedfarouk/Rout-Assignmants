using Demo.BLL;
using Demo.BLL.MappingProfiles;
using Demo.BLL.Services.Classes;
using Demo.BLL.Services.Interfaces;
using Demo.DAL.Data.Contexts;
using Demo.DAL.Repositories.Classes;
using Demo.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);// Create a builder for the web application.

            #region Configure Services : Add Services To The DI Container
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //builder.Services.AddScoped<ApplicationDbContext>();// Register Services  
            // Give CLR Permission To Inject This Service If Needed

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                ////// 3 Ways To Get Connection String From appsettings.json //
                ////var connString = builder.Configuration["ConnectionStrings:DefaultConnection"];
                ////var connString = builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"];
                var connString = builder.Configuration.GetConnectionString("DefaultConnection");

                options.UseSqlServer(connString); // Use SQL Server with the provided connection string.
            });
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>(); // Register the DepartmentRepository for dependency injection.
            builder.Services.AddScoped<IDepartmentServices, DepartmentServices>(); // Register the DepartmentRepository for dependency injection.
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>(); // Register the DepartmentRepository for dependency injection.
            builder.Services.AddScoped<IEmployeeServices, EmployeeServices>(); // Register the DepartmentRepository for dependency injection.


            //builder.Services.AddAutoMapper(typeof(ProjectReferece).Assembly); // XXXXXXXXXX
            builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));

            #endregion


            var app = builder.Build();// Build the application.

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();// Enforces the use of HTTPS.
            }

            app.UseHttpsRedirection(); // Redirects HTTP requests to HTTPS.
            app.UseStaticFiles(); // Enables serving static files like CSS, JS, images, etc.

            app.UseRouting(); // Adds route matching to the middleware pipeline.

            ////app.UseAuthentication(); // Adds authentication capabilities to the middleware pipeline.
            ////app.UseAuthorization(); // Adds authorization capabilities to the middleware pipeline.

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();// Runs the application.
        }
    }
}
