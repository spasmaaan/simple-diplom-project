﻿namespace SimpleDiplomBackend.Application.Features.Dishes.Queries.GetAllDishCategories
{
    public class DishCategoryDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public byte[]? PreviewImage { get; set; }
    }
}