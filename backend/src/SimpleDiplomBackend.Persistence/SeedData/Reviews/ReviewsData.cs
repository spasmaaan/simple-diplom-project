using Microsoft.EntityFrameworkCore;
using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Persistence.SeedData.Reviews
{
    internal static class ReviewsData
    {
        private static readonly Review[] _reviews = (new (int, DateTime, string, string, short?)[]
            {
                (1, new DateTime(2024, 6, 2, 17, 54, 11), "m@t.test", "Слишком много раз прокатились ввех и вниз.", 1),
                (2, new DateTime(2024, 7, 2, 14, 34, 1), "t1@t.test", "Неплохо провели свидание.", 4),
                (3, new DateTime(2024, 7, 2, 12, 22, 20), "t2@t.test", "Отличное место! Всем рекомендую!", 5),
                (4, new DateTime(2024, 8, 2, 11, 4, 45), "admin@admin.ru", "Всё прошло замечательно.", null),
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
