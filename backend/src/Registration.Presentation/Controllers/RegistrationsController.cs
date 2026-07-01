using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Registration.Application.Common.Models;
using Registration.Application.Registrations.Commands.CreateRegistration;
using Registration.Application.Registrations.Dtos;
using Registration.Application.Registrations.Queries.GetRegistrationById;
using Registration.Application.Registrations.Queries.ListRegistrations;
using Registration.Presentation.Swagger;
using Swashbuckle.AspNetCore.Filters;

namespace Registration.Presentation.Controllers;

[ApiController]
[Route("api/registrations")]
[Produces("application/json")]
public sealed class RegistrationsController : ControllerBase
{
    private readonly ISender _mediator;

    public RegistrationsController(ISender mediator) => _mediator = mediator;

    /// <summary>Creates a registration with one or more addresses.</summary>
    /// <response code="201">Created. Returns the new id and a Location header.</response>
    /// <response code="400">Validation failed.</response>
    /// <response code="409">Email or mobile number already exists.</response>
    [HttpPost]
    [ProducesResponseType(typeof(CreateRegistrationResult), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [SwaggerRequestExample(typeof(CreateRegistrationCommand), typeof(CreateRegistrationCommandExample))]
    public async Task<IActionResult> Create(
        [FromBody] CreateRegistrationCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    /// <summary>Returns registration details by id.</summary>
    /// <response code="200">The registration details.</response>
    /// <response code="404">No registration with that id.</response>
    [HttpGet("{id:guid}", Name = nameof(GetById))]
    [ProducesResponseType(typeof(RegistrationDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetRegistrationByIdQuery(id), cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    /// <summary>Returns a paged, optionally searched list of registrations.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<RegistrationSummaryDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> List(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] string? search = null,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new ListRegistrationsQuery(page, pageSize, search), cancellationToken);
        return Ok(result);
    }
}
