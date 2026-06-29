using FluentAssertions;
using FluentValidation;
using MediatR;
using Registration.Application.Common.Behaviors;
using ValidationException = Registration.Application.Common.Exceptions.ValidationException;

namespace Registration.Application.UnitTests.Behaviors;

public class ValidationBehaviorTests
{
    public sealed record SampleRequest(string Name) : IRequest<string>;

    private sealed class SampleValidator : AbstractValidator<SampleRequest>
    {
        public SampleValidator() => RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
    }

    [Fact]
    public async Task Throws_ValidationException_WhenInvalid()
    {
        var behavior = new ValidationBehavior<SampleRequest, string>(new[] { new SampleValidator() });

        var act = () => behavior.Handle(
            new SampleRequest(string.Empty),
            () => Task.FromResult("handler"),
            CancellationToken.None);

        var exception = (await act.Should().ThrowAsync<ValidationException>()).Which;
        exception.Errors.Should().ContainKey(nameof(SampleRequest.Name));
    }

    [Fact]
    public async Task CallsNext_WhenValid()
    {
        var behavior = new ValidationBehavior<SampleRequest, string>(new[] { new SampleValidator() });
        var nextCalled = false;

        var response = await behavior.Handle(
            new SampleRequest("Abdo"),
            () =>
            {
                nextCalled = true;
                return Task.FromResult("handler");
            },
            CancellationToken.None);

        nextCalled.Should().BeTrue();
        response.Should().Be("handler");
    }

    [Fact]
    public async Task CallsNext_WhenNoValidatorsRegistered()
    {
        var behavior = new ValidationBehavior<SampleRequest, string>(Array.Empty<IValidator<SampleRequest>>());

        var response = await behavior.Handle(
            new SampleRequest(string.Empty),
            () => Task.FromResult("handler"),
            CancellationToken.None);

        response.Should().Be("handler");
    }
}
