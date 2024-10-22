using MediatR;
using SimpleDiplomBackend.Application.Shared.Interface;
using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Booking.Features.Faqs.Commands.CreateFaq
{
    public record CreateFaqCommand : IRequest<Faq>
    {
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
    }

    public class CreateFaqCommandHandler : IRequestHandler<CreateFaqCommand, Faq>
    {
        private readonly ISimpleDiplomBackendDbContext _dbContext;

        public CreateFaqCommandHandler(ISimpleDiplomBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Faq> Handle(CreateFaqCommand request, CancellationToken cancellationToken)
        {

            var entity = new Faq
            {
                Question = request.Question,
                Answer = request.Answer,
            };

            var added = _dbContext.Faqs.Add(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return added.Entity;
        }
    }

}