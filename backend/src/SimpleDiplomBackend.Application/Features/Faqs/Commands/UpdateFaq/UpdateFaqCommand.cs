using Mediator;
using SimpleDiplomBackend.Application.Features.Faqs.Interfaces;
using SimpleDiplomBackend.Application.Shared.Exceptions;

namespace SimpleDiplomBackend.Application.Features.Faqs.Commands.UpdateFaq
{
    public record UpdateFaqCommand : IRequest
    {
        public int Id { get; set; }
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
    }

    public class UpdateFaqCommandHandler : IRequestHandler<UpdateFaqCommand>
    {
        private readonly IFaqRepository _faqsReporsitory;

        public UpdateFaqCommandHandler(IFaqRepository dishRepository)
        {
            _faqsReporsitory = dishRepository;
        }

        public async ValueTask<Unit> Handle(UpdateFaqCommand request, CancellationToken cancellationToken)
        {

            var entity = await _faqsReporsitory.GetById(request.Id);
            // check if dish was found.
            if (entity == null)
            {
                throw new NotFoundException(nameof(Faqs), request.Id);
            }

            if (request.Question != null)
            {
                entity.Question = request.Question;
            }
            if (request.Answer != null)
            {
                entity.Answer = request.Answer;
            }

            // update dish record
            await _faqsReporsitory.Update(entity);

            return Unit.Value;

        }
    }
}