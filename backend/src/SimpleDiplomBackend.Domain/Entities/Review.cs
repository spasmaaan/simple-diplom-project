namespace SimpleDiplomBackend.Domain.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public string Message { get; set; } = string.Empty;
        public short? Rating { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
