using FluentValidation;

namespace SimpleDiplomBackend.Application.Features.Authentication.Queries.GetProfile
{
    public class GetProfileQueryValidator : AbstractValidator<GetProfileQuery>
    {
        public GetProfileQueryValidator()
        {
            RuleFor(v => v.AccessToken)
                .NotEmpty().WithMessage("AccessToken field is required.");
        }
    }
}