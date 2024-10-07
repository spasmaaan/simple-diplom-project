namespace SimpleDiplomBackend.Application.Features.Application.Queries.GetAll
{
    public class ApplicationOptionDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public byte[]? PreviewImage { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}