using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ApplicationCore.Functions.Claim.Commands;

public class DeleteClaimByIdCommandHandler : IRequestHandler<DeleteClaimByIdCommand>
{
    private readonly ILogger<CreateClaimCommandHandler> _logger;
    private readonly IClaimRepository _claimRepository;


    public DeleteClaimByIdCommandHandler(ILogger<CreateClaimCommandHandler> logger, IClaimRepository claimRepository)
    {
        _logger = logger;
        _claimRepository = claimRepository;
    }
    
    public async Task Handle(DeleteClaimByIdCommand request, CancellationToken cancellationToken)
    {
        await _claimRepository.DeleteItemAsync(request.Id);
        // delete audit.
        // _auditer.AuditClaim(id, "DELETE");
    }
}