using Microsoft.EntityFrameworkCore;
using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Persistence.SeedData.ApplicationOptions
{
    internal static class ApplicationOptionsData
    {
        private static readonly ApplicationOption[] _options = (new (string, string)[]
            {
                 ("Test", "{}"),
            })
            .Select(((string Id, string Value) option) => new ApplicationOption
            {
                Id = option.Id,
                Value = option.Value,
            })
            .ToArray();

        public async static Task FillData(this DbSet<ApplicationOption> dbApplicationOptions)
        {
            await dbApplicationOptions.AddRangeAsync(_options);
        }
    }
}
