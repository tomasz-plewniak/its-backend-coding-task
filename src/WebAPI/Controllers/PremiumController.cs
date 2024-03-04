using ApplicationCore.Functions.Premium.Queries;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Enums;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PremiumController : ControllerBase
{
    private readonly ILogger<PremiumController> _logger;
    private readonly ICalculatePremiumService _calculatePremiumService;
    private readonly IMediator _mediator;

    public PremiumController(
        ILogger<PremiumController> logger,
        ICalculatePremiumService calculatePremiumService,
        IMediator mediator)
    {
        _logger = logger;
        _calculatePremiumService = calculatePremiumService;
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<ActionResult> ComputePremiumAsync(DateOnly startDate, DateOnly endDate, CoverType coverType)
    {
        var result = await _mediator.Send(new CalculatePremiumQuery(startDate, endDate, coverType));
        return Ok(result);
    }
}
