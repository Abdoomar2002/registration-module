using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Registration.Application.Common.Interfaces;
using DomainAddress = Registration.Domain.Registrations.Address;

namespace Registration.Application.Registrations.Commands.CreateRegistration;

/// <summary>
/// Validates a single address. Field rules mirror the Address domain invariants;
/// the governorate/city existence and "city belongs to governorate" rules are
/// checked asynchronously against the lookup tables.
/// </summary>
public sealed class CreateAddressRequestValidator : AbstractValidator<CreateAddressRequest>
{
    // Mirrors the Address domain rule: letters, digits, slash, dash and spaces.
    private const string BuildingOrFlatPattern = @"^[A-Za-z0-9/\- ]+$";

    public CreateAddressRequestValidator(IApplicationDbContext db)
    {
        RuleFor(x => x.GovernorateId)
            .GreaterThan(0).WithMessage("Governorate is required.");

        RuleFor(x => x.CityId)
            .GreaterThan(0).WithMessage("City is required.");

        RuleFor(x => x.Street)
            .NotEmpty().WithMessage("Street is required.")
            .MaximumLength(DomainAddress.StreetMaxLength);

        RuleFor(x => x.BuildingNumber)
            .NotEmpty().WithMessage("Building number is required.")
            .MaximumLength(DomainAddress.BuildingNumberMaxLength)
            .Matches(BuildingOrFlatPattern)
            .WithMessage("Building number may only contain letters, numbers, spaces, slashes and dashes.");

        RuleFor(x => x.FlatNumber)
            .NotEmpty().WithMessage("Flat number is required.")
            .MaximumLength(DomainAddress.FlatNumberMaxLength)
            .Matches(BuildingOrFlatPattern)
            .WithMessage("Flat number may only contain letters, numbers, spaces, slashes and dashes.");

        RuleFor(x => x.GovernorateId)
            .MustAsync((id, ct) => db.Governorates.AnyAsync(g => g.Id == id && g.IsActive, ct))
            .WithMessage("The selected governorate does not exist.")
            .When(x => x.GovernorateId > 0);

        // City must exist, be active, AND belong to the selected governorate.
        RuleFor(x => x.CityId)
            .MustAsync((address, _, ct) =>
                db.Cities.AnyAsync(
                    c => c.Id == address.CityId && c.IsActive && c.GovernorateId == address.GovernorateId,
                    ct))
            .WithMessage("The selected city does not exist or does not belong to the selected governorate.")
            .When(x => x.GovernorateId > 0 && x.CityId > 0);
    }
}
