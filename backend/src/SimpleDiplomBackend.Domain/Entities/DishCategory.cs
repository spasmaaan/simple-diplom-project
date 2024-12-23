﻿namespace SimpleDiplomBackend.Domain.Entities
{
    public class DishCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PreviewMimeType { get; set; } = string.Empty;
        public byte[]? PreviewImage { get; set; }
        // public ICollection<Dish> Dishes { get; set; } = new List<Dish>();
    }
}