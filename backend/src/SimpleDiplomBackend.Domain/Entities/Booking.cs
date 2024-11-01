namespace SimpleDiplomBackend.Domain.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public required int StatusId { get; set; }
        public string Comment { get; set; } = string.Empty;
    }
}
