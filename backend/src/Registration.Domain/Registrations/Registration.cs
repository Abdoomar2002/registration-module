using Registration.Domain.Common;
using Registration.Domain.Registrations.Events;
using Registration.Domain.Registrations.ValueObjects;

namespace Registration.Domain.Registrations;

/// <summary>
/// The Registration aggregate root: a person's profile plus one to five
/// addresses, exactly one of which is primary. All invariants are enforced
/// through the <see cref="Create"/> factory so an instance can never be in an
/// invalid state.
/// </summary>
public sealed class Registration : AggregateRoot<Guid>, IAuditableEntity
{
    public const int MinAddresses = 1;
    public const int MaxAddresses = 5;

    private readonly List<Address> _addresses = new();

    private Registration(Guid id, PersonName name, BirthDate birthDate, Email email, MobileNumber mobile)
        : base(id)
    {
        Name = name;
        BirthDate = birthDate;
        Email = email;
        Mobile = mobile;
    }

    private Registration()
    {
    }

    public PersonName Name { get; private set; } = null!;

    public BirthDate BirthDate { get; private set; } = null!;

    public Email Email { get; private set; } = null!;

    public MobileNumber Mobile { get; private set; } = null!;

    public IReadOnlyCollection<Address> Addresses => _addresses.AsReadOnly();

    // Audit columns - populated by the persistence interceptor.
    public DateTime CreatedAtUtc { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedAtUtc { get; set; }

    public string? UpdatedBy { get; set; }

    public static Registration Create(
        PersonName name,
        BirthDate birthDate,
        Email email,
        MobileNumber mobile,
        IEnumerable<Address> addresses)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(birthDate);
        ArgumentNullException.ThrowIfNull(email);
        ArgumentNullException.ThrowIfNull(mobile);
        ArgumentNullException.ThrowIfNull(addresses);

        var registration = new Registration(Guid.NewGuid(), name, birthDate, email, mobile);
        registration.SetAddresses(addresses);
        registration.RaiseDomainEvent(new RegistrationCreatedDomainEvent(
            registration.Id,
            email.Normalized,
            mobile.Value,
            DateTime.UtcNow));

        return registration;
    }

    /// <summary>The single primary address (guaranteed to exist after creation).</summary>
    public Address PrimaryAddress => _addresses.Single(a => a.IsPrimary);

    private void SetAddresses(IEnumerable<Address> addresses)
    {
        var list = addresses.ToList();

        if (list.Count < MinAddresses)
        {
            throw new DomainException("At least one address is required.");
        }

        if (list.Count > MaxAddresses)
        {
            throw new DomainException($"A maximum of {MaxAddresses} addresses is allowed.");
        }

        // "If only one address exists, it should be treated as primary."
        if (list.Count == 1)
        {
            list[0].MarkAsPrimary();
        }
        else
        {
            var primaryCount = list.Count(a => a.IsPrimary);
            if (primaryCount == 0)
            {
                throw new DomainException("One address must be marked as primary.");
            }

            if (primaryCount > 1)
            {
                throw new DomainException("Only one address can be marked as primary.");
            }
        }

        _addresses.AddRange(list);
    }
}
