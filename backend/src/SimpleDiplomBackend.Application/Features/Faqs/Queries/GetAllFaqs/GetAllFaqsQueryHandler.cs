using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SimpleDiplomBackend.Application.Shared.Extensions;
using SimpleDiplomBackend.Application.Shared.Interface;
using SimpleDiplomBackend.Application.Shared.Models;

namespace SimpleDiplomBackend.Application.Features.Faqs.Queries.GetAllFaqs
{
    public sealed class GetAllFaqsQueryHandler : IRequestHandler<GetAllFaqsQuery, PaginatedList<GetFaqDto>>
    {
        private readonly ISimpleDiplomBackendDbContext _dbContext;
        private readonly ILogger<GetAllFaqsQueryHandler> _logger;

        public GetAllFaqsQueryHandler(ISimpleDiplomBackendDbContext dbContext, ILogger<GetAllFaqsQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<PaginatedList<GetFaqDto>> Handle(GetAllFaqsQuery request, CancellationToken cancellationToken)
        {
            var faqQuery = _dbContext.Faqs.AsQueryable();

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                faqQuery = faqQuery.Where(q => q.Question.Contains(request.SearchTerm.Trim()));
            }

            var faqs = await faqQuery
                .AsNoTracking()
                .Select(f => new GetFaqDto
                {
                    Id = f.Id,
                    Question = f.Question,
                    Answer = f.Answer
                })
                .PaginatedListAsync(request.Offset, request.Limit, cancellationToken);

            _logger.LogInformation("retrieved FAQS.");

            return faqs;
        }
    }
}