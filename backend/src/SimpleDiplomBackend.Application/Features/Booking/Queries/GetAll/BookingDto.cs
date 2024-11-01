namespace SimpleDiplomBackend.Application.Features.Booking.Queries.GetAll
{
    public class BookingDto
    {
        public long Id { get; set; }
        public string? UserId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public Dictionary<int, int> Dishes { get; set; } = new Dictionary<int, int>();
        public Dictionary<int, int> Services { get; set; } = new Dictionary<int, int>();
    }
}