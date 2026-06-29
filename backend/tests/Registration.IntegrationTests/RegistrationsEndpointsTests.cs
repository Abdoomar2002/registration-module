using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Registration.Application.Registrations.Commands.CreateRegistration;
using Registration.Application.Registrations.Dtos;

namespace Registration.IntegrationTests;

[Collection(nameof(RegistrationApiCollection))]
public sealed class RegistrationsEndpointsTests
{
    private readonly HttpClient _client;

    public RegistrationsEndpointsTests(RegistrationApiFactory factory) => _client = factory.CreateClient();

    [Fact]
    public async Task Create_Valid_Returns201_WithLocation_AndIsRetrievable()
    {
        var command = TestData.ValidCommand();

        var response = await _client.PostAsJsonAsync("/api/registrations", command);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location.Should().NotBeNull();

        var created = await response.Content.ReadFromJsonAsync<CreateRegistrationResult>();
        created!.Id.Should().NotBeEmpty();

        var details = await _client.GetFromJsonAsync<RegistrationDetailsDto>($"/api/registrations/{created.Id}");
        details!.Email.Should().Be(command.Email);
        details.Addresses.Should().ContainSingle(a => a.IsPrimary);
        details.Addresses[0].GovernorateName.Should().Be("Cairo");
        details.Addresses[0].CityName.Should().Be("Nasr City");
    }

    [Fact]
    public async Task Create_DuplicateEmail_Returns409()
    {
        var email = $"dupe-{Guid.NewGuid():N}@example.com";
        (await _client.PostAsJsonAsync("/api/registrations", TestData.ValidCommand(email: email)))
            .StatusCode.Should().Be(HttpStatusCode.Created);

        // Same email (different case + different mobile) must conflict.
        var duplicate = TestData.ValidCommand(email: email.ToUpperInvariant());

        var response = await _client.PostAsJsonAsync("/api/registrations", duplicate);

        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Fact]
    public async Task Create_DuplicateMobile_Returns409()
    {
        var mobile = TestData.UniqueMobile(900_001);
        (await _client.PostAsJsonAsync("/api/registrations", TestData.ValidCommand(mobile: mobile)))
            .StatusCode.Should().Be(HttpStatusCode.Created);

        var response = await _client.PostAsJsonAsync("/api/registrations", TestData.ValidCommand(mobile: mobile));

        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Fact]
    public async Task Create_InvalidPayload_Returns400()
    {
        var command = TestData.ValidCommand() with
        {
            FirstName = "Ahmed123",          // digits not allowed
            BirthDate = new DateOnly(2015, 1, 1), // underage
        };

        var response = await _client.PostAsJsonAsync("/api/registrations", command);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Create_CityNotBelongingToGovernorate_Returns400()
    {
        // City 4 (Dokki) belongs to governorate 2 (Giza), not 1 (Cairo).
        var command = TestData.ValidCommand(governorateId: 1, cityId: 4);

        var response = await _client.PostAsJsonAsync("/api/registrations", command);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetById_Unknown_Returns404()
    {
        var response = await _client.GetAsync($"/api/registrations/{Guid.NewGuid()}");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
