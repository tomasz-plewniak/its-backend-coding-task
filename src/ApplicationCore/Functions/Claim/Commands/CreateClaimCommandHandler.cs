using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ApplicationCore.Functions.Claim.Commands;

public class CreateClaimCommandHandler : IRequestHandler<CreateClaimCommand, Entities.Claim>
{
    private readonly ILogger<CreateClaimCommandHandler> _logger;
    private readonly IClaimRepository _claimRepository;

    public CreateClaimCommandHandler(ILogger<CreateClaimCommandHandler> logger, IClaimRepository claimRepository)
    {
        _logger = logger;
        _claimRepository = claimRepository;
    }

    public async Task<Entities.Claim> Handle(CreateClaimCommand request, CancellationToken cancellationToken)
    {
        request.Claim.Id = Guid.NewGuid().ToString();
        
        await _claimRepository.AddItemAsync(request.Claim);
        // TODO: Add audit;
        // _auditer.AuditClaim(claim.Id, "POST");
        
        return request.Claim;
    }
}