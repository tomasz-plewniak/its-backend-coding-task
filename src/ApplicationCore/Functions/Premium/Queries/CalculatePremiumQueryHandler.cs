using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ApplicationCore.Functions.Premium.Queries;

public class CalculatePremiumQueryHandler : IRequestHandler<CalculatePremiumQuery, decimal>
{
    private readonly ILogger<CalculatePremiumQueryHandler> _logger;
    private readonly ICalculatePremiumService _calculatePremiumService;

    public CalculatePremiumQueryHandler(ILogger<CalculatePremiumQueryHandler> logger, ICalculatePremiumService calculatePremiumService)
    {
        _logger = logger;
        _calculatePremiumService = calculatePremiumService;
    }
    
    public async Task<decimal> Handle(CalculatePremiumQuery request, CancellationToken cancellationToken)
    {
        return _calculatePremiumService.ComputePremium(request.StartDate, request.EndDate, request.CoverType);
    }
}