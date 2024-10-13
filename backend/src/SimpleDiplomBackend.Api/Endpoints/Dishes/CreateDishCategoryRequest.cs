namespace SimpleDiplomBackend.Api.Endpoints.Dishes
{
    public class CreateDishCategoryRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PreviewImage { get; set; } = string.Empty;
    }
}