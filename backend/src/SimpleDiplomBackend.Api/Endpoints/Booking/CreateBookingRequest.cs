namespace SimpleDiplomBackend.Api.Endpoints.Bookings
{
    public class CreateBookingRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Dictionary<int, int>? Dishes { get; set; }
        public Dictionary<int, int>? Services { get; set; }
    }
}