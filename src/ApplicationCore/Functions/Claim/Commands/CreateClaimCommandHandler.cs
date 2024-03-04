using ApplicationCore.Functions.Claim.Notifications;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ApplicationCore.Functions.Claim.Commands;

public class CreateClaimCommandHandler : IRequestHandler<CreateClaimCommand, Entities.Claim>
{
    private readonly ILogger<CreateClaimCommandHandler> _logger;
    private readonly IClaimRepository _claimRepository;
    private readonly IMediator _mediator;

    public CreateClaimCommandHandler(
        ILogger<CreateClaimCommandHandler> logger,
        IClaimRepository claimRepository,
        IMediator mediator)
    {
        _logger = logger;
        _claimRepository = claimRepository;
        _mediator = mediator;
    }

    public async Task<Entities.Claim> Handle(CreateClaimCommand request, CancellationToken cancellationToken)
    {
        request.Claim.Id = Guid.NewGuid().ToString();
        
        await _claimRepository.AddItemAsync(request.Claim);
        await _mediator.Publish(new ClaimCreatedNotification(request.Claim.Id));
        
        return request.Claim;
    }
}