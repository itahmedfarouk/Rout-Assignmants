using GymCRM.Data;
using GymCRM.Model;
using GymCRM.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// MVC + Localization + RuntimeCompilation
builder.Services.AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization()
#if DEBUG
    .AddRazorRuntimeCompilation()
#endif
    ;

// Localization
builder.Services.AddLocalization(opt => opt.ResourcesPath = "Resources");

// EF Core
builder.Services.AddDbContext<GymCRMContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
{
    opt.Password.RequireDigit = true;
    opt.Password.RequiredLength = 8;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireNonAlphanumeric = false;
})
.AddEntityFrameworkStores<GymCRMContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(o =>
{
    o.LoginPath = "/account/login";           // عام للجميع
    o.AccessDeniedPath = "/account/access-denied";
});

// Services
builder.Services.AddScoped<IPricingService, PricingService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<INotificationService, NotificationService>();

// Notification settings & HttpClient
builder.Services.Configure<NotificationOptions>(builder.Configuration.GetSection("Notification"));
builder.Services.AddHttpClient();

// Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// لعرض الأزرار بناءً على السيشن في _Layout
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Supported cultures
var supported = new[] { "ar", "en" };

app.UseHttpsRedirection();
app.UseStaticFiles();

// ✔ خلي اللّوكلزيشن بدري
app.UseRequestLocalization(new RequestLocalizationOptions()
    .SetDefaultCulture("ar")
    .AddSupportedCultures(supported)
    .AddSupportedUICultures(supported));

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

// Root redirect
//app.MapGet("/", () => Results.Redirect("/join/step1"));

// ⛔ لو مش بتستخدم Areas شيل البلوك ده
// app.MapControllerRoute(
//     name: "areas",
//     pattern: "{area:exists}/{controller=Sessions}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Seeding
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<GymCRMContext>();
    db.Database.Migrate();

    Seed.Run(db);
    SeedIdentity.Run(scope.ServiceProvider).GetAwaiter().GetResult();
}

app.Run();
