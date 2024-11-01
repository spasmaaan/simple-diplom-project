using MediatR;
using SimpleDiplomBackend.Application.Features.Services.Interfaces;
using SimpleDiplomBackend.Application.Shared.Exceptions;
using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Application.Features.Service.Commands.UpdateService
{
    public record UpdateServiceCommand : IRequest<CommercialService>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public byte[]? PreviewImage { get; set; }
        public decimal? Price { get; set; }
        public int? MaxCount { get; set; }
    }

    public class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand, CommercialService>
    {
        private readonly IServicesRepository _servicesReporsitory;

        public UpdateServiceCommandHandler(IServicesRepository dishRepository)
        {
            _servicesReporsitory = dishRepository;
        }

        public async Task<CommercialService> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
        {
            var entity = await _servicesReporsitory.GetById(request.Id);
            // check if dish was found.
            if (entity == null)
            {
                throw new NotFoundException(nameof(CommercialService), request.Id);
            }

            if (request.Name != null)
            {
                entity.Name = request.Name;
            }
            if (request.Description != null)
            {
                entity.Description = request.Description;
            }
            if (request.Price.HasValue)
            {
                entity.Price = request.Price.Value;
            }
            if (request.PreviewImage != null)
            {
                entity.PreviewImage = request.PreviewImage;
            }
            if (request.MaxCount.HasValue)
            {
                entity.MaxCount = request.MaxCount.Value;
            }

            // update dish record
            await _servicesReporsitory.Update(entity);

            return entity;
        }
    }
}