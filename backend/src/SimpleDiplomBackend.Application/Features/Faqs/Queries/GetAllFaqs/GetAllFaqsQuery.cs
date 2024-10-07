using Mediator;
using SimpleDiplomBackend.Application.Shared.Models;

namespace SimpleDiplomBackend.Application.Features.Faqs.Queries.GetAllFaqs
{
    public sealed record GetAllFaqsQuery : IRequest<PaginatedList<GetFaqDto>>
    {
        public string SearchTerm { get; set; } = string.Empty;
        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}