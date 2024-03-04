using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ApplicationCore.Functions.Claim.Queries;

public class GetAllClaimsQueryHandler : IRequestHandler<GetAllClaimsQuery, IEnumerable<Entities.Claim>>
{
    private readonly ILogger<GetAllClaimsQueryHandler> _logger;
    private readonly IClaimRepository _claimRepository;

    public GetAllClaimsQueryHandler(ILogger<GetAllClaimsQueryHandler> logger, IClaimRepository claimRepository)
    {
        _logger = logger;
        _claimRepository = claimRepository;
    }
    
    public Task<IEnumerable<Entities.Claim>> Handle(GetAllClaimsQuery request, CancellationToken cancellationToken)
    {
        return _claimRepository.GetAllAsync();
    }
}