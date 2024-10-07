using Mediator;
using SimpleDiplomBackend.Application.Shared.Interface;

namespace SimpleDiplomBackend.Booking.Features.Faqs.Commands.CreateFaq
{
    public record CreateFaqCommand : IRequest
    {
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
    }

    public class CreateFaqCommandHandler : IRequestHandler<CreateFaqCommand>
    {
        private readonly ISimpleDiplomBackendDbContext _dbContext;

        public CreateFaqCommandHandler(ISimpleDiplomBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async ValueTask<Unit> Handle(CreateFaqCommand request, CancellationToken cancellationToken)
        {

            var entity = new Domain.Entities.Faq
            {
                Question = request.Question,
                Answer = request.Answer,
            };

            _dbContext.Faqs.Add(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

}