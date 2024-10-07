namespace SimpleDiplomBackend.Api.Endpoints.Dishes
{
    public class UpdateDishCategoryRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public byte[]? PreviewImage { get; set; }
    }
}