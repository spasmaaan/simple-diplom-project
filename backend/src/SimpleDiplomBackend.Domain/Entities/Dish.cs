namespace SimpleDiplomBackend.Domain.Entities
{
    public class Dish
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public byte[]? PreviewImage { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public DishCategory Catergory { get; set; } = new DishCategory();

    }
}