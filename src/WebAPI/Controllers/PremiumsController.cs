using ApplicationCore.Functions.Premium.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Enums;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PremiumsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PremiumsController(
        IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<ActionResult<decimal>> ComputePremiumAsync(
        DateOnly startDate,
        DateOnly endDate,
        CoverType coverType,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new CalculatePremiumQuery(startDate, endDate, coverType), cancellationToken);
        return Ok(result);
    }
}
