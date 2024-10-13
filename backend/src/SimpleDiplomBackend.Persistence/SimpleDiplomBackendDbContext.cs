using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleDiplomBackend.Application.Shared.Interface;
using SimpleDiplomBackend.Domain.Entities;
using SimpleDiplomBackend.Domain.Shared;
using SimpleDiplomBackend.Infrastructure.Auth;

namespace SimpleDiplomBackend.Persistence
{
    public class SimpleDiplomBackendDbContext : IdentityDbContext<ApplicationUser>, ISimpleDiplomBackendDbContext
    {
        private readonly ICurrentUserService? _currentUserService;

        public DbSet<ApplicationOption> ApplicationOptions { get; set; }
        public DbSet<Domain.Entities.Booking> Bookings { get; set; }
        public DbSet<BookingDish> BookingDishes { get; set; }
        public DbSet<BookingEvent> BookingEvents { get; set; }
        public DbSet<BookingService> BookingServices { get; set; }
        public DbSet<BookingStatus> BookingStatuses { get; set; }
        public DbSet<CommercialService> CommercialServices { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<DishCategory> DishCategories { get; set; }
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Review> Reviews { get; set; }


        public SimpleDiplomBackendDbContext(DbContextOptions<SimpleDiplomBackendDbContext> options)
            : base(options)
        {
        }

        public SimpleDiplomBackendDbContext(DbContextOptions<SimpleDiplomBackendDbContext> options,
            ICurrentUserService currentUserService)
            : base(options)
        {
            _currentUserService = currentUserService;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService?.UserId ?? string.Empty;
                        entry.Entity.CreatedOn = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService?.UserId ?? string.Empty;
                        entry.Entity.LastModifiedOn = DateTime.UtcNow;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // applies all configurations defined within the configurations folder
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SimpleDiplomBackendDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}