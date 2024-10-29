namespace SimpleDiplomBackend.Application.Features.Services.Queries.GetServiceImage
{
    public record GetServiceImage
    {
        public string MimeType { get; set; } = string.Empty;
        public byte[] Data { get; set; } = { };
    }
}
