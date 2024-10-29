namespace SimpleDiplomBackend.Application.Features.Dishes.Queries.GetDishImage
{
    public record DishImageDto
    {
        public string MimeType { get; set; } = string.Empty;
        public byte[] Data { get; set; } = { };
    }
}
