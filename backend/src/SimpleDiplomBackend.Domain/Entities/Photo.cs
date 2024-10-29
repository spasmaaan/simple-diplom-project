namespace SimpleDiplomBackend.Domain.Entities
{
    public class Photo
    {
        public int Id { get; set; }
        public string MimeType { get; set; } = string.Empty;
        public byte[]? Image { get; set; }
    }
}
