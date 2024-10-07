namespace SimpleDiplomBackend.Domain.Entities
{
    public class BookingEvent
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public int BookingId { get; set; }
        public string Message { get; set; } = string.Empty;
        public byte[]? Image { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
