using Microsoft.AspNetCore.Mvc;
using Shared.Enums;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PremiumController : ControllerBase
{
    private readonly ILogger<PremiumController> _logger;

    public PremiumController(ILogger<PremiumController> logger)
    {
        _logger = logger;
    }
    
    [HttpGet]
    public async Task<ActionResult> ComputePremiumAsync(DateOnly startDate, DateOnly endDate, CoverType coverType)
    {
        return Ok(ComputePremium(startDate, endDate, coverType));
    }
    
    private decimal ComputePremium(DateOnly startDate, DateOnly endDate, CoverType coverType)
    {
        var multiplier = 1.3m;
        if (coverType == CoverType.Yacht)
        {
            multiplier = 1.1m;
        }

        if (coverType == CoverType.PassengerShip)
        {
            multiplier = 1.2m;
        }

        if (coverType == CoverType.Tanker)
        {
            multiplier = 1.5m;
        }

        var premiumPerDay = 1250 * multiplier;
        var insuranceLength = endDate.DayNumber - startDate.DayNumber;
        var totalPremium = 0m;

        for (var i = 0; i < insuranceLength; i++)
        {
            if (i < 30) totalPremium += premiumPerDay;
            if (i < 180 && coverType == CoverType.Yacht) totalPremium += premiumPerDay - premiumPerDay * 0.05m;
            else if (i < 180) totalPremium += premiumPerDay - premiumPerDay * 0.02m;
            if (i < 365 && coverType != CoverType.Yacht) totalPremium += premiumPerDay - premiumPerDay * 0.03m;
            else if (i < 365) totalPremium += premiumPerDay - premiumPerDay * 0.08m;
        }

        return totalPremium;
    }
}
