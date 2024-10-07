using Microsoft.EntityFrameworkCore;
using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Application.Shared.Interface
{
    public interface ISimpleDiplomBackendDbContext
    {
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

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}