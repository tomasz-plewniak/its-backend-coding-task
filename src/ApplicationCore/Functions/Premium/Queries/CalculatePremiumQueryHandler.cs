using ApplicationCore.Interfaces;
using MediatR;

namespace ApplicationCore.Functions.Premium.Queries;

public class CalculatePremiumQueryHandler : IRequestHandler<CalculatePremiumQuery, decimal>
{
    private readonly ICalculatePremiumService _calculatePremiumService;

    public CalculatePremiumQueryHandler(ICalculatePremiumService calculatePremiumService)
    {
        _calculatePremiumService = calculatePremiumService;
    }
    
    public async Task<decimal> Handle(CalculatePremiumQuery request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_calculatePremiumService.ComputePremium(request.StartDate, request.EndDate, request.CoverType));
    }
}