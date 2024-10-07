namespace SimpleDiplomBackend.Domain.Entities
{
    public class CommercialService
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public byte[]? PreviewImage { get; set; }
        public decimal Price { get; set; }
    }
}
