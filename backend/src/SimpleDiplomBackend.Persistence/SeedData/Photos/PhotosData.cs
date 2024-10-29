using Microsoft.EntityFrameworkCore;
using SimpleDiplomBackend.Domain.Entities;
using SimpleDiplomBackend.Application.Shared.Utilities;

namespace SimpleDiplomBackend.Persistence.SeedData.Photos
{
    internal static class PhotosData
    {
        private static readonly Photo[] _photos = (new (int, string)[]
            {
                        (1, "1.jpg"),
                        (2, "2.jpg"),
                        (3, "3.jpg"),
                        (4, "4.jpg"),
                        (5, "5.jpg"),
                        (6, "6.jpg"),
                        (7, "7.jpg"),
                        (8, "8.jpg"),
                        (9, "9.jpg"),
                        (10, "10.jpg"),
                        (11, "11.jpg"),
                        (12, "12.jpg"),
                        (13, "13.jpg"),
            })
            .Select(((int Id, string FileName) photo) => new Photo
            {
                Id = photo.Id,
                MimeType = DataFileLoaderUtilities.GetFileMimeType(photo.FileName),
                Image = DataFileLoaderUtilities.GetFileData($"SeedData/{nameof(Photos)}/Images/{photo.FileName}")
            })
            .ToArray();

        public async static Task FillData(this DbSet<Photo> dbFaqs)
        {
            await dbFaqs.AddRangeAsync(_photos);
        }
    }
}
