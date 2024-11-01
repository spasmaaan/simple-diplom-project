namespace SimpleDiplomBackend.Application.Features.Booking.Interfaces
{
    public interface IBookingRepository
    {
        Task<Domain.Entities.Booking?> GetById(int id);
        Task<IEnumerable<Domain.Entities.Booking>> GetAll(int offset, int limit);
        Task Add(Domain.Entities.Booking booking);
        Task Update(Domain.Entities.Booking booking);
        Task UpdateDishes(Domain.Entities.Booking booking, Dictionary<int, int>? dishes);
        Task UpdateServices(Domain.Entities.Booking booking, Dictionary<int, int>? services);
        Task Remove(int id);
    }
}
