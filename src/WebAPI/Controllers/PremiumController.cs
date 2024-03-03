using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Enums;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PremiumController : ControllerBase
{
    private readonly ILogger<PremiumController> _logger;
    private readonly IPremiumService _premiumService;

    public PremiumController(ILogger<PremiumController> logger, IPremiumService premiumService)
    {
        _logger = logger;
        _premiumService = premiumService;
    }
    
    [HttpGet]
    public async Task<ActionResult> ComputePremiumAsync(DateOnly startDate, DateOnly endDate, CoverType coverType)
    {
        return Ok(_premiumService.ComputePremium(startDate, endDate, coverType));
    }
}
