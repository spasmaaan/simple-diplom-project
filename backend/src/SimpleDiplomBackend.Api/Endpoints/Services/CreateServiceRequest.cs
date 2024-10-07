namespace SimpleDiplomBackend.Api.Endpoints.Services
{
    public class CreateServiceRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public byte[]? PreviewImage { get; set; }
        public decimal Price { get; set; }
    }
}