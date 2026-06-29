using FluentValidation;

namespace Registration.Application.Registrations.Queries.ListRegistrations;

public sealed class ListRegistrationsQueryValidator : AbstractValidator<ListRegistrationsQuery>
{
    public const int MaxPageSize = 100;

    public ListRegistrationsQueryValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1).WithMessage("Page must be 1 or greater.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, MaxPageSize)
            .WithMessage($"Page size must be between 1 and {MaxPageSize}.");
    }
}
