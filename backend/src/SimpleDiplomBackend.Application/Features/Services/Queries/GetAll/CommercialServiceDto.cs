namespace SimpleDiplomBackend.Application.Features.Service.Queries.GetAll
{
    public class CommercialServiceDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}