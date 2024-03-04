using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ApplicationCore.Functions.Claim.Queries;

public class GetClaimByIdQueryHandler : IRequestHandler<GetClaimByIdQuery, Entities.Claim>
{
    private readonly ILogger<GetClaimByIdQueryHandler> _logger;
    private readonly IClaimRepository _claimRepository;

    public GetClaimByIdQueryHandler(ILogger<GetClaimByIdQueryHandler> logger, IClaimRepository claimRepository)
    {
        _logger = logger;
        _claimRepository = claimRepository;
    }
    
    public Task<Entities.Claim> Handle(GetClaimByIdQuery request, CancellationToken cancellationToken)
    {
        return _claimRepository.GetItemAsync(request.Id, cancellationToken);
    }
}
