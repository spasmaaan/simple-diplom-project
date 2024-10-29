namespace SimpleDiplomBackend.Application.Features.Photos.Queries.GetImage
{
    public record PhotoImageDto
    {
        public string MimeType { get; set; } = string.Empty;
        public byte[] Data { get; set; } = { };
    }
}
