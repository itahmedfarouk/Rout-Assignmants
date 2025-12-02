using GymCRM.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GymCRM.Data
{
    // 👇 ضروري لIdentity
    public class GymCRMContext : IdentityDbContext<ApplicationUser>
    {
        public GymCRMContext(DbContextOptions<GymCRMContext> opts) : base(opts) { }

        public DbSet<Country> Countries => Set<Country>();
        public DbSet<City> Cities => Set<City>();
        public DbSet<Branch> Branches => Set<Branch>();
        public DbSet<MembershipPlan> MembershipPlans => Set<MembershipPlan>();
        public DbSet<Coupon> Coupons => Set<Coupon>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Subscription> Subscriptions => Set<Subscription>();

        public DbSet<SessionType> SessionTypes => Set<SessionType>();
        public DbSet<ScheduledSession> ScheduledSessions => Set<ScheduledSession>();
        public DbSet<SessionBooking> SessionBookings => Set<SessionBooking>();

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // 👈 مهم لIdentity

            modelBuilder.Entity<Coupon>()
                .Property(c => c.PercentOff)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Coupon>()
                .Property(c => c.AmountOff)
                .HasPrecision(18, 2);

            modelBuilder.Entity<MembershipPlan>().Property(p => p.PriceMonthly).HasPrecision(18, 2);
            modelBuilder.Entity<MembershipPlan>().Property(p => p.PriceUpfront).HasPrecision(18, 2);

            modelBuilder.Entity<Subscription>().Property(s => s.Subtotal).HasPrecision(18, 2);
            modelBuilder.Entity<Subscription>().Property(s => s.VatAmount).HasPrecision(18, 2);
            modelBuilder.Entity<Subscription>().Property(s => s.Total).HasPrecision(18, 2);
        }
    }
}
