namespace SimpleDiplomBackend.Api.Endpoints.Services
{
    public class CreateServiceRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PreviewImage { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}