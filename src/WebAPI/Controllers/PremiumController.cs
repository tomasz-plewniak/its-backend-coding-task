using ApplicationCore.Functions.Premium.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Enums;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PremiumController : ControllerBase
{
    private readonly ILogger<PremiumController> _logger;
    private readonly IMediator _mediator;

    public PremiumController(
        ILogger<PremiumController> logger,
        IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<ActionResult> ComputePremiumAsync(DateOnly startDate, DateOnly endDate, CoverType coverType)
    {
        var result = await _mediator.Send(new CalculatePremiumQuery(startDate, endDate, coverType));
        return Ok(result);
    }
}
