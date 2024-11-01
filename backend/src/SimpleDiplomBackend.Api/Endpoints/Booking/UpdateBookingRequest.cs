using MediatR.NotificationPublishers;

namespace SimpleDiplomBackend.Api.Endpoints.Bookings
{
    public class UpdateBookingRequest
    {
        public int Id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? StatusId { get; set; }
        public string? Comment { get; set; }
        public Dictionary<int, int>? Dishes { get; set; }
        public Dictionary<int, int>? Services { get; set; }
    }
}