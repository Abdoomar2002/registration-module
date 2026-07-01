using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Registration.Application.Lookups.Dtos;
using Registration.Application.Lookups.Queries.GetCities;
using Registration.Application.Lookups.Queries.GetGovernorates;

namespace Registration.Presentation.Controllers;

[ApiController]
[Route("api/lookups")]
[Produces("application/json")]
public sealed class LookupsController : ControllerBase
{
    private readonly ISender _mediator;

    public LookupsController(ISender mediator) => _mediator = mediator;

    /// <summary>Returns the active governorates.</summary>
    [HttpGet("governorates")]
    [ProducesResponseType(typeof(IReadOnlyList<GovernorateDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetGovernorates(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetGovernoratesQuery(), cancellationToken);
        return Ok(result);
    }

    /// <summary>Returns the active cities for a governorate (dependent dropdown).</summary>
    /// <response code="200">The cities belonging to the governorate.</response>
    /// <response code="400">The governorate id is missing or invalid.</response>
    [HttpGet("cities")]
    [ProducesResponseType(typeof(IReadOnlyList<CityDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCities(
        [FromQuery] int governorateId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetCitiesQuery(governorateId), cancellationToken);
        return Ok(result);
    }
}
