namespace SimpleDiplomBackend.Application.Features.Dishes.Queries.GetCategoryImage
{
    public record DishCategoryImageDto
    {
        public string MimeType { get; set; } = string.Empty;
        public byte[] Data { get; set; } = { };
    }
}
