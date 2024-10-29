using Microsoft.EntityFrameworkCore;
using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Persistence.SeedData.Reviews
{
    internal static class ReviewsData
    {
        private static readonly Review[] _reviews = (new (int, DateTime, string, string, short?)[]
            {
                (1, new DateTime(2015, 3, 2), "admin@admin.ru", "Что-то я написал.", 1),
                (2, new DateTime(2015, 3, 2), "admin@admin.ru", "Что-то я написал.", 4),
                (3, new DateTime(2015, 3, 2), "admin@admin.ru", "Что-то я написал.", 5),
                (4, new DateTime(2015, 3, 2), "admin@admin.ru", "Что-то я написал.", null),
            })
            .Select(((int Id, DateTime CreationDate, string UserId, string Message, short? Rating) review) => new Review
            {
                Id = review.Id,
                CreationDate = review.CreationDate.ToUniversalTime(),
                UserId = review.UserId,
                Message = review.Message,
                Rating = review.Rating,
            })
            .ToArray();

        public async static Task FillData(this DbSet<Review> dbReviews)
        {
            await dbReviews.AddRangeAsync(_reviews);
        }
    }
}
