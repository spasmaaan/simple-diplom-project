using Microsoft.EntityFrameworkCore;
using SimpleDiplomBackend.Domain.Entities;
using SimpleDiplomBackend.Domain.Shared;

namespace SimpleDiplomBackend.Persistence.SeedData.Bookings
{
    internal static class BookingsData
    {
        private static readonly BookingStatus[] _bookingStatuses = (new (int, string)[]
            {
                ((int)BookingStatusType.Processing, "Ожидает отправки"),
                ((int)BookingStatusType.Approved, "На подтверждении"),
                ((int)BookingStatusType.Paid, "Ожидание оплаты"),
                ((int)BookingStatusType.InProgress, "Забронировано"),
                ((int)BookingStatusType.Completed, "Выполнено"),
                ((int)BookingStatusType.Rejected, "Отклонено"),
            })
            .Select(((int Id, string Name) dishLink) => new BookingStatus
            {
                Id = dishLink.Id,
                Name = dishLink.Name
            })
            .ToArray();

        private static readonly Domain.Entities.Booking[] _bookings = (new (int, DateTime, string, DateTime, DateTime, int)[]
            {
                (1, new DateTime(2024, 3, 2), "admin@admin.ru", new DateTime(2024, 3, 3), new DateTime(2024, 3, 4), (int)BookingStatusType.Paid),
            })
            .Select(((int Id, DateTime CreationDate, string UserId, DateTime StartDate, DateTime EndDate, int StatusId) booking) => new Domain.Entities.Booking
            {
                Id = booking.Id,
                UserId = booking.UserId,
                CreationDate = booking.CreationDate.ToUniversalTime(),
                StartDate = booking.StartDate.ToUniversalTime(),
                EndDate = booking.EndDate.ToUniversalTime(),
                StatusId = booking.StatusId,
            })
            .ToArray();

        private static readonly BookingDish[] _bookingDishes = (new (int, int, int, int)[]
            {
                (1, 1, 1, 2),
            })
            .Select(((int Id, int BookingId, int DishId, int Count) dishLink) => new BookingDish
            {
                Id = dishLink.Id,
                BookingId = dishLink.BookingId,
                DishId = dishLink.DishId,
                Count = dishLink.Count
            })
            .ToArray();

        private static readonly BookingService[] _bookingServices = (new (int, int, int, int)[]
            {
                (1, 1, 1, 1),
            })
            .Select(((int Id, int BookingId, int ServiceId, int Count) dishLink) => new BookingService
            {
                Id = dishLink.Id,
                BookingId = dishLink.BookingId,
                ServiceId = dishLink.ServiceId,
                Count = dishLink.Count
            })
            .ToArray();



        public async static Task FillData(this DbSet<Domain.Entities.Booking> dbBookings)
        {
            await dbBookings.AddRangeAsync(_bookings);
        }

        public async static Task FillData(this DbSet<BookingDish> dbBookingDishes)
        {
            await dbBookingDishes.AddRangeAsync(_bookingDishes);
        }

        public async static Task FillData(this DbSet<BookingService> dbBookingServices)
        {
            await dbBookingServices.AddRangeAsync(_bookingServices);
        }

        public async static Task FillData(this DbSet<BookingStatus> dbBookingStatus)
        {
            await dbBookingStatus.AddRangeAsync(_bookingStatuses);
        }
    }
}
