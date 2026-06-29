using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Registration.Application.Lookups.Dtos;

namespace Registration.IntegrationTests;

[Collection(nameof(RegistrationApiCollection))]
public sealed class LookupsEndpointsTests
{
    private readonly HttpClient _client;

    public LookupsEndpointsTests(RegistrationApiFactory factory) => _client = factory.CreateClient();

    [Fact]
    public async Task GetGovernorates_ReturnsSeededGovernorates()
    {
        var governorates = await _client.GetFromJsonAsync<List<GovernorateDto>>("/api/lookups/governorates");

        governorates.Should().NotBeNull();
        governorates!.Should().HaveCount(3);
        governorates.Should().Contain(g => g.NameEn == "Cairo");
    }

    [Fact]
    public async Task GetCities_FiltersBySelectedGovernorate()
    {
        var cairoCities = await _client.GetFromJsonAsync<List<CityDto>>("/api/lookups/cities?governorateId=1");

        cairoCities.Should().NotBeNullOrEmpty();
        cairoCities!.Should().OnlyContain(c => c.GovernorateId == 1);
        cairoCities.Should().Contain(c => c.NameEn == "Nasr City");
        cairoCities.Should().NotContain(c => c.NameEn == "Dokki"); // belongs to Giza
    }

    [Fact]
    public async Task GetCities_InvalidGovernorateId_Returns400()
    {
        var response = await _client.GetAsync("/api/lookups/cities?governorateId=0");
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
