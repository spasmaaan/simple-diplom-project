namespace SimpleDiplomBackend.Api.Endpoints.Dishes
{
    public class CreateDishRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PreviewImage { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}