using FluentValidation;
using Registration.Application.Common.Interfaces;
using Registration.Domain.Registrations.ValueObjects;
using DomainRegistration = Registration.Domain.Registrations.Registration;

namespace Registration.Application.Registrations.Commands.CreateRegistration;

/// <summary>
/// Server-side validation for a create-registration request. These rules mirror
/// the client-side rules (defense in depth). Uniqueness of email/mobile is NOT
/// validated here - that is a 409 Conflict handled by the command handler, not a
/// 400 validation error.
/// </summary>
public sealed class CreateRegistrationCommandValidator : AbstractValidator<CreateRegistrationCommand>
{
    private static readonly DateOnly EarliestReasonableBirthDate = new(1900, 1, 1);

    public CreateRegistrationCommandValidator(IApplicationDbContext db, IDateTimeProvider dateTime)
    {
        var today = dateTime.Today;

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .Must(PersonName.IsValidPart!)
            .WithMessage("First name may only contain Arabic or English letters, spaces, hyphens and apostrophes (max 50).");

        // Middle name is optional, but when supplied it must obey the same rules.
        RuleFor(x => x.MiddleName)
            .Must(PersonName.IsValidPart!)
            .WithMessage("Middle name may only contain Arabic or English letters, spaces, hyphens and apostrophes (max 50).")
            .When(x => !string.IsNullOrWhiteSpace(x.MiddleName));

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .Must(PersonName.IsValidPart!)
            .WithMessage("Last name may only contain Arabic or English letters, spaces, hyphens and apostrophes (max 50).");

        RuleFor(x => x.BirthDate)
            .GreaterThanOrEqualTo(EarliestReasonableBirthDate).WithMessage("Birth date is required.")
            .LessThanOrEqualTo(today).WithMessage("Birth date cannot be in the future.")
            .Must(birthDate => BirthDate.IsEligible(birthDate, today))
            .WithMessage($"Registrant must be at least {BirthDate.MinimumAgeYears} years old.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .MaximumLength(Email.MaxLength)
            .Must(Email.IsValid!).WithMessage("Email format is invalid.");

        RuleFor(x => x.Mobile)
            .NotEmpty().WithMessage("Mobile number is required.")
            .Must(MobileNumber.IsValid!)
            .WithMessage("Mobile number must be a valid E.164 number, for example +201006158123.");

        RuleFor(x => x.Addresses)
            .NotEmpty().WithMessage("At least one address is required.")
            .Must(addresses => addresses.Count <= DomainRegistration.MaxAddresses)
            .WithMessage($"A maximum of {DomainRegistration.MaxAddresses} addresses is allowed.")
            .Must(addresses => addresses.Count(a => a.IsPrimary) <= 1)
            .WithMessage("Only one address can be marked as primary.")
            .Must(addresses => addresses.Count <= 1 || addresses.Count(a => a.IsPrimary) == 1)
            .WithMessage("Exactly one address must be marked as primary when more than one address is provided.");

        RuleForEach(x => x.Addresses).SetValidator(new CreateAddressRequestValidator(db));
    }
}
