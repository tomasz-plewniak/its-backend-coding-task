﻿using ApplicationCore.Functions.Premium.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Enums;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PremiumsController : ControllerBase
{
    private readonly ILogger<PremiumsController> _logger;
    private readonly IMediator _mediator;

    public PremiumsController(
        ILogger<PremiumsController> logger,
        IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<ActionResult> ComputePremiumAsync(
        DateOnly startDate,
        DateOnly endDate,
        CoverType coverType,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new CalculatePremiumQuery(startDate, endDate, coverType), cancellationToken);
        return Ok(result);
    }
}