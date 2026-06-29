using FluentValidation;

namespace Registration.Application.Lookups.Queries.GetCities;

public sealed class GetCitiesQueryValidator : AbstractValidator<GetCitiesQuery>
{
    public GetCitiesQueryValidator()
    {
        RuleFor(x => x.GovernorateId)
            .GreaterThan(0).WithMessage("A valid governorate id is required.");
    }
}
