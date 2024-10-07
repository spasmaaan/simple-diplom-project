namespace SimpleDiplomBackend.Application.Features.Booking.Interfaces
{
    public interface IBookingRepository
    {
        Task<Domain.Entities.Booking?> GetById(int id);
        Task<IEnumerable<Domain.Entities.Booking>> GetAll(int offset, int limit);
        Task Add(Domain.Entities.Booking booking);
        Task Update(Domain.Entities.Booking booking);
        Task Remove(int id);
    }
}